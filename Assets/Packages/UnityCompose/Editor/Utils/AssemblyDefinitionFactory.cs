#if UNITY_EDITOR
using System.IO;
using Mono.Cecil;
using UnityEditor;

namespace Packages.UnityCompose.Editor.Utils;

internal static class AssemblyDefinitionFactory
{
    public static AssemblyDefinition Create(FileInfo file)
    {
        var resolver = new DefaultAssemblyResolver();

        AddAssemblySearchDirectories(resolver, file);
        return AssemblyDefinition.ReadAssembly(file.FullName);
    }

    private static void AddAssemblySearchDirectories(
        DefaultAssemblyResolver resolver,
        FileInfo assembly
    )
    {
        // Directory containing the assembly itself.
        resolver.AddSearchDirectory(assembly.Directory!.FullName);

        // Unity managed assemblies.
        resolver.AddSearchDirectory(
            Path.Combine(
                EditorApplication.applicationContentsPath,
                "Managed"
            )
        );

        // Unity modules.
        resolver.AddSearchDirectory(
            Path.Combine(
                EditorApplication.applicationContentsPath,
                "Managed",
                "UnityEngine"
            )
        );

        // Packages / project assemblies which may be next to the target assembly.
        resolver.AddSearchDirectory(
            Path.GetDirectoryName(typeof(EditorApplication).Assembly.Location)!);
    }
}
#endif