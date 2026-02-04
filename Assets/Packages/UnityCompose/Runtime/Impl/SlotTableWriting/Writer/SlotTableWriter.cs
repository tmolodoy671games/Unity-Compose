// #define LOGGING
// #define ASSERTIONS

#define PARENT_ANCHORS_FOR_EVERYONE

using System;
using System.Collections.Generic;
using System.Text;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;

internal class SlotTableWriter
{
    private readonly Groups _groups;
    private readonly Slots _slots;
    private readonly Anchors _groupsAnchors;
    private readonly Anchors _slotsAnchors;
    private readonly Composer _composer;

    private readonly Stack<ComposeGroupEntry> _enteredParents = new();
    private readonly Stack<int> _enteredElementIndices = new();
    private readonly Stack<ComposeGroupEntry> _enteredRestartGroups = new();
    private readonly Stack<ComposeGroupEntry> _enteredLocalGroups = new();
    private readonly Stack<ComposeGroupOffset> _pendingOffsets = new();

    private readonly Stack<VisualElement> _enteredElements = new();
    private VisualElement? _rootVisualElement;

    private CompositionLocalMap? _rootCompositionLocalMap = null;

    private int _currentGroupIndex = 0;
    private int _currentParentIndex = -1;
    private int _currentSlotIndex = 0;
    private int _currentParentSlotIndex = -1;
    private int _invalidationRoot = -1;
    private int _currentElementIndex = 0;

