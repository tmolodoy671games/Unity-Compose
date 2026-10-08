#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Packages.UnityCompose.Editor.Extensions;
using Packages.UnityCompose.Editor.Utils;
using StableCollections;
using Unity.CompilationPipeline.Common.Diagnostics;
using Unity.CompilationPipeline.Common.ILPostProcessing;

namespace Packages.UnityCompose.Editor.Rewriters;

internal static class ComposableMethodRewriter
{
    public static bool CanPatch(AssemblyDefinition assembly)
    {
        return assembly.MainModule.GetTypes()
            .SelectMany(it => it.Methods)
            .Any(it => it.IsRecompiled());
    }

    public static IStableList<DiagnosticMessage> Patch(AssemblyDefinition assembly)
    {
        var messages = MutableStableListOf<DiagnosticMessage>();
        var patchedCount = 0;
        messages.Add(new DiagnosticMessage { MessageData = $"Patching {assembly.Name}" });

        foreach (var type in assembly.MainModule.GetTypes().ToList())
        {
            var patched = new HashSet<MethodDefinition>();

            // 1. __Foo -> Foo
            foreach (var recompiledMethod in type.Methods.Where(it => it.IsRecompiled()).ToList())
            {
                var originalMethod = FindOriginal(type, recompiledMethod);
                if (originalMethod == null || !originalMethod.HasBody || !recompiledMethod.HasBody)
                {
                    messages.Add(MissingRecompiled(type, recompiledMethod));
                    continue;
                }

                var document = originalMethod.DebugInformation.SequencePoints
                    .FirstOrDefault(it => it.Document != null)?.Document;

                originalMethod.CopyBodyFrom(recompiledMethod);
                type.RetargetClosures(recompiledMethod.Name, document);

                patched.Add(originalMethod);
                patchedCount++;
            }

            // 2. [Composable] методы, для которых пара не нашлась
            foreach (var composableMethod in type.Methods.Where(it => it.IsComposable()).ToList())
            {
                if (!composableMethod.HasBody || patched.Contains(composableMethod))
                    continue;

                var recompiledMethod = FindRecompiled(type, composableMethod);
                if (recompiledMethod == null)
                {
                    messages.Add(MissingRecompiled(type, composableMethod));
                    continue;
                }

                composableMethod.CopyBodyFrom(recompiledMethod);
                patchedCount++;
            }
        }

        messages.Add(new DiagnosticMessage
        {
            MessageData = $"Compose ILPP: patched {patchedCount} methods in {assembly.Name.Name}",
        });
        return messages;
    }

    private static MethodDefinition? FindOriginal(TypeDefinition type, MethodDefinition recompiled)
    {
        // Убираем только префикс "__", а не все подчёркивания
        var name = recompiled.Name;
        var originalName = name.StartsWith("get_")
            ? name.Replace("get___", "get_")
            : name.Substring(2);
        // var name = recompiled.Name.StartsWith("__") ? recompiled.Name.Substring(2) : recompiled.Name;
        return type.Methods
            .Where(it => it.Name == originalName)
            .FirstOrDefault(it =>
                it.Parameters.SequenceEqual(recompiled.Parameters, new ParameterEqualityComparer()));
    }

    private static MethodDefinition? FindRecompiled(TypeDefinition type, MethodDefinition composable)
    {
        var name = composable.Name;
        var recompiledName = name.StartsWith("get_")
            ? name.Replace("get_", "get___")
            : "__" + name;
        return type.Methods
            .Where(it => it.Name == recompiledName)
            .FirstOrDefault(it =>
                it.Parameters.SequenceEqual(composable.Parameters, new ParameterEqualityComparer()));
    }

    private static DiagnosticMessage MissingRecompiled(TypeDefinition type, MethodDefinition composable)
    {
        var message = new DiagnosticMessage
        {
            DiagnosticType = DiagnosticType.Error,
            MessageData =
                $"{composable.Name}: {type.FullName} is not marked as partial (or code generation failed)!",
        };

        var sequencePoint = composable.DebugInformation.SequencePoints.FirstOrDefault();
        if (sequencePoint != null)
        {
            message.File = sequencePoint.Document.Url;
            message.Line = sequencePoint.StartLine;
            message.Column = sequencePoint.StartColumn;
        }

        return message;
    }
}
#endif