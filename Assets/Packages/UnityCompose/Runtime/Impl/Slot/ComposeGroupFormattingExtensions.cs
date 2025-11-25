using System.Collections.Generic;
using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal static partial class GroupsFormattingExtensions
{
    public static string Format(
        this IList<ComposeGroup> groups,
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
            var element = group.ElementOrNull();
            var rememberedValue = group.RememberedValueOrNull();
            if (group.State is IComposeGroupState.Reusable reusableState)
                builder.Append($", CurrentIndex = {reusableState.RestartScope.GroupIndex}");
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

    private static string Multiply(this string str, int times)
    {
        var builder = new StringBuilder(str.Length * times);
        for (var i = 0; i < times; i++)
            builder.Append(str);
        return builder.ToString();
    }

    private static int ParentsCount(this ComposeGroup group, IList<ComposeGroup> groups)
    {
        var parentsCount = 0;
        var currentGroup = group;
        var i = 0;
        while (currentGroup.ParentIndex >= 0 && i++ < 10)
        {
            parentsCount++;
            currentGroup = groups[currentGroup.ParentIndex];
        }
    
        return parentsCount;
    }
}