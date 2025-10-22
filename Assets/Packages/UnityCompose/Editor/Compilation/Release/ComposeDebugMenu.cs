using UnityCompose.Editor.InEditor;
using UnityEditor;

namespace Packages.UnityCompose.Editor.Compilation.Release;

public static class ComposeDebugMenu
{
    [MenuItem("Tools/Unity Compose/Patch Debug Assemblies")]
    private static void PatchDebugAssemblies()
    {
        ComposeDebugAssemblyCompiler.ModifyAssemblies(false);
    }
}