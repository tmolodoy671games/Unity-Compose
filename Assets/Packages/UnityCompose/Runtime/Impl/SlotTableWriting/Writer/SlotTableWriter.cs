// #define LOGGING
// #define ASSERTIONS
#define PARENT_ANCHORS_FOR_EVERYONE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;

// TODO: Shifting ancestors sizes on restart
// TODO: Shifting anchors on insertion/removal
internal class SlotTableWriter : ISlotTableWriter
{
    private readonly Groups _groups;
    private readonly Slots _slots;
    private readonly Anchors _groupsAnchors;
    private readonly Anchors _slotsAnchors;
    private readonly Stack<int> _enteredParentsIndices = new();
    private readonly Stack<int> _enteredParentsSlotIndices = new();
    private readonly Stack<int> _enteredElementIndices = new();
    private readonly Stack<ComposeGroupEntry> _enteredRestartGroups = new();
    private readonly Stack<ComposeGroupEntry> _enteredLocalGroups = new();

    private readonly Stack<CompositionLocalMapEntry> _enteredCompositionLocalMaps = new();
    private readonly List<IImmutableStableList<CompositionLocalProvides>> _enteredProvides = new();
    private Dictionary<ICompositionLocal, IMutableState<object?>>? _rootCompositionLocalMap = null;

    private int _currentGroupIndex = 0;
    private int _currentParentIndex = -1;
    private int _currentSlotIndex = 0;
    private int _currentParentSlotIndex = -1;
    private int _invalidationRoot = -1;
    private int _currentElementIndex = 0;
    private int _alreadyRemovedGroups = 0;
    private int _alreadyRemovedSlots = 0;

    public SlotTableWriter(SlotTable.Models.SlotTable table)
    {
        _groups = new Groups(table.Groups);
        _slots = new Slots(table.Slots);
        _groupsAnchors = new Anchors(table.GroupsAnchors);
        _slotsAnchors = new Anchors(table.SlotsAnchors);
    }

    private ComposeGroup CurrentParent() => _groups[_currentParentIndex];

    #region Restart Group

