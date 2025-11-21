using System;
using System.Collections.Generic;
using System.Linq;
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

    private ComposeGroup ParentGroup => _groups[_parentGroupIndex];

    public void StartGroup(int key)
    {
        var currentGroup = _currentGroupIndex < _groups.Count
            ? _groups[_currentGroupIndex]
            : Optional.Empty<ComposeGroup>();
        if (currentGroup.HasValue && currentGroup.Value.Key == key)
        {
            // var currentState = _slots[_currentSlotIndex + SlotTable.StateSlotOffset] as ComposeGroupState<TState>;
            // if (currentState != null && EqualityUtils.FastEquals(currentState.Value, state))
            // {
            //     _currentElementIndex += currentGroup.Value.ElementsCount;
            //     _currentSlotIndex += currentGroup.Value.SlotsSize;
            // }
            //
            // if (currentState == null)
            // {
            //     // _slots[_currentSlotIndex + SlotTable.StateSlotOffset]; // Dispose
            //     currentState = new ComposeGroupState<TState>(state);
            //     _slots[_currentSlotIndex + SlotTable.StateSlotOffset] = currentState;
            // }
            // currentState.Value = state;
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
        _slots.Insert(_currentSlotIndex + SlotTable.MetadataOffset, new ComposeGroupData(this));
        ShiftParentIndices(1);
        ShiftSlotIndices(_currentGroupIndex + 1, SlotTable.GroupDataSlots);
        EnterGroup();
    }

    public void EndGroup(Action restart)
    {
        var parentGroup = _groups[_parentGroupIndex];
        
        var data = GetData();
        data.RestartScope.GroupIndex = _parentGroupIndex;
        data.RestartScope.Restart = restart;
        
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
            parentGroup = ParentGroup;
        }

        var oldSlotsCount = parentGroup.SlotsSize;
        var newSlotsCount = _currentSlotIndex - parentGroup.SlotIndex;
        if (newSlotsCount != oldSlotsCount)
        {
            _groups[_parentGroupIndex] = parentGroup with { SlotsSize = newSlotsCount };
            parentGroup = ParentGroup;
        }

        if (parentGroup.HasElement(_slots))
        {
            _currentElementIndex = parentGroup.ElementIndex + 1;
        }
        
        var newElementsCount = _currentElementIndex - parentGroup.ElementIndex;
        if (newElementsCount != parentGroup.ElementsCount)
        {
            _groups[_parentGroupIndex] = parentGroup with { ElementsCount = newElementsCount };
            parentGroup = ParentGroup;
        }

        // Write(SlotTable.RestartCallbackSlotOffset, restart);
        if (GetData().CompositionLocalMap != null)
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
        var currentGroup = ParentGroup;
        return currentGroup.ElementIndex;
    }

    public TVisualElement? ReadVisualElement<TVisualElement>() where TVisualElement : VisualElement =>
        GetData().Element as TVisualElement;

    public void WriteVisualElement<TVisualElement>(TVisualElement element) where TVisualElement : VisualElement =>
        GetData().Element = element;

    public void ResetElementIndex()
    {
        _currentElementIndex = 0;
    }

    public void WriteCompositionLocal(
        IImmutableStableList<CompositionLocalProvides> provides
    )
    {
        var metadata = GetData();
        var compositionLocalMap = metadata.CompositionLocalMap;
        if (compositionLocalMap == null)
        {
            var parent = _compositionLocalMaps.IsNotEmpty() ? _compositionLocalMaps.Peek() : null;
            compositionLocalMap = new CompositionLocalMap(
                parent, provides
            );
            metadata.CompositionLocalMap = compositionLocalMap;
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

    public ComposeGroupRestartScope GetRestartScope()
    {
        var currentGroup = ParentGroup;
        return _slots[currentGroup.SlotIndex].NotNull().CastTo<ComposeGroupData>().RestartScope;
    }

    private ComposeGroupData GetData()
    {
        var currentGroup = _groups[_parentGroupIndex];
        var value = _slots[currentGroup.SlotIndex + SlotTable.MetadataOffset];
        return (ComposeGroupData)value.NotNull();
    }

    public void IncrementSlotIndex()
    {
        _currentSlotIndex++;
    }

    private void EnterGroup()
    {
        _parentGroupIndex = _currentGroupIndex;
        _currentSlotIndex = _groups[_currentGroupIndex].SlotIndex + SlotTable.GroupDataSlots;
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
        for (var i = startIndex + 1; i < _groups.Count; i += SlotTable.GroupSize)
        {
            var group = _groups[i];
            if (group.ParentIndex >= startIndex)
                _groups[i] = group with { ParentIndex = group.ParentIndex + offset };
        }
    }

    private void ShiftSlotIndices(int startIndex, int offset)
    {
        for (var i = startIndex; i < _groups.Count; i += SlotTable.GroupSize)
        {
            var group = _groups[i];
            _groups[i] = group with { SlotIndex = group.SlotIndex + offset };
        }
    }
}

internal static class CastToExtensions
{
    public static T CastTo<T>(this object value)
    {
        return value is T obj ? obj : throw new InvalidCastException($"{value} is not a {typeof (T).GetReadableName()}");
    }
    
    public static string GetReadableName(this Type type, bool includeNamespace = false)
    {
        if (type.IsGenericType)
        {
            return GetGenericName(type, includeNamespace);
        }

        // Handle arrays
        if (type.IsArray)
        {
            return type.GetElementType()!.GetReadableName(includeNamespace) + "[]";
        }

        // Non-generic type
        return includeNamespace ? type.FullName ?? type.Name : type.Name;
    }

    private static string GetGenericName(Type type, bool includeNamespace)
    {
        string name = includeNamespace
            ? type.Namespace + "." + StripArity(type.Name)
            : StripArity(type.Name);

        Type[] args = type.GetGenericArguments();

        string argsJoined = string.Join(", ", args.Select(t => t.GetReadableName(includeNamespace)));

        return $"{name}<{argsJoined}>";
    }

    private static string StripArity(string name)
    {
        int index = name.IndexOf('`');
        return index < 0 ? name : name.Substring(0, index);
    }
}