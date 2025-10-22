#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Compilation;

// ReSharper disable CheckNamespace
namespace UnityCompose.Editor.InEditor;

[InitializeOnLoad]
public static class ComposeDebugCompilerEditorScript
{
    static ComposeDebugCompilerEditorScript()
    {
        CompilationPipeline.compilationFinished += _ => ComposeDebugAssemblyCompiler.ModifyAssemblies(false);
    }

    // [PostProcessBuild]
    // private static void OnCompilationFinished(BuildTarget target, string pathToBuiltProject)
    // {
    //     ComposeAssemblyCompiler.ModifyAssemblies(true);
    // }
}
#endif