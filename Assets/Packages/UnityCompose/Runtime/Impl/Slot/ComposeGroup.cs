using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;

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

internal static partial class GroupsExtensions
{
    public static string Format(this List<ComposeGroup> groups, List<object?> slots)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < groups.Count; i++)
        {
            var group = groups[i];
            var indent = "*".Multiply(group.ParentsCount(groups));

            builder.Append($"[{i}] ");
            var hasElement = slots[group.SlotIndex + SlotTable.MetadataOffset].NotNull().CastTo<ComposeGroupData>()
                .Element?.Format();
            builder.AppendLine(indent + group + $" Element = {hasElement}");
        }

        return builder.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasElement(this ComposeGroup group, List<object?> slots)
    {
        return ((ComposeGroupData) slots[group.SlotIndex + SlotTable.MetadataOffset]!).Element != null;
    }

    private static int ParentsCount(this ComposeGroup group, List<ComposeGroup> groups)
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