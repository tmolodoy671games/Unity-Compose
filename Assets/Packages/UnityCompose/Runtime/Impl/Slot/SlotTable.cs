using System.Collections.Generic;
using System.Text;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal class SlotTable
{
    public const int GroupSize = 1;
    public const int GroupDataSlots = 1;
    public const int MetadataOffset = 0;
    
    public readonly List<object?> Slots;

    public readonly List<ComposeGroup> Groups;

    public SlotTable(int initialGroupCapacity = 64, int initialSlotCapacity = 64)
    {
        Slots = new List<object?>(initialSlotCapacity);
        Groups = new List<ComposeGroup>(initialGroupCapacity);
    }

    public override string ToString()
    {
        return $"Groups:\n{Groups.Format(Slots)}\n" +
               $"Slots:\n{Slots.Format()}";
    }
}

internal class RememberedValue<TKey, TValue>
{
    public RememberedValue(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public TKey Key { get; set; }
    public TValue Value { get; set; }

    public override string ToString()
    {
        return $"({Key}: {Value})";
    }
}

internal static partial class SlotsExtensions
{
    public static string Format(this List<object?> slots)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            builder.Append($"[{i}] ");
            builder.AppendLine(slot?.ToString() ?? "Null");
        }

        return builder.ToString();
    }
}