using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Models;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct Slots
{
    public const int RestartGroupHeaderSize = 2;
    public const int ReusableGroupHeaderSize = 1;
    public const int ReplaceGroupHeaderSize = 0;

    private const int VisualElementOffset = 0;

    private const int PreviousStateOffset = 0;
    private const int RestartScopeOffset = 1;
    private const int CompositionLocalOffset = 2;

    private readonly List<object?> _slots;

    public Slots(List<object?> slots)
    {
        _slots = slots;
    }

    public int Count => _slots.Count;

    public object? this[int index]
    {
        get => _slots[index];
        set => _slots[index] = value;
    }

    private T Get<T>(int index) => (T)_slots[index]!;

    public Optional<T> GetAsMutableState<T>(int index)
    {
        if (index < 0 || index >= Count)
            return Optional.Empty<T>();
        var slot = _slots[index];
        if (slot is MutableSlotEntry<T> mutableSlotEntry)
            return mutableSlotEntry.Value;
        return Optional.Empty<T>();
    }

    public void SetAsMutableState<T>(int index, T value)
    {
        var slot = _slots[index];
        if (slot is MutableSlotEntry<T> mutableSlotEntry)
            mutableSlotEntry.Value = value;
        else
            _slots[index] = new MutableSlotEntry<T>(value);
    }

    public void Insert(int index, object? value)
    {
        _slots.Insert(index, value);
    }

    public void InsertAsMutableState<T>(int index, T value)
    {
        _slots.Insert(index, new MutableSlotEntry<T>(value));
    }

    #region PreviousState

    public Optional<T> GetPreviousState<T>(int dataIndex)
    {
        return GetAsMutableState<T>(dataIndex + PreviousStateOffset);
    }

    public void SetPreviousState<T>(int dataIndex, T previousState)
    {
        SetAsMutableState(dataIndex + PreviousStateOffset, previousState);
    }

    public void InsertPreviousState<T>(int dataIndex, T previousState)
    {
        InsertAsMutableState(dataIndex + PreviousStateOffset, previousState);
    }

    #endregion

    #region Restart Scope

    public ComposeRestartScope? GetRestartScope(int dataIndex)
    {
        var index = dataIndex + RestartScopeOffset;
        if (index < 0 || index >= Count)
            return null;
        return _slots[index] as ComposeRestartScope;
    }

    public void InsertRestartScope(int dataIndex, ComposeRestartScope? restartScope)
    {
        _slots.Insert(dataIndex + RestartScopeOffset, restartScope);
    }

    public void SetRestartScope(int dataIndex, ComposeRestartScope? restartScope)
    {
        _slots[dataIndex + RestartScopeOffset] = restartScope;
    }

    #endregion

    #region CompositionLocal

    public CompositionLocalMap GetCompositionLocalMap(int index)
    {
        return (_slots[index + CompositionLocalOffset] as CompositionLocalMap).NotNull();
    }

    public void SetCompositionLocalMap(int index, CompositionLocalMap? map)
    {
        _slots[index + CompositionLocalOffset] = map;
    }

    public void InsertCompositionLocalMap(int index, CompositionLocalMap? map)
    {
        _slots.Insert(index + CompositionLocalOffset, map);
    }

    #endregion

    #region VisualElement

    public VisualElement? GetVisualElement(int index)
    {
        return _slots[index + VisualElementOffset] as VisualElement;
    }

    public void SetVisualElement(int index, VisualElement visualElement)
    {
        _slots[index + VisualElementOffset] = visualElement;
    }

    public void InsertVisualElement(int index, VisualElement? visualElement)
    {
        _slots.Insert(index + VisualElementOffset, visualElement);
    }

    #endregion

    public void Clear() => _slots.Clear();

    public override string ToString()
    {
        return ToString(-100);
    }

    public string ToString(int currentAnchorIndex)
    {
        var builder = new StringBuilder();
        if (currentAnchorIndex == -1)
            builder.AppendLine("< CURRENT_ANCHOR_INDEX");
        for (var i = 0; i < _slots.Count; i++)
        {
            builder.Append($"[{i}] ");
            builder.Append(_slots[i]);
            if (i == currentAnchorIndex)
                builder.Append(" < CURRENT_ANCHOR_INDEX");
            builder.AppendLine();
        }

        if (currentAnchorIndex == _slots.Count)
            builder.AppendLine("< CURRENT_ANCHOR_INDEX");

        return builder.ToString();
    }
}