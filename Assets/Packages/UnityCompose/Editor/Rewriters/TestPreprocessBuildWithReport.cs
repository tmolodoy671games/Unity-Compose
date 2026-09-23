#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Packages.UnityCompose.Editor.Rewriters;

internal class TestPreprocessBuildWithReport : IPreprocessBuildWithReport
{
    public int callbackOrder { get; }

    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("OnPreprocessBuild()"); 
    }
}
#endif