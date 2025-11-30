using System.Collections.Generic;
using System.Text;
using Packages.UnityCompose.Impl.SlotTableModels.Models;

namespace Packages.UnityCompose.Impl.SlotTableWriting.Wrappers;

internal readonly struct Groups
{
    private readonly List<ComposeGroup> _groups;

    public Groups(List<ComposeGroup> groups)
    {
        _groups = groups;
    }
    
    public ComposeGroup this[int index] => _groups[index];
    public int Count => _groups.Count;
    
    public void Add(ComposeGroup group) => _groups.Add(group);

    public override string ToString()
    {
        return ToString(-100, -100);
    }

    public string ToString(int currentParentIndex, int currentGroupIndex)
    {
        var builder = new StringBuilder();
        if (currentParentIndex == -1)
            builder.AppendLine("< CURRENT_PARENT_INDEX");
        for (var i = 0; i < _groups.Count; i++)
        {
            var group = _groups[i];
            builder.Append($"[{i}] ");
            builder.Append(group);
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