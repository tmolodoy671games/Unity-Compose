using System.Collections.Generic;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class LocalGroup
{
    public const int MetadataSize = 1;
    public const int CompositionLocalMapOffset = 0;
}

internal static class LocalGroupSlotsExtensions
{
    public static CompositionLocalMap GetCompositionLocalMap(
        this Slots slots,
        int index
    )
    {
        return (CompositionLocalMap) slots[index + LocalGroup.CompositionLocalMapOffset].NotNull();
    }

    public static void InsertCompositionLocalMap(this Slots slots, int index, CompositionLocalMap map)
    {
        slots.Insert(index + LocalGroup.CompositionLocalMapOffset, map);
    }
}