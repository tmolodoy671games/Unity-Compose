using UnityEditor;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

internal static class ApplicationUtils
{
    public static bool IsPlaying
    {
        get
        {
#if UNITY_EDITOR
            return EditorApplication.isPlaying && !BuildPipeline.isBuildingPlayer;
#else
            return Application.isPlaying;
#endif
        }
    }
}