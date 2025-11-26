using System.Collections.Generic;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Utils;

internal static class ComposeGroupExtensions
{
    public static IEnumerable<ReusableComposeGroup> Ancestors(
        this ComposeGroup group
    )
    {
        var currentParent = group.Parent;
        var i = 0;
        while (currentParent != null && i++ < 100)
        {
            yield return currentParent;
            currentParent = currentParent.Parent;
        }
    }
}