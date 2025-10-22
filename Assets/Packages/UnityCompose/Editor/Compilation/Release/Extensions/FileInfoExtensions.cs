using System.Collections.Generic;
using System.IO;

namespace Packages.UnityCompose.Editor.Compilation.Release.Extensions;

internal static class FileInfoExtensions
{
    public static IEnumerable<DirectoryInfo> AncestorDirectories(this FileInfo fileInfo)
    {
        var parent = fileInfo.Directory;
        while (parent != null)
        {
            yield return parent;
            parent = parent.Parent;
        }
    }
}