using System;
using System.IO;
using System.Linq;
using StableCollections;
using UnityEditor;
using UnityEngine;
using static Packages.UnityCompose.Editor.Compilation.Release.ComposeReleaseAssembliesFinder;
using static Packages.UnityCompose.Editor.Compilation.Release.ComposeReleaseFileRewriter;

namespace Packages.UnityCompose.Editor.Compilation.Release;

public static class ComposeReleaseCompiler
{
    public static void Compile(
        Action compile
    )
    {
        var files = FindFiles()
            .ToImmutableStableList();
        var originalFileContents = files
            .Select(it => (FullName: it.FullName, Content: File.ReadAllText(it.FullName)))
            .ToImmutableStableList();
        var unityComposeDirectory = new DirectoryInfo("UnityCompose");
        if (!unityComposeDirectory.Exists)
            unityComposeDirectory.Create();
        var composeBuildFile = new FileInfo(Path.Combine("UnityCompose", ".unityComposeReleaseLock"));
        composeBuildFile.Create().Dispose();
        Rewrite(files);
        AssetDatabase.Refresh();
        try
        {
            compile();
        }
        finally
        {
            foreach (var originalFileContent in originalFileContents)
                File.WriteAllText(originalFileContent.FullName, originalFileContent.Content);
            composeBuildFile.Delete();
        }
    }
}