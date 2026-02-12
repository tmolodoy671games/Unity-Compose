#if UNITY_EDITOR
using System.IO;
using StableCollections;
using UnityEditor;

namespace UnityCompose.Samples.Editor;

internal static class ClearGeneratedCodeMenu
{
    [MenuItem("Tools/Clear Generated Code")]
    private static void ClearGeneratedCode()
    {
        var generatedFiles = new DirectoryInfo(Application.dataPath)
            .EnumerateFiles("*.g.cs", SearchOption.AllDirectories)
            .ToImmutableStableList();
        foreach (var generatedFile in generatedFiles)
            generatedFile.Delete();
    }
}
#endif
