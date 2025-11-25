using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal class SlotTable
{
    public readonly List<ComposeGroup> Groups;

    public SlotTable(int initialGroupCapacity = 64, int initialSlotCapacity = 64)
    {
        Groups = new List<ComposeGroup>();
    }

    public string ToString(
        int currentGroupIndex,
        int parentGroupIndex
    )
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Groups:\n{Groups.Format(currentGroupIndex, parentGroupIndex)}");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine();
        return builder.ToString();
    }
}

internal static class GroupIndex
{
    public const int MetadataSize = 1;
    public const int MetadataOffset = 0;
}

internal static class SlotIndex
{
    public const int DataSize = 1;
    public const int DataOffset = 0;
}

internal static partial class SlotsExtensions
{
    public static string Format(this IList<object?> slots, int currentSlotIndex)
    {
        var builder = new StringBuilder();
        if (currentSlotIndex < 0)
            builder.AppendLine(" < CURRENT_SLOT_INDEX");
        for (var i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            builder.Append($"[{i}] ");
            builder.Append(slot?.ToString() ?? "Null");
            if (currentSlotIndex == i)
                builder.Append(" < CURRENT_SLOT_INDEX");
            builder.AppendLine();
        }
        if (currentSlotIndex >= slots.Count)
            builder.Append(" < CURRENT_SLOT_INDEX");

        return builder.ToString();
    }
}