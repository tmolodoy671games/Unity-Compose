using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Packages.UnityCompose.Editor.Compilation.Release.Extensions;
using SharpExtensions;
using StableCollections;
using UnityEngine;

namespace Packages.UnityCompose.Editor.Compilation.Release;

internal static class ComposeReleaseAssembliesFinder
{
    public static IEnumerable<ComposeSourcePair> FindFiles()
    {
        return FindComposeDirectories()
            .SelectMany(FindComposeSources);
    }

    public static IEnumerable<DirectoryInfo> FindComposeDirectories()
    {
        var searchDirectories = Enumerables.Of(
            new DirectoryInfo(Application.dataPath),
            new DirectoryInfo(Path.Combine("Library", "PackageCache"))
        );
        return searchDirectories
            .SelectMany(static it => it.EnumerateDirectories("*", SearchOption.AllDirectories))
            .Where(static it => it.IsComposeDirectory());
    }

    private static IEnumerable<ComposeSourcePair> FindComposeSources(DirectoryInfo directory)
    {
        var generatedFiles = directory.GetFiles("*.g.cs", SearchOption.AllDirectories)
            .Where(file => file.IsComposeGeneratedFile(directory))
            .ToImmutableStableList();
        var originalFileNames = generatedFiles
            .Select(static it => it.Name.Replace(".g.cs", ".cs"))
            .ToImmutableStableSet();
        var originalFiles = directory.GetFiles("*.cs", SearchOption.AllDirectories)
            .Where(static it => !it.Name.EndsWith(".g.cs"))
            .Where(file => originalFileNames.Contains(file.Name));
        return originalFiles
            .SelectNotNull(originalFile =>
            {
                var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(originalFile.FullName));
                var namespaceName = tree.GetRoot()
                    .DescendantNodes()
                    .OfType<BaseNamespaceDeclarationSyntax>()
                    .FirstOrDefault()
                    ?.Name.ToString();
                var generatedFile = generatedFiles
                    .Where(it => it.Name == originalFile.Name.Replace(".cs", ".g.cs"))
                    .Where(it => it.Directory?.Name == (namespaceName ?? "Compose"))
                    .FirstOrDefault();
                return generatedFile == null ? null : new ComposeSourcePair(originalFile, generatedFile);
            });
    }

    private static bool IsComposeDirectory(this DirectoryInfo directory)
    {
        // if (directory.Name == "UnityCompose")
        //     return true;
        var definitionFile = directory.EnumerateFiles()
            .FirstOrDefault(it => it.Extension == ".asmdef");
        if (definitionFile == null)
            return false;
        return File.ReadAllText(definitionFile.FullName).Contains("UnityCompose");
    }

    private static bool IsComposeGeneratedFile(this FileInfo fileInfo, DirectoryInfo directoryInfo)
    {
        return fileInfo.AncestorDirectories()
            .Any(it => it.Name == "Compose" &&
                       it.Parent?.Name == "Generated"
            );
    }
}

internal record ComposeSourcePair(
    FileInfo Original,
    FileInfo Generated
);