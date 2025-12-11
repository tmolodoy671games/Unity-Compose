using System;
using System.Collections.Generic;
using System.Text;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct Slots
{
    public const int ReusableGroupHeaderSize = 1;
    public const int ReplaceGroupHeaderSize = 0;

    private const int VisualElementOffset = 0;

    private readonly List<object?> _slots;

    public Slots(List<object?> slots)
    {
        _slots = slots;
    }

    public int Count => _slots.Count;

    public object? this[int index]
    {
        get
        {
            return _slots[index];
        }
        set
        {
            _slots[index] = value;
        }
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

    public Optional<T> GetStruct<T>(int index) where T : struct
    {
        if (index < 0 || index >= Count)
            return Optional.Empty<T>();
        var slot = _slots[index];
        if (slot is MutableSlotEntry<T> mutableSlotEntry)
            return mutableSlotEntry.Value;
        return Optional.Empty<T>();
    }

    public void SetAsStruct<T>(int index, T value) where T : struct
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

    #region VisualElement

    public VisualElement? GetVisualElement(int index)
    {
        return _slots[index + VisualElementOffset] as VisualElement;
    }

    public void SetVisualElement(int index, VisualElement visualElement)
    {
        _slots[index + VisualElementOffset] = visualElement;
    }

    public void InsertVisualElement(int index)
    {
        _slots.Insert(index + VisualElementOffset, ComposeEmptySlot.Instance);
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

internal static class RestartGroup
{
    public const int MetadataSize = 2;
    public const int PreviousStateOffset = 0;
    public const int RestartScopeOffset = 1;
}

internal static class PreviousStateSlotsExtensions
{
    public static Optional<T> GetPreviousState<T>(this Slots slots, int dataIndex)
    {
        return slots.GetAsOptional<T>(dataIndex + RestartGroup.PreviousStateOffset);
    }

    public static void SetPreviousState<T>(this Slots slots, int dataIndex, T previousState)
    {
        slots[dataIndex + RestartGroup.PreviousStateOffset] = previousState;
    }

    public static void InsertPreviousState(this Slots slots, int dataIndex)
    {
        slots.Insert(dataIndex + RestartGroup.PreviousStateOffset, ComposeEmptySlot.Instance);
    }
}

internal static class RestartScopeSlotsExtensions
{
    public static ComposeRestartScope? GetRestartScope(this Slots slots, int dataIndex)
    {
        return slots.Get<ComposeRestartScope>(dataIndex + RestartGroup.RestartScopeOffset);
    }

    public static void SetRestartScope(this Slots slots, int dataIndex, ComposeRestartScope? restartScope)
    {
        slots[dataIndex + RestartGroup.RestartScopeOffset] = restartScope;
    }

    public static void InsertRestartScope(this Slots slots, int dataIndex)
    {
        slots.Insert(dataIndex + RestartGroup.RestartScopeOffset, ComposeEmptySlot.Instance);
    }
}

internal static class LocalGroup
{
    public const int MetadataSize = 1;
    public const int CompositionLocalMapOffset = 0;
}

internal static class LocalGroupSlotsExtensions
{
    public static Dictionary<ICompositionLocal, IMutableState<object?>>? GetCompositionLocalMap(
        this Slots slots,
        int index
    )
    {
        return slots[index + LocalGroup.CompositionLocalMapOffset] as
            Dictionary<ICompositionLocal, IMutableState<object?>>;
    }

    public static void SetCompositionLocalMap(
        this Slots slots,
        int index,
        Dictionary<ICompositionLocal, IMutableState<object?>>? map
    )
    {
        slots[index + LocalGroup.CompositionLocalMapOffset] = map;
    }

    public static void InsertCompositionLocalMap(this Slots slots, int index)
    {
        slots.Insert(index + LocalGroup.CompositionLocalMapOffset, ComposeEmptySlot.Instance);
    }
}