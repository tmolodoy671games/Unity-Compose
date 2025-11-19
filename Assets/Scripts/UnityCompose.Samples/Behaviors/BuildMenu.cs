#if UNITY_EDITOR
using UnityEditor;
using Packages.UnityCompose.Editor.Compilation.Release;

namespace UnityCompose.Samples.Behaviors;

internal static class BuildMenu
{
    [MenuItem("Tools/Unity Compose/Build OS X")]
    private static void BuildOsX()
    {
        ComposeReleaseCompiler.Compile(() => BuildPipeline.BuildPlayer(
            buildPlayerOptions: new BuildPlayerOptions
            {
                target = BuildTarget.StandaloneOSX,
                locationPathName = "Build/OsX/UnityCompose.app",
            }
        ));
    }
}
#endif