    public SlotTableWriter(Composer composer)
    {
        _composer = composer;
        var table = new SlotTable();
        _groups = new Groups(table.Groups);
        _slots = new Slots(table.Slots);
        _groupsAnchors = new Anchors(AnchorsType.Groups, table.GroupsAnchors, table.FreedGroupAnchors);
        _slotsAnchors = new Anchors(AnchorsType.Slots, table.SlotsAnchors, table.FreedSlotAnchors);

        _groups.AddItemsShiftObserver(it => ShiftGroupsAnchors(it.StartIndex, it.Count, it.Offset));
        _slots.AddItemsShiftObserver(it => ShiftSlotsAnchors(it.StartIndex, it.Count, it.Offset));
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

            // _enteredRestartGroups.Push(
            //     new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex)
            // );
            // SyncTypeAndKey(existingGroup, ComposeGroupType.Restart, key);
            // EnterGroup(existingGroup);
            // _currentSlotIndex += RestartGroup.MetadataSize;
            // return;

            var currentGroupIndex = _currentGroupIndex;
            var currentSlotIndex = _currentSlotIndex;
            if (TryEnterGroup(existingGroup, ComposeGroupType.Restart, key))
            {
                _enteredRestartGroups.Push(new ComposeGroupEntry(currentGroupIndex, currentSlotIndex));
                _currentSlotIndex += RestartGroup.MetadataSize;
                return;
            }
        }
        
        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Restart,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 0,
            SlotsSize: RestartGroup.MetadataSize,
            AnchorId: AnchorId.None,
            DataAnchorId: AnchorId.None,
            ElementIndex: _currentElementIndex,
            ElementsCount: 0,
            ContainsKeyGroups: false,
            CalledThisComposition: true
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertPreviousState(_currentSlotIndex);
        _slots.InsertRestartScope(_currentSlotIndex);
        _enteredRestartGroups.Push(
            new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex)
        );
        EnterGroup(newGroup);
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

    public Optional<T> GetPreviousStateAsStruct<T>() where T : struct
    {
        return _slots.GetPreviousStateAsStruct<T>(_currentParentSlotIndex);
    }

    public void UpdatePreviousState<T>(T state)
    {
        _slots.SetPreviousState(_currentParentSlotIndex, state);
    }

    public void UpdatePreviousStateAsStruct<T>(T state) where T : struct
    {
        _slots.SetPreviousStateAsStruct(_currentParentSlotIndex, state);
    }

    // TODO Sync descendant indices.
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
            restartGroup = restartGroup with
            {
                AnchorId = _groupsAnchors.AllocateAnchor(AbsoluteGroupIndex(enteredRestartGroupIndex))
            };
            _groups[enteredRestartGroupIndex] = restartGroup;
        }

        if (!restartGroup.DataAnchorId.IsValid)
        {
            restartGroup = restartGroup with
            {
                DataAnchorId = _slotsAnchors.AllocateAnchor(AbsoluteSlotIndex(enteredRestartGroupSlotIndex))
            };
            _groups[enteredRestartGroupIndex] = restartGroup;
        }

        restartScope = ComposeRestartScope.Get(
            groupAnchor: restartGroup.AnchorId,
            writer: this,
            compositionLocalMap: GetCompositionLocalMap(),
            element: GetParentVisualElement()
        );
        _slots.SetRestartScope(enteredRestartGroupSlotIndex, restartScope);
        return restartScope;
    }

    public void EndRestartGroup(int key)
    {
#if LOGGING
        Log($"EndRestartGroup({key})");
#endif
        var parent = CurrentParent();
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
            // SyncTypeAndKey(existingGroup, ComposeGroupType.Replace, key);
            // EnterGroup(existingGroup);
            // return;

            if (TryEnterGroup(existingGroup, ComposeGroupType.Replace, key))
                return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Replace,
            ParentAnchorId: GetOrAllocateParentAnchorForNonRestartGroup(),
            Size: 0,
            SlotsSize: 0,
            AnchorId: AnchorId.None,
            DataAnchorId: AnchorId.None,
            ElementIndex: _currentElementIndex,
            ElementsCount: 0,
            ContainsKeyGroups: false,
            CalledThisComposition: true
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
    }

    public void EndReplaceGroup(int key)
    {
        var parent = CurrentParent();
        ExitGroup(parent);
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
            // SyncTypeAndKey(existingGroup, ComposeGroupType.Reusable, key);
            // EnterGroup(existingGroup);
            // _currentSlotIndex += ReusableGroup.MetadataSize;
            // return;

            if (TryEnterGroup(existingGroup, ComposeGroupType.Reusable, key))
            {
                _currentSlotIndex += ReusableGroup.MetadataSize;
                return;
            }
        }

        _slots.InsertVisualElement(_currentSlotIndex);
        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Reusable,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 0,
            SlotsSize: 0,
            AnchorId: AnchorId.None,
            DataAnchorId: _slotsAnchors.AllocateAnchor(AbsoluteSlotIndex(_currentSlotIndex)),
            ElementIndex: _currentElementIndex,
            ElementsCount: 1,
            ContainsKeyGroups: false,
            CalledThisComposition: true
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        _currentSlotIndex += ReusableGroup.MetadataSize;
    }

    public void EndReusableGroup(int key)
    {
#if LOGGING
        Log($"EndReusableGroup({key})");
#endif
        var parent = CurrentParent();
        ExitGroup(parent);
        _currentElementIndex = _enteredElementIndices.PeekOrDefault(0);
        _currentElementIndex++;
        _enteredElementIndices.TryPop(out _);
        _enteredElements.Pop();
    }

    #endregion


    #region Movable Group

    public void StartMovableGroup<T>(int key, T dataKey)
    {
#if LOGGING
        Log($"StartKeyGroup({key}, {dataKey})");
#endif
        if (IsThereAlreadyAGroup())
        {
            if (TryFindAndSwapExisingKeyGroup(key, dataKey))
            {
                // Debug.Log($"Moving existing group {key}");
                var existingGroup = _groups[_currentGroupIndex];
                EnterGroup(existingGroup);
                _currentSlotIndex += MovableGroup.MetadataSize;
                return;
            }

            // throw new ArgumentOutOfRangeException("Should be unreachable");
        }

        var parent = CurrentParent();
        if (!parent.ContainsKeyGroups)
        {
            parent = parent with { ContainsKeyGroups = true };
            _groups[_currentParentIndex] = parent;
        }

        _slots.InsertKey(_currentSlotIndex, dataKey);
        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Movable,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 0,
            SlotsSize: MovableGroup.MetadataSize,
            AnchorId: AnchorId.None,
            DataAnchorId: _slotsAnchors.AllocateAnchor(AbsoluteSlotIndex(_currentSlotIndex)),
            ElementIndex: _currentElementIndex,
            ElementsCount: 0,
            ContainsKeyGroups: false,
            CalledThisComposition: true
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        _currentSlotIndex += MovableGroup.MetadataSize;
    }

    public void EndMovableGroup(int key)
    {
        var currentParent = CurrentParent();
        ExitGroup(currentParent);
    }

    private bool IsTheSameKeyGroup<T>(ComposeGroup group, int key, T dataKey)
    {
        if (group.Type != ComposeGroupType.Movable)
            return false;
        if (group.Key != key)
            return false;
        return _slots.GetKey<T>(LogicalSlotIndex(group.DataAnchorId)).Equals(dataKey);
    }

    private int IndexOfExistingKey<T>(int key, T dataKey)
    {
        var parent = CurrentParent();
        var startIndex = _currentParentIndex + 1;
        var resolvedParentSize = parent.Size + _pendingOffsets.PeekOrDefault(new ComposeGroupOffset(0, 0)).GroupOffset;
        var endIndex = _currentParentIndex + resolvedParentSize;
        for (var i = startIndex; i < endIndex;)
        {
            var candidate = _groups[i];
            // Debug.Log($"{key}, {dataKey}" + ": " + IsTheSameKeyGroup(candidate, key, dataKey));
            if (IsTheSameKeyGroup(candidate, key, dataKey))
                return i;
            i += candidate.Size;
        }

        return -1;
    }

    private bool TryFindAndSwapExisingKeyGroup<T>(int key, T dataKey)
    {
        var existingGroupIndex = IndexOfExistingKey(key, dataKey);
        if (existingGroupIndex < 0)
            return false;
        if (existingGroupIndex == _currentGroupIndex)
            return true;
        var existingGroup = _groups[existingGroupIndex];
        var existingGroupSlotIndex = LogicalSlotIndex(_groups[existingGroupIndex].DataAnchorId);
        var currentGroup = _groups[_currentGroupIndex];
        _groups.Swap(
            sourceIndex: existingGroupIndex,
            sourceCount: existingGroup.Size,
            targetIndex: _currentGroupIndex,
            targetCount: currentGroup.Size
        );
        _slots.Swap(
            sourceIndex: existingGroupSlotIndex,
            sourceCount: existingGroup.SlotsSize,
            targetIndex: _currentSlotIndex,
            targetCount: currentGroup.SlotsSize
        );

        return true;
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

    public Optional<T> ReadAsStruct<T>() where T : struct
    {
#if LOGGING
        Log($"ReadAsStruct<T>()");
#endif
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return _slots.GetAsStruct<T>(_currentSlotIndex);
    }

    public bool ReadAndWrite<T>(T value)
    {
        if (!IsThereAlreadyASlot())
        {
            _slots.Insert(_currentSlotIndex, value);
            _currentSlotIndex++;
            return true;
        }

        var result = !_slots.GetAsOptional<T>(_currentSlotIndex).Equals(value);
        if (result)
            _slots[_currentSlotIndex] = value;
        _currentSlotIndex++;
        return result;
    }

    public Optional<T> ReadAndWriteAsStruct<T>(T value) where T : struct
    {
        if (!IsThereAlreadyASlot())
        {
            _slots.InsertAsStruct(_currentSlotIndex, value);
            _currentSlotIndex++;
            return Optional.Empty<T>();
        }

        var result = _slots.GetAsStruct<T>(_currentSlotIndex);
        _slots.SetAsStruct(_currentSlotIndex, value);
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

    public void WriteAsStruct<T>(T value) where T : struct
    {
#if LOGGING
        Log($"WriteAsStruct<T>()");
#endif
        if (!IsThereAlreadyASlot())
        {
            _slots.InsertAsStruct(_currentSlotIndex, value);
            _currentSlotIndex++;
            return;
        }

        _slots.SetAsStruct(_currentSlotIndex, value);
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

    public ReusableComposeNode<T> GetReusableNode<T>() where T : VisualElement
    {
        return _slots.GetReusableNode<T>(_currentParentSlotIndex);
    }

    public void WriteVisualElement(VisualElement visualElement)
    {
        // if (IsDebugVisualElementNamesEnabled)
        //     visualElement.name = CurrentParent().Key.ToString();
        _slots.SetVisualElement(_currentParentSlotIndex, visualElement);
        var currentParent = CurrentParent();
        if (currentParent.ElementsCount != 1)
        {
            currentParent = currentParent with { ElementsCount = 1 };
            _groups[_currentParentIndex] = currentParent;
        }
    }

    public int GetCurrentElementIndex() => _currentElementIndex;

    public void EnterVisualElement(VisualElement element)
    {
        _enteredElementIndices.Push(_currentElementIndex);
        _currentElementIndex = 0;
        _enteredElements.Push(element);
    }

    public VisualElement? GetParentVisualElement()
    {
        return _enteredElements!.PeekOrDefault(_rootVisualElement);
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
            // _enteredLocalGroups.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
            // SyncTypeAndKey(existingGroup, ComposeGroupType.Local, key);
            // EnterGroup(existingGroup);
            // _currentSlotIndex += LocalGroup.MetadataSize;
            // return;

            var currentGroupIndex = _currentGroupIndex;
            var currentSlotIndex = _currentSlotIndex;
            if (TryEnterGroup(existingGroup, ComposeGroupType.Local, key))
            {
                _enteredLocalGroups.Push(new ComposeGroupEntry(currentGroupIndex, currentSlotIndex));
                _currentSlotIndex += LocalGroup.MetadataSize;
                return;
            }
        }

        var parentDictionary = _enteredLocalGroups.IsNotEmpty()
            ? _slots.GetCompositionLocalMap(_enteredLocalGroups.Peek().SlotIndex)
            : _rootCompositionLocalMap;
        var map = parentDictionary != null
            ? parentDictionary.Copy()
            : CompositionLocalMap.Get();
        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Local,
            ParentAnchorId: GetOrAllocateParentAnchorForNonRestartGroup(),
            Size: 0,
            SlotsSize: LocalGroup.MetadataSize,
            AnchorId: AnchorId.None,
            DataAnchorId: AnchorId.None,
            ElementIndex: _currentElementIndex,
            ElementsCount: 0,
            ContainsKeyGroups: false,
            CalledThisComposition: true
        );
        _enteredLocalGroups.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertCompositionLocalMap(_currentSlotIndex, map);
        EnterGroup(newGroup);
        _currentSlotIndex += LocalGroup.MetadataSize;
    }

    public void EndLocalGroup(int key)
    {
#if LOGGING
        Log($"EndLocalGroup({key})");
#endif
        var parent = CurrentParent();
        ExitGroup(parent);
        _enteredLocalGroups.Pop();
    }

    public T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        var map = GetCompositionLocalMap() ?? _rootCompositionLocalMap;
        return map == null ? defaultValueFactory() : map.Get(compositionLocal, defaultValueFactory);
    }

    public CompositionLocalMap? GetCompositionLocalMap()
    {
        if (_enteredLocalGroups.IsEmpty())
            return _rootCompositionLocalMap;
        var map = _slots.GetCompositionLocalMap(_enteredLocalGroups.Peek().SlotIndex);
        return map;
    }

    #endregion


    #region Restarting

    public void Clear()
    {
        _groups.Clear();
        for (var i = 0; i < _slots.Count; i++)
        {
            if (_slots[i] is IDisposable disposable)
                disposable.Dispose();
        }

        _slots.Clear();
        _groupsAnchors.Clear();
        _slotsAnchors.Clear();

        _enteredParents.Clear();
        _enteredElementIndices.Clear();
        _enteredRestartGroups.Clear();
        _enteredLocalGroups.Clear();
        _pendingOffsets.Clear();

        _enteredElements.Clear();
        _rootVisualElement = null;
        _rootCompositionLocalMap = null;

        _currentGroupIndex = 0;
        _currentParentIndex = -1;
        _currentSlotIndex = 0;
        _currentParentSlotIndex = -1;
        _invalidationRoot = -1;
        _currentElementIndex = 0;
    }

    private void ResetTo(
        int groupIndex,
        CompositionLocalMap? compositionLocalMap,
        VisualElement? element
    )
    {
        // Log($"ResetTo({groupIndex})");
#if LOGGING
        LogWarning($"ResetTo({groupIndex})");
#endif
        var group = _groups[groupIndex];
#if ASSERTIONS
        if (group.Type != ComposeGroupType.Restart)
            throw new InvalidOperationException($"Trying to restart non-restart group: {group}");
        if (!group.AnchorId.IsValid)
            throw new InvalidOperationException($"Group {groupIndex} has invalid AnchorId!");
        if (!group.DataAnchorId.IsValid)
            throw new InvalidOperationException($"Group {groupIndex} has invalid DataAnchorId!");
#endif
        _invalidationRoot = groupIndex;
        _currentGroupIndex = groupIndex;
        _currentSlotIndex = LogicalSlotIndex(group.DataAnchorId);
        _currentParentIndex = -1;
        _currentParentSlotIndex = -1;
        _currentElementIndex = group.ElementIndex;
        _rootCompositionLocalMap = compositionLocalMap;
        _rootVisualElement = element;

        _enteredParents.Clear();
        _enteredElementIndices.Clear();
        _enteredRestartGroups.Clear();
        _enteredLocalGroups.Clear();

        _pendingOffsets.Clear();
        _enteredElements.Clear();
    }

    public bool ResetTo(
        AnchorId groupAnchor,
        CompositionLocalMap? compositionLocalMap,
        VisualElement? element
    )
    {
        if (!groupAnchor.IsValid)
            return false;
        var anchor = _groupsAnchors[groupAnchor];
        if (!anchor.IsValid)
            return false;
        ResetTo(LogicalGroupIndex(anchor.Location), compositionLocalMap, element);
        return true;
    }

    public void RequestCurrentComposer() => _composer.SetAsCurrentComposer();

    public void ReleaseCurrentComposer() => _composer.ResetAsCurrentComposer();

    #endregion


    #region Utils

    private bool TryEnterGroup(ComposeGroup group, ComposeGroupType type, int key)
    {
        if (group.Type == type && group.Key == key)
        {
            EnterGroup(group);
            return true;
        }

#if LOGGING
        LogWarning($"Remove existing group at {_currentGroupIndex}");
#endif
        CleanupGroups(_currentGroupIndex, group.Size);
        _groups.RemoveRange(_currentGroupIndex, group.Size);
        CleanupSlots(_currentSlotIndex, group.SlotsSize);
        _slots.RemoveRange(_currentSlotIndex, group.SlotsSize);

        if (_pendingOffsets.IsNotEmpty())
        {
            var oldOffset = _pendingOffsets.Pop();
            _pendingOffsets.Push(
                new ComposeGroupOffset(
                    GroupOffset: oldOffset.SlotOffset - group.Size,
                    SlotOffset: oldOffset.SlotOffset - group.SlotsSize
                )
            );
        }

        return false;
    }

    private void SyncTypeAndKey(ComposeGroup group, ComposeGroupType type, int key)
    {
        if (group.Type == type && group.Key == key)
            return;
        group = group with { Type = type, Key = key };
        _groups[_currentGroupIndex] = group;
        for (var i = _currentSlotIndex; i < _currentSlotIndex + group.SlotsSize; i++)
            _slots[i] = ComposeEmptySlot.Instance;
    }

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
        var newAnchor = _groupsAnchors.AllocateAnchor(AbsoluteGroupIndex(_currentParentIndex));
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
        var resolvedParentSize = parent.Size + _pendingOffsets.PeekOrDefault(new ComposeGroupOffset(0, 0)).GroupOffset;
        if (resolvedParentSize == 0)
            return false;
        return _currentGroupIndex < _currentParentIndex + resolvedParentSize;
    }

    private bool IsThereAlreadyASlot()
    {
        if (_currentGroupIndex == _invalidationRoot)
            return true;
        if (_currentParentIndex < 0)
            return _currentSlotIndex < _slots.Count;
        var parent = CurrentParent();
        var resolvedParentSlotSize = parent.SlotsSize +
                                     _pendingOffsets.PeekOrDefault(new ComposeGroupOffset(0, 0)).SlotOffset;
        if (resolvedParentSlotSize == 0)
            return false;
        return _currentSlotIndex < _currentParentSlotIndex + resolvedParentSlotSize;
    }

    private void EnterGroup(ComposeGroup group)
    {
        SyncElementIndex(group, canReinsert: true);
        _currentParentIndex = _currentGroupIndex;
        _enteredParents.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
        _pendingOffsets.Push(new ComposeGroupOffset(0, 0));
        _currentParentSlotIndex = _currentSlotIndex;
        _currentGroupIndex++;
    }

    private void SyncElementIndex(ComposeGroup group, bool canReinsert)
    {
        if (group.ElementIndex == _currentElementIndex)
            return;
        // Log($"SyncElementIndex: {group.ElementIndex} vs {_currentElementIndex}");
        var offset = _currentElementIndex - group.ElementIndex;
        var maxIndex = _currentGroupIndex + group.Size;
        for (var groupIndex = _currentGroupIndex; groupIndex < maxIndex;)
        {
            var checkedGroup = _groups[groupIndex];
            checkedGroup = checkedGroup with { ElementIndex = checkedGroup.ElementIndex + offset };
            _groups[groupIndex] = checkedGroup;
            if (checkedGroup.Type == ComposeGroupType.Reusable)
            {
                if (canReinsert)
                    RepositionVisualElement(checkedGroup, checkedGroup.ElementIndex);
                groupIndex += checkedGroup.Size;
            }
            else
                groupIndex++;
        }

        // Log("After SyncElementIndex");
    }

    private void RepositionVisualElement(ComposeGroup group, int elementIndex)
    {
        var slotAnchorId = group.DataAnchorId;
        if (!slotAnchorId.IsValid)
            return;
        var slotAnchor = _slotsAnchors[slotAnchorId];
        if (!slotAnchor.IsValid)
            return;
        var slotIndex = LogicalSlotIndex(slotAnchor.Location);
        var node = _slots.GetReusableNode(slotIndex);
        node?.ReInsert(elementIndex);
    }

    private void ExitGroup(ComposeGroup currentParent)
    {
        var offsets = _pendingOffsets.Peek();
        var oldSize = currentParent.Size + offsets.GroupOffset;
        var oldSlotsSize = currentParent.SlotsSize + offsets.SlotOffset;

        var newSize = _currentGroupIndex - _currentParentIndex;
        var newSlotsSize = _currentSlotIndex - _currentParentSlotIndex;
        var newElementsCount = _currentElementIndex - currentParent.ElementIndex;
        if (currentParent.Type == ComposeGroupType.Reusable)
            newElementsCount = 1;

        var parentGroupSizeOffset = newSize - currentParent.Size;
        var parentSlotsSizeOffset = newSlotsSize - currentParent.SlotsSize;
        var elementsCountOffset = newElementsCount - currentParent.ElementsCount;

        var groupsToRemove = -(newSize - oldSize);
        var slotsToRemove = -(newSlotsSize - oldSlotsSize);

        var anySizeChanged = newSize != currentParent.Size ||
                             newSlotsSize != currentParent.SlotsSize ||
                             newElementsCount != currentParent.ElementsCount;
        if (anySizeChanged)
        {
            currentParent = currentParent with
            {
                Size = newSize,
                SlotsSize = newSlotsSize,
                ElementsCount = newElementsCount
            };
            _groups[_currentParentIndex] = currentParent;
        }

        if (groupsToRemove > 0)
        {
#if LOGGING
            Log($"_groups.RemoveRange({_currentGroupIndex}, {groupsToRemove})");
#endif
            CleanupGroups(_currentGroupIndex, groupsToRemove);
            _groups.RemoveRange(_currentGroupIndex, groupsToRemove);
        }

        if (slotsToRemove > 0)
        {
#if LOGGING
            Log($"_slots.RemoveRange({_currentSlotIndex}, {slotsToRemove})");
#endif
            CleanupSlots(_currentSlotIndex, slotsToRemove);
            _slots.RemoveRange(_currentSlotIndex, slotsToRemove);
        }

        if (_enteredParents.Count == 1)
        {
            ShiftAncestorsGroupSizes(parentGroupSizeOffset);
            ShiftAncestorsSlotSizes(parentSlotsSizeOffset);
            ShiftAncestorsElementsCounts(elementsCountOffset);
        }

        _enteredParents.Pop();
        var newParent = _enteredParents.PeekOrDefault(new ComposeGroupEntry(-1, -1));
        _currentParentIndex = newParent.GroupIndex;
        _currentParentSlotIndex = newParent.SlotIndex;
        _pendingOffsets.Pop();
        if (_pendingOffsets.IsNotEmpty())
        {
            var oldOffsets = _pendingOffsets.Pop();
            _pendingOffsets.Push(
                new ComposeGroupOffset(
                    oldOffsets.GroupOffset + parentGroupSizeOffset,
                    oldOffsets.SlotOffset + parentSlotsSizeOffset
                )
            );
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"CURRENT_ELEMENT_INDEX: {_currentElementIndex}");
        builder.AppendLine("Groups:");
        builder.AppendLine(
            _groups.ToString(_currentParentIndex, _currentGroupIndex, _groupsAnchors, _slotsAnchors, _slots)
        );

        // builder.AppendLine("Slots:");
        // builder.AppendLine(_slots.ToString(_currentSlotIndex));
        //
        // builder.AppendLine("Groups Anchors:");
        // builder.AppendLine(_groupsAnchors.ToString());
        // builder.AppendLine("Slots Anchors:");
        // builder.AppendLine(_slotsAnchors.ToString());

        return builder.ToString();
    }

    public string SlotsToString()
    {
        return _slots.ToString(_currentSlotIndex);
    }

    #region Group Anchors

    private void ShiftGroupsAnchors(int startIndex, int count, int offset)
    {
        if (offset == 0)
            return;

        for (var i = 0; i < _groupsAnchors.Count; i++)
        {
            var anchor = _groupsAnchors[i];
            if (!anchor.IsValid)
                continue;
            var location = anchor.Location;
            if (location >= startIndex && location < startIndex + count)
                _groupsAnchors[i] = new Anchor(location + offset);
        }
    }

    #endregion

    #region Slot Anchors

    private void ShiftSlotsAnchors(int startIndex, int count, int offset)
    {
        if (offset == 0)
            return;

        for (var i = 0; i < _slotsAnchors.Count; i++)
        {
            var anchor = _slotsAnchors[i];
            if (!anchor.IsValid)
                continue;
            var location = anchor.Location;
            if (location >= startIndex && location < startIndex + count)
                _slotsAnchors[i] = new Anchor(location + offset);
        }
    }

    #endregion


    private void ShiftAncestorsGroupSizes(int offset)
    {
        if (offset == 0) return;
        var ancestorIndex = _currentParentIndex;
        var i = 0;
        while (ancestorIndex >= 0 && i < 100)
        {
            var ancestor = _groups[ancestorIndex];
            if (i++ > 0)
            {
                ancestor = ancestor with { Size = ancestor.Size + offset };
                _groups[ancestorIndex] = ancestor;
            }

            if (!ancestor.ParentAnchorId.IsValid)
                return;
            ancestorIndex = LogicalGroupIndex(ancestor.ParentAnchorId);
        }
    }

    private void ShiftAncestorsSlotSizes(int offset)
    {
        if (offset == 0) return;
        var ancestorIndex = _currentParentIndex;
        var i = 0;
        while (ancestorIndex >= 0 && i < 100)
        {
            var ancestor = _groups[ancestorIndex];
            if (i++ > 0)
            {
                ancestor = ancestor with { SlotsSize = ancestor.SlotsSize + offset };
                _groups[ancestorIndex] = ancestor;
            }

            if (!ancestor.ParentAnchorId.IsValid)
                return;
            ancestorIndex = LogicalGroupIndex(ancestor.ParentAnchorId);
        }
    }

    private void ShiftAncestorsElementsCounts(int offset)
    {
        if (offset == 0) return;
        var ancestorIndex = _currentParentIndex;
        var i = 0;
        while (ancestorIndex >= 0 && i < 100)
        {
            var ancestor = _groups[ancestorIndex];
            if (ancestor.Type == ComposeGroupType.Reusable)
                return;

            if (i++ > 0)
            {
                ancestor = ancestor with { ElementsCount = ancestor.ElementsCount + offset };
                _groups[ancestorIndex] = ancestor;
            }

            if (!ancestor.ParentAnchorId.IsValid)
                return;
            ancestorIndex = LogicalGroupIndex(ancestor.ParentAnchorId);
        }
    }

    private void CleanupGroups(int startIndex, int count)
    {
        if (count == 0)
            return;
        var targetIndex = Math.Min(startIndex + count, _groups.Count);
        if (startIndex + count > _groups.Count)
            Debug.LogError("Out of range clean ups");
        for (var i = startIndex; i < targetIndex; i++)
        {
            var group = _groups[i];
            if (group.AnchorId.IsValid)
                _groupsAnchors.ReleaseAnchor(group.AnchorId);
            if (group.DataAnchorId.IsValid)
                _slotsAnchors.ReleaseAnchor(group.DataAnchorId);
        }
    }

    private void CleanupSlots(int startIndex, int count)
    {
        if (count == 0)
            return;
        var targetIndex = Math.Min(startIndex + count, _slots.Count);
        if (startIndex + count > _slots.Count)
            Debug.LogError("Out of range clean ups");
        for (var i = startIndex; i < targetIndex; i++)
        {
            var slot = _slots[i];
            if (slot is IDisposable disposable)
                disposable.Dispose();
        }
    }

    private int AbsoluteGroupIndex(int index) => _groups.LogicalToAbsoluteIndex(index);
    private int LogicalGroupIndex(int index) => _groups.AbsoluteToLogicalIndex(index);
    private int AbsoluteGroupIndex(AnchorId anchorId) => _groupsAnchors[anchorId].Location;

    private int LogicalGroupIndex(AnchorId anchorId) =>
        _groups.AbsoluteToLogicalIndex(_groupsAnchors[anchorId].Location);

    private int AbsoluteSlotIndex(int index) => _slots.LogicalToAbsoluteIndex(index);
    private int LogicalSlotIndex(int index) => _slots.AbsoluteToLogicalIndex(index);
    private int AbsoluteSlotIndex(AnchorId anchorId) => _slotsAnchors[anchorId].Location;
    private int LogicalSlotIndex(AnchorId anchorId) => _slots.AbsoluteToLogicalIndex(_slotsAnchors[anchorId].Location);

    public void Log(object? message)
    {
        var formattedMessage = message + "\n\n" + ToString();
        Debug.Log(formattedMessage);
    }

    public void LogWarning(object? message)
    {
        var formattedMessage = message + "\n\n" + ToString();
        Debug.LogWarning(formattedMessage);
    }

    #endregion
}

internal readonly record struct ComposeGroupEntry(
    int GroupIndex,
    int SlotIndex
);

internal readonly record struct ComposeGroupOffset(
    int GroupOffset,
    int SlotOffset
);