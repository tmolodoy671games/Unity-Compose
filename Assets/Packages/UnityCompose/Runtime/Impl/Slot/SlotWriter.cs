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
    private readonly SlotTable _table;
    private readonly List<ComposeGroup> _groups;
    private readonly List<object?> _slots;
    private readonly Stack<CompositionLocalMap> _compositionLocalMaps = new();
    private readonly Stack<(int GroupIndex, int StartIndex, int Count)> _skippedGroups = new();

    private int _parentGroupIndex = -1;
    private int _currentGroupIndex;
    private int _currentElementIndex;
    private int _currentSlotIndex = 0;

    public SlotWriter(SlotTable table)
    {
        _table = table;
        _groups = table.Groups;
        _slots = table.Slots;
        _currentGroupIndex = 0;
    }

    public int CurrentGroupIndex => _currentGroupIndex;
    public int ParentGroupIndex => _parentGroupIndex;
    public int CurrentSlotIndex => _currentSlotIndex;
    public bool IsInCompositionContext => _parentGroupIndex != -1;

    private ComposeGroup ParentGroup => _groups[_parentGroupIndex];

    #region Reusable Group

    public void StartReusableGroup<T>(int key, T state, VisualElement? element = null)
    {
        _currentGroupIndex = FindMatchingKeyIndex(key);

        var currentGroup = _currentGroupIndex < _groups.Count
            ? _groups[_currentGroupIndex]
            : Optional.Empty<ComposeGroup>();
        if (currentGroup.HasValue && currentGroup.Value.Key == key)
        {
            EnterReusableGroup();
            return;
        }

        // Write new group
        var newGroup = new ComposeGroup(
            Key: key,
            ParentIndex: _parentGroupIndex,
            Size: 1,
            SlotIndex: _currentSlotIndex,
            SlotsSize: 0,
            ElementIndex: _currentElementIndex,
            ElementsCount: element != null ? 1 : 0
        );
        var newData = new ComposeGroupData<T>(this, state)
        {
            Element = element
        };
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.Insert(_currentSlotIndex + GroupIndex.MetadataOffset, newData);
        ShiftParentIndices(_currentGroupIndex + 1, 1);
        ShiftSlotIndices(_currentGroupIndex + 1, SlotIndex.DataSize);
        EnterReusableGroup();
    }

    public void EndReusableGroup(Action restart)
    {
        RemoveSkippedGroups();
        var parentGroup = _groups[_parentGroupIndex];

        var data = GetData();
        data.RestartScope.GroupIndex = _parentGroupIndex;
        data.RestartScope.Restart = restart;

        var newSize = _currentGroupIndex - _parentGroupIndex;

        if (parentGroup.HasElement(_slots))
            _currentElementIndex = parentGroup.ElementIndex + 1;

        var newSlotsCount = _currentSlotIndex - parentGroup.SlotIndex;
        var newElementsCount = _currentElementIndex - parentGroup.ElementIndex;
        var anyFieldChanged = newElementsCount != parentGroup.ElementsCount ||
                              newSlotsCount != parentGroup.SlotsSize ||
                              newSize != parentGroup.Size;
        if (anyFieldChanged)
        {
            parentGroup = parentGroup with
            {
                ElementsCount = newElementsCount,
                SlotsSize = newSlotsCount,
                Size = newSize
            };
            if (parentGroup.Key == 0)
                Debug.Log(newSize);
            _groups[_parentGroupIndex] = parentGroup;
        }

        _parentGroupIndex = parentGroup.ParentIndex;
    }

    private void EnterReusableGroup()
    {
        _parentGroupIndex = _currentGroupIndex;
        _currentSlotIndex += SlotIndex.DataSize;
        _currentGroupIndex++;
    }

    #endregion

    #region Replaceable Group

    public void StartReplaceableGroup<TKey, TValue>(int key)
    {
        _currentGroupIndex = FindMatchingKeyIndex(key);

        var currentGroup = _currentGroupIndex < _groups.Count
            ? _groups[_currentGroupIndex]
            : Optional.Empty<ComposeGroup>();
        if (currentGroup.HasValue && currentGroup.Value.Key == key)
        {
            EnterReplaceableGroup();
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            ParentIndex: _parentGroupIndex,
            Size: 1,
            SlotIndex: _currentSlotIndex,
            SlotsSize: 1,
            ElementIndex: -1,
            ElementsCount: 0
        );
        var newRememberedValue = new RememberedValue<TKey, TValue>();
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.Insert(_currentSlotIndex, newRememberedValue);
        ShiftParentIndices(_currentGroupIndex + 1, 1);
        ShiftSlotIndices(_currentGroupIndex + 1, 1);

        EnterReplaceableGroup();
    }

    private void EnterReplaceableGroup()
    {
        _parentGroupIndex = _currentGroupIndex;
        _currentGroupIndex++;
    }

    public RememberedValue<TKey, TValue>? Read<TKey, TValue>()
    {
        var existingValue = _slots[_currentSlotIndex];
        return (RememberedValue<TKey, TValue>)existingValue!;
    }

    public void Write<TKey, TValue>(TValue value)
    {
        var rememberedValue = (RememberedValue<TKey, TValue>)_slots[_currentSlotIndex]!;
        rememberedValue.Value = value;
    }

    public void EndReplaceableGroup()
    {
        var parentGroup = ParentGroup;
        _parentGroupIndex = parentGroup.ParentIndex;
        _currentSlotIndex++;
    }

    #endregion

    #region Elements

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

    #endregion

    #region CompositionLocal

    public void StartCompositionLocal(
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

    public void EndCompositionLocal()
    {
        _compositionLocalMaps.Pop();
    }

    public T ReadCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        if (_compositionLocalMaps.IsEmpty())
            return defaultValueFactory();
        return _compositionLocalMaps.Peek().Get(compositionLocal, defaultValueFactory);
    }

    #endregion

    #region Restarting

    public void ResetTo(int groupIndex)
    {
        _currentGroupIndex = groupIndex;
        var group = _groups[_currentGroupIndex];
        _parentGroupIndex = group.ParentIndex;
        _currentSlotIndex = group.SlotIndex;
        _currentElementIndex = group.ElementIndex;
    }

    public ComposeGroupRestartScope? GetRestartScope()
    {
        if (!IsInCompositionContext)
            return null;
        var currentGroup = ParentGroup;
        return _slots[currentGroup.SlotIndex].NotNull().CastTo<ComposeGroupData>().RestartScope;
    }

    #endregion

    private ComposeGroupData GetData()
    {
        var currentGroup = _groups[_parentGroupIndex];
        var value = _slots[currentGroup.SlotIndex + GroupIndex.MetadataOffset];
        return (ComposeGroupData)value.NotNull();
    }

    private void ShiftParentIndices(int startIndex, int offset)
    {
        for (var i = startIndex + 1; i < _groups.Count; i += GroupIndex.MetadataSize)
        {
            var group = _groups[i];
            if (group.ParentIndex >= startIndex)
                _groups[i] = group with { ParentIndex = group.ParentIndex + offset };
        }
    }

    private void ShiftSlotIndices(int startIndex, int offset)
    {
        for (var i = startIndex; i < _groups.Count; i += GroupIndex.MetadataSize)
        {
            var group = _groups[i];
            _groups[i] = group with { SlotIndex = group.SlotIndex + offset };
        }
    }

    private int FindMatchingKeyIndex(int key)
    {
        if (_parentGroupIndex < 0)
            return _currentGroupIndex;
        var parent = ParentGroup;
        var maxIndex = _parentGroupIndex + parent.SlotsSize - 1;

        var startRemoveIndex = _currentGroupIndex;
        var removeCount = 0;
        for (var i = _currentGroupIndex; i <= maxIndex; i++)
        {
            var group = _groups[i];
            if (group.Key == key)
            {
                if (removeCount > 0)
                    Debug.Log($"Marked {_parentGroupIndex}, {startRemoveIndex}, {removeCount} for deletion");
                return i;
            }

            removeCount++;
        }

        return _currentGroupIndex;
    }

    private void RemoveSkippedGroups()
    {
        if (_skippedGroups.IsEmpty())
            return;
        while (_skippedGroups.IsNotEmpty())
        {
            var range = _skippedGroups.Peek();
            if (range.GroupIndex != _parentGroupIndex)
                return;
            _skippedGroups.Pop();
            var startGroup = _groups[range.StartIndex];
            var endGroup = _groups[range.StartIndex + range.Count - 1];
            var startSlotIndex = startGroup.SlotIndex;
            var slotsCount = endGroup.SlotIndex + endGroup.SlotsSize - 1 - startSlotIndex;
            if (slotsCount > 0)
            {
                _slots.RemoveRange(startSlotIndex, slotsCount);
                ShiftSlotIndices(startSlotIndex, slotsCount);
            }

            _groups.RemoveRange(range.StartIndex, range.Count);
            ShiftParentIndices(range.StartIndex, range.Count);
        }
    }
}

internal static class CastToExtensions
{
    public static T CastTo<T>(this object value)
    {
        return value is T obj ? obj : throw new InvalidCastException($"{value} is not a {typeof(T).GetReadableName()}");
    }

    private static string GetReadableName(this Type type, bool includeNamespace = false)
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
        var name = includeNamespace
            ? type.Namespace + "." + StripArity(type.Name)
            : StripArity(type.Name);

        var args = type.GetGenericArguments();

        var argsJoined = string.Join(", ", args.Select(t => t.GetReadableName(includeNamespace)));

        return $"{name}<{argsJoined}>";
    }

    private static string StripArity(string name)
    {
        int index = name.IndexOf('`');
        return index < 0 ? name : name.Substring(0, index);
    }
}