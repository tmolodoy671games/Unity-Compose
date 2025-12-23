using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class ModifierGroup
{
    public const int MetadataSize = 1;
    public const int ModifiersOffset = 0;
}

internal static class ModifierGroupSlotsExtensions
{
    public static ModifiersStatePair? GetModifiersStatePair(this Slots slots, int slotIndex)
    {
        return slots[slotIndex + ModifierGroup.ModifiersOffset] as ModifiersStatePair;
    }

    public static void InsertModifiersStatePair(this Slots slots, int slotIndex)
    {
        slots.Insert(slotIndex + ModifierGroup.ModifiersOffset, ComposeEmptySlot.Instance);
    }

    public static void SetModifiersStatePair(this Slots slots, int slotIndex, ModifiersStatePair state)
    {
        slots[slotIndex + ModifierGroup.ModifiersOffset] = state;
    }
}