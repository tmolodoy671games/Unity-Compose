using UnityEditor;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Packages.UnityCompose.Editor.Compilation.Release;
#endif

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal class BuildMenu : MonoBehaviour
    {
        [Button]
        private void Build()
        {
#if UNITY_EDITOR
            ComposeReleaseCompiler.Compile(() => BuildPipeline.BuildPlayer(
                buildPlayerOptions: new BuildPlayerOptions
                {
                    target = BuildTarget.StandaloneOSX,
                    locationPathName = "Build/OsX/UnityCompose.app",
                }
            ));
#endif
        }
    }
}