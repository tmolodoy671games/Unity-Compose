using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;

internal static class ComposeGroupExtensions
{
    public static IEnumerable<IComposeGroupDeprecated> Ancestors(
        this IComposeGroupDeprecated groupDeprecated,
        bool includeSelf = false
        )
    {
        if (includeSelf)
            yield return groupDeprecated;
        var currentParent = groupDeprecated.Parent;
        while (currentParent != null)
        {
            yield return currentParent;
            currentParent = currentParent.Parent;
        }
    }
}