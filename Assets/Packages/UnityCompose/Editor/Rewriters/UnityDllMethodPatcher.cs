#if UNITY_EDITOR
using System.IO;
using Mono.Cecil;
using Packages.UnityCompose.Editor.Utils;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Packages.UnityCompose.Editor.Rewriters;

internal class UnityDllMethodPatcher : IPostBuildPlayerScriptDLLs
{
    public int callbackOrder => 0;

    public void OnPostBuildPlayerScriptDLLs(BuildReport report)
    {
        foreach (var file in report.GetFiles())
        {
            var fileInfo = new FileInfo(file.path);
            if (fileInfo.Extension != ".dll")
                continue;

            Patch(fileInfo);
        }
    }

    private static void Patch(FileInfo fileInfo)
    {
        try
        {
            var assembly = AssemblyDefinitionFactory.Create(fileInfo);
            Debug.Log($"Patch {assembly.Name}");
            ComposableMethodRewriter.Patch(assembly);
        }
        catch (AssemblyResolutionException)
        {
            Debug.LogWarning($"Failed to patch {fileInfo.Name}");
        }
    }
}
#endif