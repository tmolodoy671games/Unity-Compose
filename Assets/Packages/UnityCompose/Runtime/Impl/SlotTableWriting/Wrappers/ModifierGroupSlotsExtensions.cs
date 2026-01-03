using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class ModifierGroup
{
    public const int MetadataSize = 1;
    public const int ModifiersOffset = 0;
}

internal static class ModifierGroupSlotsExtensions
{
    public static ModifiersStatePair GetModifiersStatePair(this Slots slots, int slotIndex)
    {
        return (ModifiersStatePair) slots[slotIndex + ModifierGroup.ModifiersOffset].NotNull();
    }

    public static void InsertModifiersStatePair(this Slots slots, int slotIndex, ModifiersStatePair state)
    {
        slots.Insert(slotIndex + ModifierGroup.ModifiersOffset, state);
    }
}