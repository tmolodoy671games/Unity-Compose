using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal readonly record struct ComposeGroup(
    int Key,
    int ParentIndex,
    int Size,
    int SlotIndex,
    int SlotsSize,
    int ElementIndex,
    int ElementsCount
)
{
    public override string ToString()
    {
        var builder = new StringBuilder("(");
        builder.Append($"Key: {Key}, ");
        builder.Append($"ParentIndex: {ParentIndex}, ");
        builder.Append($"Size: {Size}, ");
        builder.Append($"Slots: [{SlotIndex}, {SlotsSize}], ");
        builder.Append($"Elements: [{ElementIndex}, {ElementsCount}]");
        // builder.Append($"ParentElementIndex: {ParentElementIndex}");
        builder.Append(")");
        return builder.ToString();
    }
}

internal static class GroupExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VisualElement? ElementOrNull(this ComposeGroup group, IList<object?> slots)
    {
        return slots[group.SlotIndex + SlotIndex.DataOffset].NotNull().CastToOrNull<ComposeGroupData>()?.Element;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IRememberedValue? RememberedValueOrNull(this ComposeGroup group, IList<object?> slots)
    {
        return slots[group.SlotIndex + SlotIndex.DataOffset].NotNull().CastToOrNull<IRememberedValue>();
    }
}

internal static partial class GroupsFormattingExtensions
{
    public static string Format(
        this IList<ComposeGroup> groups,
        IList<object?> slots,
        int currentGroupIndex,
        int parentGroupIndex
    )
    {
        var builder = new StringBuilder();
        if (currentGroupIndex < 0)
            builder.AppendLine("< CURRENT_GROUP_INDEX");
        if (parentGroupIndex < 0)
            builder.AppendLine("< PARENT_GROUP_INDEX");
        for (var i = 0; i < groups.Count; i++)
        {
            var group = groups[i];
            var indent = "*".Multiply(group.ParentsCount(groups));

            builder.Append($"[{i}] ");
            builder.Append(indent);
            builder.Append(group);
            var element = group.ElementOrNull(slots);
            var rememberedValue = group.RememberedValueOrNull(slots);
            if (element != null)
                builder.Append($", Element = {element.Format()}");
            if (rememberedValue != null)
                builder.Append($", RememberedValue = {rememberedValue}");
            if (currentGroupIndex == i)
                builder.Append(" < CURRENT_GROUP_INDEX");
            if (parentGroupIndex == i)
                builder.Append(" < PARENT_GROUP_INDEX");
            builder.AppendLine();
        }
        if (currentGroupIndex >= groups.Count)
            builder.Append("< CURRENT_GROUP_INDEX");
        if (parentGroupIndex >= groups.Count)
            builder.Append("< PARENT_GROUP_INDEX");

        return builder.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasElement(this ComposeGroup group, IList<object?> slots)
    {
        return ((ComposeGroupData)slots[group.SlotIndex + GroupIndex.MetadataOffset]!).Element != null;
    }

    private static int ParentsCount(this ComposeGroup group, IList<ComposeGroup> groups)
    {
        var count = 0;
        var currentGroup = group;
        while (currentGroup.ParentIndex >= 0)
        {
            count++;
            currentGroup = groups[currentGroup.ParentIndex];
        }

        return count;
    }

    private static string Multiply(this string str, int times)
    {
        var builder = new StringBuilder(str.Length * times);
        for (var i = 0; i < times; i++)
            builder.Append(str);
        return builder.ToString();
    }
}