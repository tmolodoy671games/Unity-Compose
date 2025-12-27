using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct Groups
{
    public const int GroupHeaderSize = 1;

    private readonly GapBufferList<ComposeGroup> _groups;
    private readonly List<ComposeGroup> _buffer = new(0);

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

    public void MoveGapAt(int index) => _groups.MoveGapAt(index);

    public void Insert(int index, ComposeGroup group) => _groups.Insert(index, group);

    public void RemoveRange(int index, int count) => _groups.RemoveRange(index, count);

    public void Move(int startIndex, int targetIndex, int count)
    {
        _groups.Move(_buffer, startIndex, targetIndex, count);
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
            builder.Append("-".Multiply(group.AncestorsCount(groupsAnchors, this)));
            builder.Append(group.ToString(groupsAnchors, slotsAnchors, slots, this));
            var isSelfIndexInvalid = group.AnchorId.IsValid &&
                                     (!groupsAnchors.ContainsIndex(group.AnchorId) ||
                                      group.Index(groupsAnchors, this) != i);
            if (isSelfIndexInvalid)
                builder.Append(" [SELF ANCHOR IS INVALID]");
            var isDataIndexInvalid = group.DataAnchorId.IsValid &&
                                     !slotsAnchors.ContainsIndex(group.DataAnchorId);
            if (isDataIndexInvalid)
                builder.Append(" [DATA ANCHOR IS INVALID]");
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
        builder.Append($", ParentIndex: {group.ParentIndex(groupsAnchors, groups)}");
        builder.Append($", Index: {group.Index(groupsAnchors, groups)}");
        builder.Append($", Size: {group.Size}");
        builder.Append($", DataIndex: {group.SlotIndex(slotsAnchors, slots)}");
        builder.Append($", SlotsSize: {group.SlotsSize}");
        builder.Append($", ElementIndex: {group.ElementIndex}");
        builder.Append($", ElementsCount: {group.ElementsCount}");
        builder.Append(")");
        if (group.Type == ComposeGroupType.Key && group.DataAnchorId.IsValid)
        {
            // builder.Append($", Absolute Index: {groupsAnchors[group.AnchorId].Location}");
            // var slotIndex = slots.AbsoluteToLogicalIndex(slotsAnchors[group.DataAnchorId].Location);
            // builder.Append($", Data: {slots[slotIndex]}");
        }

        return builder.ToString();
    }

    public static int AncestorsCount(this ComposeGroup group, Anchors anchors, Groups groups)
    {
        var count = 0;
        var parentIndex = group.ParentIndex(anchors, groups);
        var i = 0;
        while (parentIndex >= 0 && i++ < 100)
        {
            count++;
            if (parentIndex >= groups.Count)
                return 100;
            parentIndex = groups[parentIndex].ParentIndex(anchors, groups);
        }

        return count;
    }
}