    public void StartRestartGroup(int key)
    {
#if LOGGING
        Log($"StartRestartGroup({key})");
#endif
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
#if ASSERTIONS
            if (existingGroup.Key != key)
                throw new InvalidOperationException($"Found {existingGroup.Key} instead of {key}!");
            if (existingGroup.Type != ComposeGroupType.Restart)
                throw new InvalidOperationException($"Found {existingGroup.Type} instead of RestartGroup!");
#endif
            EnsureIndex(existingGroup);
            _enteredRestartGroups.Push(
                new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex)
            );
            EnterGroup();
            _currentSlotIndex += RestartGroup.MetadataSize;
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Restart,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 1,
            SlotsSize: RestartGroup.MetadataSize,
            AnchorId: AnchorId.None,
            DataAnchorId: AnchorId.None,
            ElementIndex: _currentElementIndex,
            ElementsCount: 0
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertPreviousState(_currentSlotIndex);
        _slots.InsertRestartScope(_currentSlotIndex);
        _enteredRestartGroups.Push(
            new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex)
        );
        EnterGroup();
        _currentSlotIndex += RestartGroup.MetadataSize;
    }

    public bool IsInInvalidationRoot()
    {
        return _invalidationRoot == _currentParentIndex;
    }

    public Optional<T> GetPreviousState<T>()
    {
        return _slots.GetPreviousState<T>(_currentParentSlotIndex);
    }

    public void UpdatePreviousState<T>(T state)
    {
        _slots.SetPreviousState(_currentParentSlotIndex, state);
    }

    public void SkipToGroupEnd()
    {
#if LOGGING
        Log("SkipToGroupEnd()");
#endif
        var parent = CurrentParent();
        _currentGroupIndex += parent.Size - 1;
        _currentSlotIndex += parent.SlotsSize - RestartGroup.MetadataSize;
        _currentElementIndex += parent.ElementsCount;
    }

    public ComposeRestartScope? GetRestartScope()
    {
        if (_enteredRestartGroups.IsEmpty())
            return null;
        var scope = _slots.GetRestartScope(_enteredRestartGroups.Peek().SlotIndex);
        if (scope != null)
        {
            scope.CompositionLocalMap = RequireCompositionLocalMap();
        }

        return scope;
    }

    public ComposeRestartScope? RequireRestartScope()
    {
        var restartScope = GetRestartScope();
        if (restartScope != null)
            return restartScope;
        if (_enteredRestartGroups.IsEmpty())
            return null;
        var (enteredRestartGroupIndex, enteredRestartGroupSlotIndex) = _enteredRestartGroups.Peek();
        var restartGroup = _groups[enteredRestartGroupIndex];
        if (!restartGroup.AnchorId.IsValid)
        {
            restartGroup = restartGroup with { AnchorId = _groupsAnchors.AllocateAnchor(enteredRestartGroupIndex) };
            _groups[enteredRestartGroupIndex] = restartGroup;
        }

        if (!restartGroup.DataAnchorId.IsValid)
        {
            restartGroup = restartGroup with
            {
                DataAnchorId = _slotsAnchors.AllocateAnchor(enteredRestartGroupSlotIndex)
            };
            _groups[enteredRestartGroupIndex] = restartGroup;
        }

        restartScope = new ComposeRestartScope(restartGroup.AnchorId, this);
        _slots.SetRestartScope(enteredRestartGroupSlotIndex, restartScope);
        return restartScope;
    }

    public void EndRestartGroup(int key)
    {
#if LOGGING
        Log($"EndRestartGroup({key})");
#endif
        var parent = CurrentParent();
#if ASSERTIONS
        if (parent.Key != key)
            throw new InvalidOperationException($"Mismatching ending group key: {key} vs {parent.Key}!");
#endif
        ExitGroup(parent);
        if (parent.Key == _invalidationRoot)
            _invalidationRoot = -1;
        _enteredRestartGroups.Pop();
    }

    #endregion


    #region Replace Group

    public void StartReplaceGroup(int key)
    {
#if LOGGING
        Log($"StartReplaceGroup({key})");
#endif
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
#if ASSERTIONS
            if (existingGroup.Type != ComposeGroupType.Replace)
                throw new InvalidOperationException($"Found {existingGroup.Type} group instead of replace group!");
#endif
            if (existingGroup.Key != key)
            {
                existingGroup = existingGroup with { Key = key };
                _groups[_currentGroupIndex] = existingGroup;
            }

            EnsureIndex(existingGroup);
            EnterGroup();
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Replace,
            ParentAnchorId: GetOrAllocateParentAnchorForNonRestartGroup(),
            Size: 1,
            SlotsSize: Slots.ReplaceGroupHeaderSize,
            AnchorId: AnchorId.None,
            DataAnchorId: AnchorId.None,
            ElementIndex: _currentElementIndex,
            ElementsCount: 0
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        EnterGroup();
    }

    public void EndReplaceGroup(int key)
    {
        var parent = CurrentParent();
#if ASSERTIONS
        if (parent.Key != key)
            throw new InvalidOperationException($"Mismatching ending group key: {key} vs {parent.Key}!");
#endif
        ExitGroup(parent);
    }

    public void EndReplaceGroup()
    {
        ExitGroup(CurrentParent());
    }

    #endregion


    #region Reusable Group

    public void StartReusableGroup(int key)
    {
#if LOGGING
        Log($"StartReusableGroup({key})");
#endif
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
#if ASSERTIONS
            if (existingGroup.Key != key)
                throw new InvalidOperationException($"Found {existingGroup.Key} instead of {key}!");
            if (existingGroup.Type != ComposeGroupType.Reusable)
                throw new InvalidOperationException($"Found {existingGroup.Type} group instead of Reusable group!");
#endif
            EnsureIndex(existingGroup);
            EnterGroup();
            _currentSlotIndex += Slots.ReusableGroupHeaderSize;
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Reusable,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 1,
            SlotsSize: 1,
            AnchorId: _groupsAnchors.AllocateAnchor(_currentGroupIndex),
            DataAnchorId: _slotsAnchors.AllocateAnchor(_currentSlotIndex),
            ElementIndex: _currentElementIndex,
            ElementsCount: 1
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertVisualElement(_currentSlotIndex);
        EnterGroup();
        _currentSlotIndex += Slots.ReusableGroupHeaderSize;
    }

    public void EndReusableGroup(int key)
    {
#if LOGGING
        Log($"EndReusableGroup({key})");
#endif
        var parent = CurrentParent();
#if ASSERTIONS
        if (parent.Key != key)
            throw new InvalidOperationException($"Mismatching ending group key: {key} vs {parent.Key}!");
#endif
        ExitGroup(parent);
        _currentElementIndex = _enteredElementIndices.PeekOrDefault(0);
        _currentElementIndex++;
        _enteredElementIndices.TryPop(out _);
    }

    #endregion


    #region Remember

    public Optional<T> Read<T>()
    {
#if LOGGING
        Log($"Read<T>()");
#endif
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return _slots.GetAsOptional<T>(_currentSlotIndex);
    }

    public Optional<T> ReadAndWrite<T>(T value)
    {
        if (!IsThereAlreadyASlot())
        {
            _slots.Insert(_currentSlotIndex, value);
            _currentSlotIndex++;
            return Optional.Empty<T>();
        }

        var result = _slots.GetAsOptional<T>(_currentSlotIndex);
        _slots[_currentSlotIndex] = value;
        _currentSlotIndex++;
        return result;
    }

    public void Write<T>(T value)
    {
#if LOGGING
        Log($"Write<T>()");
#endif
        if (!IsThereAlreadyASlot())
        {
            _slots.Insert(_currentSlotIndex, value);
            _currentSlotIndex++;
            return;
        }

        _slots[_currentSlotIndex] = value;
        _currentSlotIndex++;
    }

    public void IncrementSlotIndex()
    {
#if LOGGING
        Log($"IncrementSlotIndex()");
#endif
        _currentSlotIndex++;
    }

    #endregion


    #region VisualElement

    public T? GetVisualElement<T>() where T : VisualElement
    {
        return _slots.GetVisualElement(CurrentParent().SlotIndex(_slotsAnchors)) as T;
    }

    public void WriteVisualElement(VisualElement visualElement)
    {
        // if (IsDebugVisualElementNamesEnabled)
        //     visualElement.name = CurrentParent().Key.ToString();
        _slots.SetVisualElement(CurrentParent().SlotIndex(_slotsAnchors), visualElement);
        var currentParent = CurrentParent();
        if (currentParent.ElementsCount != 1)
        {
            currentParent = currentParent with { ElementsCount = 1 };
            _groups[_currentParentIndex] = currentParent;
        }
    }

    public int GetCurrentElementIndex() => _currentElementIndex;

    public void EnterVisualElement()
    {
        _enteredElementIndices.Push(_currentElementIndex);
        _currentElementIndex = 0;
    }

    #endregion


    #region CompositionLocal

    public void StartLocalGroup(int key)
    {
#if LOGGING
        Log($"StartLocalGroup({key})");
#endif
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
#if ASSERTIONS
            if (existingGroup.Type != ComposeGroupType.Local)
                throw new InvalidOperationException($"Found {existingGroup.Type} group instead of local group!");
            if (existingGroup.Key != key)
                throw new InvalidOperationException($"Found {existingGroup.Key} group instead of {key}!");
#endif
            EnsureIndex(existingGroup);
            _enteredLocalGroups.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
            EnterGroup();
            _currentSlotIndex += LocalGroup.MetadataSize;
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Local,
            ParentAnchorId: GetOrAllocateParentAnchorForNonRestartGroup(),
            Size: 1,
            SlotsSize: Slots.ReplaceGroupHeaderSize,
            AnchorId: AnchorId.None,
            DataAnchorId: AnchorId.None,
            ElementIndex: _currentElementIndex,
            ElementsCount: 0
        );
        _enteredLocalGroups.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertCompositionLocalMap(_currentSlotIndex);
        EnterGroup();
        _currentSlotIndex += LocalGroup.MetadataSize;
    }

    public void EndLocalGroup(int key)
    {
#if LOGGING
        Log($"EndLocalGroup({key})");
#endif
        var parent = CurrentParent();
#if ASSERTIONS
        if (parent.Key != key)
            throw new InvalidOperationException($"Mismatching ending group key: {key} vs {parent.Key}!");
#endif
        var map = GetCompositionLocalMap();
        if (map != null)
            _enteredCompositionLocalMaps.Pop();
        else
            _enteredProvides.RemoveAt(_enteredProvides.Count - 1);
        ExitGroup(parent);
        if (parent.Key == _invalidationRoot)
            _invalidationRoot = -1;
        _enteredLocalGroups.Pop();
    }

    public T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        var map = RequireCompositionLocalMap() ?? _rootCompositionLocalMap;
        if (map == null)
            return defaultValueFactory();
        if (map.TryGetValue(compositionLocal, out var state))
        {
            if (state.Value is not T castedValue)
            {
                Debug.LogError($"Invalid cast of {state.Value?.GetType()} to {typeof(T)}");
                throw new InvalidCastException();
            }

            return castedValue;
        }

        return defaultValueFactory();
    }

    public void SetCompositionLocal(IImmutableStableList<CompositionLocalProvides> providers)
    {
        var map = GetCompositionLocalMap();
        if (map != null)
        {
            foreach (var provider in providers)
            {
                if (map.TryGetValue(provider.CompositionLocal, out var state))
                    state.Value = provider.Value;
                else
                    map[provider.CompositionLocal] = MutableStateOf(provider.Value);
            }

            _enteredCompositionLocalMaps.Push(new CompositionLocalMapEntry(_enteredLocalGroups.Peek().GroupIndex, map));
            return;
        }

        _enteredProvides.Add(providers);
    }

    private Dictionary<ICompositionLocal, IMutableState<object?>>? GetCompositionLocalMap()
    {
        if (!_enteredCompositionLocalMaps.TryPeek(out var lastLocalMapEntry))
            return null;
        if (_enteredLocalGroups.IsEmpty())
            return null;
        var lastLocalGroupIndex = _enteredLocalGroups.Peek().GroupIndex;
        return lastLocalMapEntry.GroupIndex == lastLocalGroupIndex ? lastLocalMapEntry.Map : null;
    }

    private Dictionary<ICompositionLocal, IMutableState<object?>>? RequireCompositionLocalMap()
    {
        if (_enteredLocalGroups.IsEmpty())
            return null;
        var map = GetCompositionLocalMap();
        if (map != null)
            return map;
        var (groupIndex, slotIndex) = _enteredLocalGroups.Peek();
        map = _enteredCompositionLocalMaps.IsNotEmpty()
            ? _enteredCompositionLocalMaps.Peek().Map.ToDictionary(static it => it.Key, static it => it.Value)
            : new Dictionary<ICompositionLocal, IMutableState<object?>>();
        foreach (var provider in _enteredProvides.SelectMany(static it => it))
            map[provider.CompositionLocal] = MutableStateOf(provider.Value);
        _slots.SetCompositionLocalMap(slotIndex, map);
        _enteredProvides.Clear();
        _enteredCompositionLocalMaps.Push(new CompositionLocalMapEntry(groupIndex, map));
        return map;
    }

    #endregion


    #region Restarting

    public void Clear()
    {
        _groups.Clear();
        _slots.Clear();
        _groupsAnchors.Clear();
        _slotsAnchors.Clear();
        _enteredParentsIndices.Clear();
        _enteredParentsSlotIndices.Clear();
        _enteredElementIndices.Clear();
        _enteredRestartGroups.Clear();
        _enteredLocalGroups.Clear();

        _enteredCompositionLocalMaps.Clear();
        _enteredProvides.Clear();
        _rootCompositionLocalMap = null;

        _currentGroupIndex = 0;
        _currentParentIndex = -1;
        _currentSlotIndex = 0;
        _currentParentSlotIndex = -1;
        _invalidationRoot = -1;
        _currentElementIndex = 0;
        _alreadyRemovedGroups = 0;
        _alreadyRemovedSlots = 0;
    }

    public void ResetTo(int groupIndex, Dictionary<ICompositionLocal, IMutableState<object?>>? compositionLocalMap)
    {
#if LOGGING
        Log($"ResetTo({groupIndex})");
#endif
        var group = _groups[groupIndex];
#if ASSERTIONS
        if (!group.AnchorId.IsValid)
            throw new InvalidOperationException($"Group {groupIndex} has invalid AnchorId!");
        if (!group.DataAnchorId.IsValid)
            throw new InvalidOperationException($"Group {groupIndex} has invalid DataAnchorId!");
#endif
        _invalidationRoot = groupIndex;
        _currentGroupIndex = groupIndex;
        _currentSlotIndex = _slotsAnchors[group.DataAnchorId].Index;
        _currentParentIndex = group.ParentAnchorId.IsValid ? _groupsAnchors[group.ParentAnchorId].Index : -1;
        _currentElementIndex = group.ElementIndex;
        _rootCompositionLocalMap = compositionLocalMap;
    }

    public void ResetTo(
        AnchorId groupAnchor,
        Dictionary<ICompositionLocal, IMutableState<object?>>? compositionLocalMap
    )
    {
        if (!groupAnchor.IsValid)
            return;
        var anchor = _groupsAnchors[groupAnchor];
        if (!anchor.IsValid)
            return;
        ResetTo(anchor.Index, compositionLocalMap);
    }

    public void ResetToOutOfBounds()
    {
        _enteredParentsIndices.Clear();
        _invalidationRoot = -1;
        _currentGroupIndex = _groups.Count;
        _currentSlotIndex = _slots.Count;
        _currentParentIndex = -1;
    }

    internal int GetGroupIndex(AnchorId groupAnchor)
    {
        return _groupsAnchors[groupAnchor].Index;
    }

    #endregion

    private AnchorId GetOrAllocateParentAnchorForNonRestartGroup()
    {
#if PARENT_ANCHORS_FOR_EVERYONE
        return GetOrAllocateParentAnchor();
#else
            return AnchorId.None;
#endif
    }

    private AnchorId GetOrAllocateParentAnchor()
    {
        if (_currentParentIndex < 0)
            return AnchorId.None;
        var parent = CurrentParent();
        if (parent.AnchorId.IsValid)
            return parent.AnchorId;
        var newAnchor = _groupsAnchors.AllocateAnchor(_currentParentIndex);
        parent = parent with { AnchorId = newAnchor };
        _groups[_currentParentIndex] = parent;
        return newAnchor;
    }

    private bool IsThereAlreadyAGroup()
    {
        if (_currentGroupIndex == _invalidationRoot)
            return true;
        if (_currentParentIndex < 0)
            return _currentGroupIndex < _groups.Count;
        var parent = CurrentParent();
        if (parent.Size == 0)
            return false;
        return _currentGroupIndex < _currentParentIndex + parent.Size;
    }

    private bool IsThereAlreadyASlot()
    {
        if (_currentGroupIndex == _invalidationRoot)
            return true;
        if (_currentParentIndex < 0)
            return _currentSlotIndex < _slots.Count;
        var parent = CurrentParent();
        if (parent.SlotsSize == 0)
            return false;
        return _currentSlotIndex < _currentParentSlotIndex + parent.SlotsSize;
    }

    private void EnsureIndex(ComposeGroup group)
    {
        if (!group.AnchorId.IsValid)
            return;
        var index = _groupsAnchors[group.AnchorId].Index;
        if (index == _currentGroupIndex)
            return;
        _groupsAnchors[group.AnchorId] = new Anchor(_currentGroupIndex);
    }

    private void EnterGroup()
    {
        _currentParentIndex = _currentGroupIndex;
        _enteredParentsIndices.Push(_currentGroupIndex);
        _enteredParentsSlotIndices.Push(_currentSlotIndex);
        _currentParentSlotIndex = _currentSlotIndex;
        _currentGroupIndex++;
    }

    private void ExitGroup(ComposeGroup currentParent)
    {
        var newSize = _currentGroupIndex - _currentParentIndex;
        var newSlotsSize = _currentSlotIndex - _currentParentSlotIndex;
        var newElementsCount = _currentElementIndex - currentParent.ElementIndex;
        if (currentParent.Type == ComposeGroupType.Reusable)
            newElementsCount = 1;
        var groupSizeOffset = newSize - currentParent.Size;
        var slotsSizeOffset = newSlotsSize - currentParent.SlotsSize;
        var elementsCountOffset = newElementsCount - currentParent.ElementsCount;
        if (newSize != currentParent.Size || newSlotsSize != currentParent.SlotsSize ||
            newElementsCount != currentParent.ElementsCount)
        {
            currentParent = currentParent with
            {
                Size = newSize,
                SlotsSize = newSlotsSize,
                ElementsCount = newElementsCount
            };
            _groups[_currentParentIndex] = currentParent;
        }

        if (groupSizeOffset < 0)
        {
            var currentGroupSizeOffset = -groupSizeOffset - _alreadyRemovedGroups;
#if LOGGING
            Log($"_groups.RemoveRange({_currentGroupIndex}, {currentGroupSizeOffset})");
#endif
            _groups.RemoveRange(_currentGroupIndex, currentGroupSizeOffset);
            _alreadyRemovedGroups += currentGroupSizeOffset;
        }

        if (slotsSizeOffset < 0)
        {
            var currentSlotsSizeOffset = -slotsSizeOffset - _alreadyRemovedSlots;
#if LOGGING
            Log($"_slots.RemoveRange({_currentSlotIndex}, {currentSlotsSizeOffset})");
#endif
            _slots.RemoveRange(_currentSlotIndex, currentSlotsSizeOffset);
            _alreadyRemovedSlots += currentSlotsSizeOffset;
        }

        if (_enteredParentsIndices.Count == 1)
        {
            ShiftGroupsAnchors(_currentGroupIndex + 1, groupSizeOffset);
            ShiftSlotsAnchors(_currentSlotIndex + 1, slotsSizeOffset);
            ShiftAncestorsGroupSizes(groupSizeOffset);
            ShiftAncestorsSlotSizes(slotsSizeOffset);
            ShiftAncestorsElementsCounts(elementsCountOffset);
            _alreadyRemovedGroups = 0;
            _alreadyRemovedSlots = 0;
        }

        _enteredParentsIndices.Pop();
        _enteredParentsSlotIndices.Pop();
        _currentParentIndex = _enteredParentsIndices.PeekOrDefault(-1);
        _currentParentSlotIndex = _enteredParentsSlotIndices.PeekOrDefault(-1);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine("Groups:");
        builder.AppendLine(_groups.ToString(_currentParentIndex, _currentGroupIndex, _groupsAnchors, _slotsAnchors));

        builder.AppendLine("Slots:");
        builder.AppendLine(_slots.ToString(_currentSlotIndex));

        builder.AppendLine("Groups Anchors:");
        builder.AppendLine(_groupsAnchors.ToString());
        builder.AppendLine("Slots Anchors:");
        builder.AppendLine(_slotsAnchors.ToString());

        return builder.ToString();
    }

    private void ShiftSlotsAnchors(int startIndex, int offset)
    {
        for (var i = 0; i < _slotsAnchors.Count; i++)
        {
            var anchor = _slotsAnchors[i];
            if (anchor.IsValid && anchor.Index >= startIndex)
                _slotsAnchors[i] = new Anchor(anchor.Index + offset);
        }
    }

    private void ShiftGroupsAnchors(int startIndex, int offset)
    {
        for (var i = 0; i < _groupsAnchors.Count; i++)
        {
            var anchor = _groupsAnchors[i];
            if (anchor.IsValid && anchor.Index >= startIndex)
                _groupsAnchors[i] = new Anchor(anchor.Index + offset);
        }
    }

    private void ShiftAncestorsGroupSizes(int offset)
    {
        if (offset == 0) return;
        var ancestorIndex = _currentParentIndex;
        var i = 0;
        while (ancestorIndex >= 0)
        {
            var ancestor = _groups[ancestorIndex];
            if (i++ > 0)
            {
                ancestor = ancestor with { Size = ancestor.Size + offset };
                _groups[ancestorIndex] = ancestor;
            }

            if (!ancestor.ParentAnchorId.IsValid)
                return;
            ancestorIndex = _groupsAnchors[ancestor.ParentAnchorId].Index;
        }
    }

    private void ShiftAncestorsSlotSizes(int offset)
    {
        if (offset == 0) return;
        var ancestorIndex = _currentParentIndex;
        var i = 0;
        while (ancestorIndex >= 0)
        {
            var ancestor = _groups[ancestorIndex];
            if (i++ > 0)
            {
                ancestor = ancestor with { SlotsSize = ancestor.SlotsSize + offset };
                _groups[ancestorIndex] = ancestor;
            }

            if (!ancestor.ParentAnchorId.IsValid)
                return;
            ancestorIndex = _groupsAnchors[ancestor.ParentAnchorId].Index;
        }
    }

    private void ShiftAncestorsElementsCounts(int offset)
    {
        if (offset == 0) return;
        var ancestorIndex = _currentParentIndex;
        while (ancestorIndex >= 0)
        {
            var ancestor = _groups[ancestorIndex];
            if (ancestor.Type == ComposeGroupType.Reusable)
                return;
            ancestor = ancestor with { ElementsCount = ancestor.ElementsCount + offset };
            _groups[ancestorIndex] = ancestor;
            if (!ancestor.ParentAnchorId.IsValid)
                return;
            ancestorIndex = _groupsAnchors[ancestor.ParentAnchorId].Index;
        }
    }

    private void Log(string message)
    {
        Debug.Log(message + "\n" + ToString());
    }
}

internal readonly record struct ComposeGroupEntry(
    int GroupIndex,
    int SlotIndex
);

internal readonly record struct CompositionLocalMapEntry(
    int GroupIndex,
    Dictionary<ICompositionLocal, IMutableState<object?>> Map
);