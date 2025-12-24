using System;
using System.Collections.Generic;
using System.Text;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct Slots
{
    private readonly GapBufferList<object?> _slots;
    private readonly List<object?> _buffer = new(0);

    public Slots(GapBufferList<object?> slots)
    {
        _slots = slots;
    }

    public int Count => _slots.Count;
    public int GapStart => _slots.GapStart;
    public int GapLength => _slots.GapLength;

    public object? this[int index]
    {
        get => _slots[index];
        set => _slots[index] = value;
    }

    public void RemoveRange(int index, int count) => _slots.RemoveRange(index, count);

    public T? Get<T>(int index)
    {
        var item = _slots[index];
        if (item is T slot)
            return slot;
        return default;
    }

    public Optional<T> GetAsOptional<T>(int index)
    {
        var item = _slots[index];
        if (item == ComposeEmptySlot.Instance)
            return Optional.Empty<T>();
        if (item is T slot)
            return slot;
        return default!;
    }

    public Optional<T> GetAsStruct<T>(int index)
    {
        if (index < 0 || index >= Count)
            return Optional.Empty<T>();
        var slot = _slots[index];
        if (slot is MutableSlotEntry<T> mutableSlotEntry)
            return mutableSlotEntry.Value;
        return Optional.Empty<T>();
    }

    public void SetAsStruct<T>(int index, T value)
    {
        var slot = _slots[index];
        if (slot is MutableSlotEntry<T> mutableSlotEntry)
            mutableSlotEntry.Value = value;
        else
            _slots[index] = MutableSlotEntry.Get(value);
    }

    public void Insert(int index, object? value)
    {
        _slots.Insert(index, value);
    }

    public void InsertAsStruct<T>(int index, T value)
    {
        _slots.Insert(index, MutableSlotEntry.Get(value));
    }

    public void Clear() => _slots.Clear();
    
    public void Move(int startIndex, int targetIndex, int count)
    {
        _slots.Move(_buffer, startIndex, targetIndex, count);
    }

    public int LogicalToAbsoluteIndex(int index) => _slots.LogicalToAbsoluteIndex(index);
    public int AbsoluteToLogicalIndex(int index) => _slots.AbsoluteToLogicalIndex(index);
    
    public void AddItemsShiftObserver(Action<ItemsShiftEvent> onItemsShift) =>
        _slots.AddItemsShiftObserver(onItemsShift);

    public override string ToString()
    {
        return ToString(-100);
    }

    public string ToString(int currentAnchorIndex)
    {
        var builder = new StringBuilder();
        if (currentAnchorIndex == -1)
            builder.AppendLine("< CURRENT_SLOT_INDEX");
        for (var i = 0; i < _slots.Count; i++)
        {
            builder.Append($"[{i}] ");
            builder.Append(Format(_slots[i]));
            if (i == currentAnchorIndex)
                builder.Append(" < CURRENT_SLOT_INDEX");
            builder.AppendLine();
        }

        if (currentAnchorIndex == _slots.Count)
            builder.AppendLine("< CURRENT_SLOT_INDEX");

        return builder.ToString();
    }

    private static string Format(object? value)
    {
        if (value == null)
            return "Null";
        if (value is Delegate)
            return $"Lambda";
        return value.ToString();
    }
}