using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct Groups
{
    public const int GroupHeaderSize = 1;
    
    private readonly List<ComposeGroup> _groups;

    public Groups(List<ComposeGroup> groups)
    {
        _groups = groups;
    }

    public ComposeGroup this[int index]
    {
        get => _groups[index];
        set => _groups[index] = value;
    } 
    public int Count => _groups.Count;
    
    public void Insert(int index, ComposeGroup group) => _groups.Insert(index, group);
    
    public void Clear() => _groups.Clear();

    public string ToString(int currentParentIndex, int currentGroupIndex, Anchors groupsAnchors, Anchors slotsAnchors)
    {
        var builder = new StringBuilder();
        if (currentParentIndex == -1)
            builder.AppendLine("< CURRENT_PARENT_INDEX");
        for (var i = 0; i < _groups.Count; i++)
        {
            var group = _groups[i];
            builder.Append($"[{i}] ");
            builder.Append("-".Multiply(group.AncestorsCount(groupsAnchors, this)));
            builder.Append(group.ToString(groupsAnchors, slotsAnchors));
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
    public static int Index(this ComposeGroup group, Anchors anchors)
    {
        if (!group.AnchorId.IsValid)
            return -1;
        return anchors[group.AnchorId].Index;
    }
    
    public static int ParentIndex(this ComposeGroup group, Anchors anchors)
    {
        if (!group.ParentAnchorId.IsValid)
            return -1;
        return anchors[group.ParentAnchorId].Index;
    }

    public static int SlotIndex(this ComposeGroup group, Anchors anchors)
    {
        if (!group.DataAnchorId.IsValid)
            return -1;
        return anchors[group.DataAnchorId].Index;
    }
    
    public static string ToString(this ComposeGroup group, Anchors groupsAnchors, Anchors slotsAnchors)
    {
        var builder = new StringBuilder();
        builder.Append(
            group.Type switch
            {
                ComposeGroupType.Replace => "ReplaceGroup",
                ComposeGroupType.Restart => "RestartGroup",
                ComposeGroupType.Reusable => "ReusableGroup",
                _ => throw new ArgumentOutOfRangeException()
            }
        );
        builder.Append("(");
        builder.Append($"Key: {group.Key}");
        builder.Append($", Size: {group.Size}");
        builder.Append($", SlotsSize: {group.SlotsSize}");
        builder.Append($", Index: {group.Index(groupsAnchors)}");
        builder.Append($", ParentIndex: {group.ParentIndex(groupsAnchors)}");
        builder.Append($", DataIndex: {group.SlotIndex(slotsAnchors)}");
        builder.Append(")");
        return builder.ToString();
    }
    
    public static int AncestorsCount(this ComposeGroup group, Anchors anchors, Groups groups)
    {
        var count = 0;
        var parentIndex = group.ParentIndex(anchors);
        var i = 0;
        while (parentIndex >= 0 && i++ < 100)
        {
            count++;
            parentIndex = groups[parentIndex].ParentIndex(anchors);
        }

        return count;
    }
}