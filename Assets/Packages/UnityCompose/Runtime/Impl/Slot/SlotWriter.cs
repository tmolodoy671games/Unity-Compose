using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal class SlotWriter
{
    private readonly List<ComposeGroup> _groups;
    private readonly List<object?> _slots;
    private readonly Stack<CompositionLocalMap> _compositionLocalMaps = new();

    private int _parentGroupIndex = -1;
    private int _currentGroupIndex;

    private int _currentElementIndex;

    private int _currentSlotIndex = 0;

    public SlotWriter(SlotTable table)
    {
        _groups = table.Groups;
        _slots = table.Slots;
        _currentGroupIndex = 0;
    }

    private ComposeGroup CurrentGroup => _groups[_parentGroupIndex];

    public void StartGroup<TState>(int key, TState state)
    {
        var currentGroup = _currentGroupIndex < _groups.Count
            ? _groups[_currentGroupIndex]
            : Optional.Empty<ComposeGroup>();
        if (currentGroup.HasValue && currentGroup.Value.Key == key)
        {
            var currentState = _slots[_currentSlotIndex + SlotTable.StateSlotOffset] as ComposeGroupState<TState>;
            if (currentState != null && EqualityUtils.FastEquals(currentState.Value, state))
            {
                _currentElementIndex += currentGroup.Value.ElementsCount;
                _currentSlotIndex += currentGroup.Value.SlotsSize;
            }

            if (currentState == null)
            {
                // _slots[_currentSlotIndex + SlotTable.StateSlotOffset]; // Dispose
                currentState = new ComposeGroupState<TState>(state);
                _slots[_currentSlotIndex + SlotTable.StateSlotOffset] = currentState;
            }
            currentState.Value = state;
            EnterGroup();
            return;
        }

        if (currentGroup.HasValue && currentGroup.Value.Key != key)
            RemoveGroup();

        // Write new group
        var newGroup = new ComposeGroup(
            Key: key,
            ParentIndex: _parentGroupIndex,
            Size: 1,
            SlotIndex: _currentSlotIndex,
            SlotsSize: 0,
            ElementIndex: _currentElementIndex,
            ElementsCount: 0
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.Insert(_currentSlotIndex + SlotTable.ObjectKeySlotOffset, null);
        _slots.Insert(_currentSlotIndex + SlotTable.StateSlotOffset, new ComposeGroupState<TState>(state));
        _slots.Insert(_currentSlotIndex + SlotTable.RestartCallbackSlotOffset, null);
        _slots.Insert(_currentSlotIndex + SlotTable.CompositionLocalSlotOffset, null);
        _slots.Insert(_currentSlotIndex + SlotTable.ElementSlotOffset, null);
        ShiftParentIndices(1);
        ShiftSlotIndices(_currentGroupIndex + 1, SlotTable.GroupMetadataSlots);
        EnterGroup();
    }

    public void EndGroup(Action restart)
    {
        var parentGroup = _groups[_parentGroupIndex];
        var oldSize = parentGroup.Size;
        var newSize = _currentGroupIndex - _parentGroupIndex;
        if (newSize != parentGroup.Size)
        {
            // Removing unused groups and slots
            if (newSize < oldSize)
            {
                var firstRemovedGroup = _groups[_parentGroupIndex + 1];
                var lastRemovedGroup = _groups[_parentGroupIndex + 1 + (oldSize - newSize - 1)];
                var removedGroupsCount = oldSize - newSize;
                _groups.RemoveRange(_parentGroupIndex + newSize + 1, removedGroupsCount);
                var removeCount = lastRemovedGroup.SlotIndex + lastRemovedGroup.SlotsSize - firstRemovedGroup.SlotIndex;
                _slots.RemoveRange(firstRemovedGroup.SlotIndex, removeCount);
                
                ShiftSlotIndices(_currentSlotIndex + 1, -removeCount);
                ShiftParentIndices(-removedGroupsCount);
            }

            _groups[_parentGroupIndex] = parentGroup with { Size = newSize };
        }

        var oldSlotsCount = parentGroup.SlotsSize;
        var newSlotsCount = _currentSlotIndex - parentGroup.SlotIndex;
        if (newSlotsCount != oldSlotsCount)
        {
            _groups[_parentGroupIndex] = parentGroup with { SlotsSize = newSlotsCount };
        }

        var newElementsCount = _currentElementIndex - parentGroup.ElementIndex;
        if (newElementsCount != parentGroup.ElementsCount)
        {
            _groups[_parentGroupIndex] = parentGroup with { ElementsCount = newElementsCount };
        }

        if (parentGroup.HasElement(_slots))
        {
            _currentElementIndex = parentGroup.ElementIndex + 1;
        }

        Write(SlotTable.RestartCallbackSlotOffset, restart);
        if (Read<CompositionLocalMap>(SlotTable.CompositionLocalSlotOffset) != null)
            _compositionLocalMaps.Pop();
        _parentGroupIndex = parentGroup.ParentIndex;
    }

    public void ResetTo(int groupIndex)
    {
        _currentGroupIndex = groupIndex;
        var group = _groups[_currentGroupIndex];
        _parentGroupIndex = group.ParentIndex;
        _currentSlotIndex = group.SlotIndex;
        _currentElementIndex = group.ElementIndex;
    }

    public RememberedValue<TKey, TValue>? Read<TKey, TValue>()
    {
        var currentGroup = _groups[_parentGroupIndex];
        var maxIndex = currentGroup.SlotIndex + currentGroup.SlotsSize - 1;
        if (_currentSlotIndex < currentGroup.SlotIndex || _currentSlotIndex > maxIndex)
            return null;
        var existingValue = _slots[_currentSlotIndex];
        return existingValue as RememberedValue<TKey, TValue>;
    }

    public void Write<TKey, TValue>(TKey key, TValue value)
    {
        var currentGroup = _groups[_parentGroupIndex];

        var maxIndex = currentGroup.SlotIndex + currentGroup.SlotsSize - 1;
        if (_currentSlotIndex < currentGroup.SlotIndex || _currentSlotIndex > maxIndex)
        {
            var newValue = new RememberedValue<TKey, TValue>(key, value);
            _slots.Insert(_currentSlotIndex, newValue);
            ShiftSlotIndices(_currentGroupIndex + 1, 1);
            return;
        }

        var existingValue = _slots[_currentSlotIndex]!.CastTo<RememberedValue<TKey, TValue>>();
        existingValue.Key = key;
        existingValue.Value = value;
    }

    public int GetElementIndex()
    {
        var currentGroup = CurrentGroup;
        return currentGroup.ElementIndex;
    }

    public TVisualElement? ReadVisualElement<TVisualElement>() where TVisualElement : VisualElement =>
        Read<TVisualElement>(SlotTable.ElementSlotOffset);

    public void WriteVisualElement<TVisualElement>(TVisualElement element) =>
        Write(element, SlotTable.ElementSlotOffset);

    public void ResetElementIndex()
    {
        _currentElementIndex = 0;
    }

    public void WriteCompositionLocal(
        IImmutableStableList<CompositionLocalProvides> provides
    )
    {
        var compositionLocalMap = Read<CompositionLocalMap>(SlotTable.CompositionLocalSlotOffset);
        if (compositionLocalMap == null)
        {
            var parent = _compositionLocalMaps.IsNotEmpty() ? _compositionLocalMaps.Peek() : null;
            compositionLocalMap = new CompositionLocalMap(
                parent, provides
            );
            Write(compositionLocalMap, SlotTable.CompositionLocalSlotOffset);
        }

        compositionLocalMap.Update(provides);
        _compositionLocalMaps.Push(compositionLocalMap);
    }

    public T ReadCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        if (_compositionLocalMaps.IsEmpty())
            return defaultValueFactory();
        return _compositionLocalMaps.Peek().Get(compositionLocal, defaultValueFactory);
    }

    private TValue? Read<TValue>(int index)
    {
        var currentGroup = _groups[_parentGroupIndex];
        var value = _slots[currentGroup.SlotIndex + index];
        return value != null ? value.CastTo<TValue>() : default;
    }

    private void Write<TValue>(TValue value, int offset)
    {
        var currentGroup = _groups[_parentGroupIndex];
        _slots[currentGroup.SlotIndex + offset] = value;
    }

    public void IncrementSlotIndex()
    {
        _currentSlotIndex++;
    }

    private void EnterGroup()
    {
        _parentGroupIndex = _currentGroupIndex;
        _currentSlotIndex = _groups[_currentGroupIndex].SlotIndex + SlotTable.GroupMetadataSlots;
        _currentGroupIndex++;
    }

    private void RemoveGroup()
    {
        var index = _currentGroupIndex;
        var group = _groups[index];
        _groups.RemoveRange(index, group.Size);
        _slots.RemoveRange(group.SlotIndex, group.SlotsSize);
        ShiftParentIndices(-group.Size);
        ShiftSlotIndices(_currentGroupIndex, -group.SlotsSize);
    }

    private void ShiftParentIndices(int offset)
    {
        var startIndex = _currentGroupIndex;
        for (var i = startIndex + 1; i < _groups.Count; i++)
        {
            var group = _groups[i];
            if (group.ParentIndex >= startIndex)
                _groups[i] = group with { ParentIndex = group.ParentIndex + offset };
        }
    }

    private void ShiftSlotIndices(int startIndex, int offset)
    {
        for (var i = startIndex; i < _groups.Count; i++)
        {
            var group = _groups[i];
            _groups[i] = group with { SlotIndex = group.SlotIndex + offset };
        }
    }
}