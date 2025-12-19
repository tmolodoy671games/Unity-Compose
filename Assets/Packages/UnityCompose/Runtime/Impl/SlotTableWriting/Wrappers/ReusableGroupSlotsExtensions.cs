using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class ReusableGroup
{
    public const int MetadataSize = 1;
    public const int VisualElementOffset = 0;
}

internal static class ReusableGroupSlotsExtensions
{
    public static VisualElement? GetVisualElement(this Slots slots, int index)
    {
        return slots[index + ReusableGroup.VisualElementOffset] as VisualElement;
    }

    public static void SetVisualElement(this Slots slots, int index, VisualElement visualElement)
    {
        slots[index + ReusableGroup.VisualElementOffset] = visualElement;
    }

    public static void InsertVisualElement(this Slots slots, int index)
    {
        slots.Insert(index + ReusableGroup.VisualElementOffset, ComposeEmptySlot.Instance);
    }
}