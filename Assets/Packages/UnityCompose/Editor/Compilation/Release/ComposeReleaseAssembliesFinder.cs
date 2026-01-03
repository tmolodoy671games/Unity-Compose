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
    public static IEnumerable<FileInfo> FindFiles()
    {
        return FindComposeDirectories()
            .SelectMany(FindComposeSources);
    }

    private static IEnumerable<DirectoryInfo> FindComposeDirectories()
    {
        return new DirectoryInfo(".")
            .EnumerateDirectories("*", SearchOption.AllDirectories)
            .Where(static it => it.IsComposeDirectory());
    }

    private static IEnumerable<FileInfo> FindComposeSources(DirectoryInfo directory)
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
                    .Where(it => it. Name == originalFile.Name.Replace(".cs", ".g.cs"))
                    .FirstOrDefault(it => it.Directory?.Name == (namespaceName ?? "Compose"));
                return generatedFile == null ? null : originalFile;
            });
    }

    private static bool IsComposeDirectory(this DirectoryInfo directory)
    {
        // if (directory.Name == "UnityCompose")
        //     return true;
        var definitionFile = directory.EnumerateFiles()
            .FirstOrDefault(it => it.Extension == ".asmdef");
        return definitionFile != null && File.ReadAllText(definitionFile.FullName).Contains("\"UnityCompose\"");
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