using System.Collections.Generic;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class LocalGroup
{
    public const int MetadataSize = 1;
    public const int CompositionLocalMapOffset = 0;
}

internal static class LocalGroupSlotsExtensions
{
    public static Dictionary<ICompositionLocal, IMutableState<object?>>? GetCompositionLocalMap(
        this Slots slots,
        int index
    )
    {
        return slots[index + LocalGroup.CompositionLocalMapOffset] as
            Dictionary<ICompositionLocal, IMutableState<object?>>;
    }

    public static void SetCompositionLocalMap(
        this Slots slots,
        int index,
        Dictionary<ICompositionLocal, IMutableState<object?>>? map
    )
    {
        slots[index + LocalGroup.CompositionLocalMapOffset] = map;
    }

    public static void InsertCompositionLocalMap(this Slots slots, int index)
    {
        slots.Insert(index + LocalGroup.CompositionLocalMapOffset, ComposeEmptySlot.Instance);
    }
}