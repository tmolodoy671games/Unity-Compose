#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using SharpExtensions;
using StableCollections;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose.Editor.InEditor;

internal static class ComposeDebugAssemblyCompiler
{
    public static void ModifyAssemblies(bool isRelease)
    {
        var isBuilding = BuildPipeline.isBuildingPlayer ||
                         new FileInfo(Path.Combine("UnityCompose", ".unityComposeReleaseLock")).Exists;
        if (isBuilding)
            return;
        MigrateDlls();
        GetAssemblies(isRelease)
            .AsParallel()
            .ForAll(it => ProcessAssembly(it, isRelease));
    }

    private static void MigrateDlls()
    {
        var outputDirectory = new DirectoryInfo(Path.Combine(Application.dataPath, "..", "UnityCompose"));
        if (outputDirectory.Exists && outputDirectory.GetFiles().Length > 0)
            return;
        if (!outputDirectory.Exists)
            outputDirectory.Create();
        var assembliesFiles = new DirectoryInfo(Path.Combine(Application.dataPath, ".."))
            .EnumerateDirectories("Assemblies", SearchOption.AllDirectories)
            .Where(it => it.Parent?.Name == "Editor" && it.Parent?.Parent?.Name == "UnityCompose")
            .SelectMany(it => it.EnumerateFiles())
            .Where(it => it.Extension == ".unitycomposedll");
        foreach (var file in assembliesFiles)
        {
            File.Copy(
                file.FullName,
                Path.Combine(outputDirectory.FullName, file.Name.Replace(".unitycomposedll", ".dll"))
            );
        }
    }

    private static IStableList<AssemblyDefinition> GetAssemblies(bool isRelease)
    {
        var projectAssemblies = new DirectoryInfo(Application.dataPath + "/Scripts")
            .EnumerateFiles("*.asmdef", SearchOption.AllDirectories)
            .Select(it => it.Name.Replace(".asmdef", ""))
            .Append("UnityCompose")
            .ToImmutableStableList();
        var resolver = new DefaultAssemblyResolver();
        resolver.RegisterFoldersRecursively(
            new DirectoryInfo(Path.Combine(Application.dataPath, "..", "Build"))
        );
        if (!isRelease)
        {
            resolver.RegisterFoldersRecursively(
                new DirectoryInfo(
                    Path.Combine(Application.dataPath, "..", "Library", "ScriptAssemblies")
                )
            );
            resolver.RegisterFoldersRecursively(
                new DirectoryInfo(
                    Path.Combine(Application.dataPath, "..", "Library", "PackageCache")
                )
            );
            resolver.RegisterFoldersRecursively(
                new DirectoryInfo(Path.Combine(Application.dataPath, "..", "UnityCompose"))
            );
        }

        var readerParams = new ReaderParameters
        {
            AssemblyResolver = resolver,
            ReadingMode = ReadingMode.Immediate,
            ReadWrite = true,
        };
        if (!isRelease)
        {
            readerParams.ReadSymbols = true;
            readerParams.ThrowIfSymbolsAreNotMatching = false;
            readerParams.SymbolReaderProvider = new PortablePdbReaderProvider();
        }

        return isRelease
            ? new DirectoryInfo(Application.dataPath).Parent.NotNull()
                .GetDirectories("Build")
                .First()
                .GetFiles("*.dll", SearchOption.AllDirectories)
                .Where(it => projectAssemblies.Contains(it.Name.Replace(".dll", "")))
                .Select(it =>
                    AssemblyDefinition.ReadAssembly(it.FullName, readerParams))
                .ToImmutableStableList()
            : new DirectoryInfo(Application.dataPath).Parent.NotNull()
                .GetDirectories(Path.Combine("Library", "ScriptAssemblies"))
                .First()
                .EnumerateFiles("*.dll", SearchOption.AllDirectories)
                .Where(it => projectAssemblies.Contains(it.Name.Replace(".dll", "")))
                .Select(it =>
                    AssemblyDefinition.ReadAssembly(it.FullName, readerParams))
                .ToImmutableStableList();
    }

    private static void RegisterFoldersRecursively(this DefaultAssemblyResolver resolver, DirectoryInfo directory)
    {
        if (!directory.Exists)
            return;
        resolver.AddSearchDirectory(directory.FullName);
        foreach (var subDirectory in directory.EnumerateDirectories())
            resolver.RegisterFoldersRecursively(subDirectory);
    }

    private static void ProcessAssembly(AssemblyDefinition assembly, bool isRelease)
    {
        var needToRewrite = assembly.Modules
            .SelectMany(it => it.Types)
            .AsParallel()
            .Select(ProcessType)
            .ToImmutableStableList()
            .Any(it => it);
        if (needToRewrite)
        {
            var writerParams = new WriterParameters();
            if (!isRelease)
            {
                writerParams.WriteSymbols = true;
                writerParams.SymbolWriterProvider = new PortablePdbWriterProvider();
            }

            assembly.Write(writerParams);
        }

        assembly.Dispose();
    }

    private static bool ProcessType(TypeDefinition type)
    {
        var composableMethods = type
            .Methods
            .Where(ShouldBeRewritten)
            .ToImmutableStableList();
        if (composableMethods.IsEmpty())
            return false;
        foreach (var composableMethod in composableMethods)
        {
            var otherMethod = type.Methods
                .FirstOrDefault(it =>
                    it.Name == "__" + composableMethod.Name && ParametersMatch(it, composableMethod)
                );
            if (otherMethod == null)
            {
                Debug.LogWarning(
                    $"No Composable implementation generated for method {composableMethod.DeclaringType.Name}.{composableMethod.Name}. Did you forget marking class as partial?");
                continue;
            }

            composableMethod.Body = otherMethod.Body;
            CopyDebugInformation(composableMethod, otherMethod);
        }

        return true;
    }

    private static void CopyDebugInformation(MethodDefinition targetMethod, MethodDefinition sourceMethod)
    {
        var sourceDebugInformation = sourceMethod.DebugInformation;
        var targetDebugInformation = targetMethod.DebugInformation;

        targetDebugInformation.Scope = sourceDebugInformation.Scope;
        targetDebugInformation.StateMachineKickOffMethod = sourceDebugInformation.StateMachineKickOffMethod;
        targetDebugInformation.SequencePoints.Clear();
        targetDebugInformation.SequencePoints.AddRange(sourceDebugInformation.SequencePoints);

        targetDebugInformation.CustomDebugInformations.Clear();
        targetDebugInformation.CustomDebugInformations.AddRange(sourceDebugInformation.CustomDebugInformations);
    }

    private static bool ParametersMatch(MethodDefinition method, MethodDefinition referenceMethod)
    {
        if (method.Parameters.Count != referenceMethod.Parameters.Count)
            return false;

        for (int i = 0; i < method.Parameters.Count; i++)
        {
            var param1 = method.Parameters[i];
            var param2 = referenceMethod.Parameters[i];

            if (param1.ParameterType.FullName != param2.ParameterType.FullName)
                return false;
        }

        return true;
    }

    private static bool ShouldBeRewritten(this MethodDefinition method)
    {
        if (method.IsAbstract) return false;
        if (method.Name.StartsWith("__")) return false;
        var composableAttribute = method.CustomAttributes
            .FirstOrDefault(it => it.AttributeType.Name == "ComposableAttribute");
        if (composableAttribute == null)
            return false;
        return method.CustomAttributes
            .None(it => it.AttributeType.Name == "CompiledAttribute");
    }

    private static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
            collection.Add(item);
    }
}
#endif