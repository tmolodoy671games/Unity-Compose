using System.Collections.Generic;
using System.Text;
using Packages.UnityCompose.Impl.SlotTableModels.Models;

namespace Packages.UnityCompose.Impl.SlotTableWriting.Wrappers;

internal readonly struct Slots
{
    private const int RestartScopeOffset = 0;
    
    private readonly List<object?> _slots;

    public Slots(List<object?> slots)
    {
        _slots = slots;
    }

    public object? this[int index] => _slots[index];
    
    public T Get<T>(int index) => (T)_slots[index]!;

    public IScopeUpdateScope GetRestartScope(int dataIndex)
    {
        return Get<IScopeUpdateScope>(dataIndex + RestartScopeOffset);
    }

    public override string ToString()
    {
        return ToString(-100);
    }

    public string ToString(int currentAnchorIndex)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < _slots.Count; i++)
        {
            builder.Append($"[{i}] ");
            builder.Append(_slots[i]);
            if (i == currentAnchorIndex)
                builder.Append(" < CURRENT_ANCHOR_INDEX");
            builder.AppendLine();
        }
        return builder.ToString();
    }
}