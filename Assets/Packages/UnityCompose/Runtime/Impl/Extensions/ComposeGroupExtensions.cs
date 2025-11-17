using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;

internal static class ComposeGroupExtensions
{
    public static IEnumerable<IComposeGroup> Ancestors(this IComposeGroup group)
    {
        var currentParent = group.Parent;
        while (currentParent != null)
        {
            yield return currentParent;
            currentParent = currentParent.Parent;
        }
    }
}