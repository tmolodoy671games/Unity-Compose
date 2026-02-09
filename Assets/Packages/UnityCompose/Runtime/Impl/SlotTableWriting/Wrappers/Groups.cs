using System;
using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct Groups
{
    public const int GroupHeaderSize = 1;

    private readonly GapBufferList<ComposeGroup> _groups;

    public Groups(GapBufferList<ComposeGroup> groups)
    {
        _groups = groups;
    }

    public ComposeGroup this[int index]
    {
        get => _groups[index];
        set => _groups[index] = value;
    }

    public int Count => _groups.Count;
    public int GapStart => _groups.GapStart;
    public int GapLength => _groups.GapLength;

    public void Insert(int index, ComposeGroup group) => _groups.Insert(index, group);

    public void RemoveRange(int index, int count) => _groups.RemoveRange(index, count);

    public void Swap(int sourceIndex, int sourceCount, int targetIndex, int targetCount)
    {
        _groups.Swap(sourceIndex, sourceCount, targetIndex, targetCount);
    }

    public int LogicalToAbsoluteIndex(int index) => _groups.LogicalToAbsoluteIndex(index);
    public int AbsoluteToLogicalIndex(int index) => _groups.AbsoluteToLogicalIndex(index);

    public void AddItemsShiftObserver(Action<ItemsShiftEvent> onItemsShift) =>
        _groups.AddItemsShiftObserver(onItemsShift);

    public void Clear() => _groups.Clear();

    public string ToString(
        int currentParentIndex,
        int currentGroupIndex,
        Anchors groupsAnchors,
        Anchors slotsAnchors,
        Slots slots
    )
    {
        var builder = new StringBuilder();
        if (currentParentIndex == -1)
            builder.AppendLine("< CURRENT_PARENT_INDEX");
        for (var i = 0; i < _groups.Count; i++)
        {
            var group = _groups[i];
            builder.Append($"[{i}]\t");
            var ancestorsCount = group.AncestorsCount(groupsAnchors, this);
            builder.Append("-".Multiply(ancestorsCount));
            builder.Append(group.ToString(groupsAnchors, slotsAnchors, slots, this));
            var isSelfIndexInvalid = group.AnchorId.IsValid &&
                                     (!groupsAnchors.ContainsIndex(group.AnchorId) ||
                                      group.SafeIndex(groupsAnchors, this) != i);
            if (isSelfIndexInvalid)
                builder.Append(" [SELF ANCHOR IS INVALID]");
            var isDataIndexInvalid = group.DataAnchorId.IsValid &&
                                     !slotsAnchors.ContainsIndex(group.DataAnchorId);
            if (isDataIndexInvalid)
                builder.Append(" [DATA ANCHOR IS INVALID]");
            if (ancestorsCount < 0)
                builder.Append(" [ANCESTORS STRUCTURE IS INVALID]");
            if (currentParentIndex == i)
                builder.Append(" < CURRENT_PARENT_INDEX");
            if (currentGroupIndex == i)
                builder.Append(" < CURRENT_GROUP_INDEX");
            builder.AppendLine();
        }

        if (currentGroupIndex == _groups.Count)
            builder.AppendLine("< CURRENT_GROUP_INDEX");
        return builder.ToString();
    }
}

internal static class ComposeGroupExtensions
{
    public static int Index(this ComposeGroup group, Anchors anchors, Groups groups)
    {
        if (!group.AnchorId.IsValid)
            return -1;
        return groups.AbsoluteToLogicalIndex(anchors[group.AnchorId].Location);
    }

    public static int ParentIndex(this ComposeGroup group, Anchors anchors, Groups groups)
    {
        if (!group.ParentAnchorId.IsValid)
            return -1;
        return groups.AbsoluteToLogicalIndex(anchors[group.ParentAnchorId].Location);
    }

    public static int SlotIndex(this ComposeGroup group, Anchors anchors, Slots slots)
    {
        if (!group.DataAnchorId.IsValid)
            return -1;
        return slots.AbsoluteToLogicalIndex(anchors[group.DataAnchorId].Location);
    }

    public static int SafeIndex(this ComposeGroup group, Anchors anchors, Groups groups)
    {
        try
        {
            return group.Index(anchors, groups);
        }
        catch (Exception)
        {
            return -2;
        }
    }

    private static int SafeParentIndex(this ComposeGroup group, Anchors anchors, Groups groups)
    {
        try
        {
            return group.ParentIndex(anchors, groups);
        }
        catch (Exception)
        {
            return -2;
        }
    }

    private static int SafeSlotIndex(this ComposeGroup group, Anchors anchors, Slots slots)
    {
        try
        {
            return group.SlotIndex(anchors, slots);
        }
        catch (Exception)
        {
            return -2;
        }
    }

    public static string ToString(
        this ComposeGroup group,
        Anchors groupsAnchors,
        Anchors slotsAnchors,
        Slots slots,
        Groups groups
    )
    {
        var builder = new StringBuilder();
        builder.Append(group.Type + "Group");
        builder.Append("(");
        builder.Append($"Key: {group.Key}");
        // builder.Append($", ParentIndex: {group.SafeParentIndex(groupsAnchors, groups)}");
        // builder.Append($", Index: {group.Index(groupsAnchors, groups)}");
        builder.Append($", Size: {group.Size}");
        // builder.Append($", DataIndex: {group.SafeSlotIndex(slotsAnchors, slots)}");
        builder.Append($", SlotsSize: {group.SlotsSize}");
        // builder.Append($", ElementIndex: {group.ElementIndex}");
        // builder.Append($", ElementsCount: {group.ElementsCount}");
        builder.Append(")");
        if (group.DataAnchorId.IsValid)
        {
            var slotIndex = group.SafeSlotIndex(slotsAnchors, slots);
            if (slotIndex < 0)
            {
                builder.Append("\t\t");
                builder.Append("[INVALID DATA INDEX]");
                return builder.ToString();
            }

            switch (group.Type)
            {
                case ComposeGroupType.Reusable:
                    builder.Append("\t\t");
                    try
                    {
                        var visualElement = slots.GetReusableNode(slotIndex)?.GetVisualElement();
                        builder.Append(visualElement?.GetType().Name ?? "[INVALID DATA INDEX]");
                    }
                    catch (Exception)
                    {
                        builder.Append("[INVALID DATA INDEX]");
                    }

                    break;
                case ComposeGroupType.Movable:
                    builder.Append("\t\t");
                    var key = slotIndex > 0 && slotIndex < slots.Count ? slots[slotIndex] : null;
                    builder.Append(key ?? "[INVALID DATA INDEX]");
                    break;
            }
        }

        return builder.ToString();
    }

    public static int AncestorsCount(this ComposeGroup group, Anchors anchors, Groups groups)
    {
        var count = 0;
        var parentIndex = group.SafeParentIndex(anchors, groups);
        if (parentIndex == -2)
            return -2;
        var i = 0;
        while (parentIndex >= 0 && i++ < 100)
        {
            count++;
            if (parentIndex >= groups.Count)
                return 100;
            parentIndex = groups[parentIndex].SafeParentIndex(anchors, groups);
            if (parentIndex == -2)
                return -2;
        }

        return count;
    }
}