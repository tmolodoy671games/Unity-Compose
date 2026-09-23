#if UNITY_EDITOR
using System.IO;
using Mono.Cecil;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

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
        var assembly = AssemblyDefinition.ReadAssembly(fileInfo.FullName);
        ComposableMethodRewriter.Patch(assembly);
    }
}
#endif