#define PARENT_ANCHORS_FOR_EVERYONE

using System;
using System.Collections.Generic;
using System.IO;
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

    public bool StartRestartGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"StartRestartGroup({key})");
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
            var currentGroupIndex = _currentGroupIndex;
            var currentSlotIndex = _currentSlotIndex;
            if (TryEnterGroup(existingGroup, ComposeGroupType.Restart, key))
            {
                _enteredRestartGroups.Push(new ComposeGroupEntry(currentGroupIndex, currentSlotIndex));
                _currentSlotIndex += RestartGroup.MetadataSize;
                return false;
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
            ElementsCount: 0
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        // _slots.InsertPreviousState(_currentSlotIndex);
        _slots.InsertRestartScope(_currentSlotIndex);
        _enteredRestartGroups.Push(
            new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex)
        );
        EnterGroup(newGroup);
        _currentSlotIndex += RestartGroup.MetadataSize;
        return true;
    }

    public bool IsInInvalidationRoot()
    {
        return _invalidationRoot == _currentParentIndex;
    }

    // TODO Sync descendant indices.
    public void SkipToGroupEnd()
    {
        if (ComposeConstants.Logging)
            Log($"SkipToGroupEnd(slots: {CurrentParent().SlotsSize - RestartGroup.MetadataSize})");
        var parent = CurrentParent();

        _currentGroupIndex += parent.Size - 1;
        var offset = _currentSlotIndex - _enteredRestartGroups.Peek().SlotIndex;
        _currentSlotIndex += parent.SlotsSize - offset;
        _currentElementIndex += parent.ElementsCount;
    }

    public IComposeRestartScope? GetRestartScope()
    {
        if (_enteredRestartGroups.IsEmpty())
            return null;
        var scope = _slots.GetRestartScope(_enteredRestartGroups.Peek().SlotIndex);
        return scope;
    }

    public IComposeRestartScope? RequireRestartScope()
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
        if (ComposeConstants.Logging)
            Log($"EndRestartGroup({key})");
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
        if (ComposeConstants.Logging)
            Log($"StartReplaceGroup({key})");
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
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
            ElementsCount: 0
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
        if (ComposeConstants.Logging)
            Log($"StartReusableGroup({key})");
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
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
            ElementsCount: 1
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        _currentSlotIndex += ReusableGroup.MetadataSize;
    }

    public void EndReusableGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"EndReusableGroup({key})");
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
        if (ComposeConstants.Logging)
            Log($"StartKeyGroup({key}, {dataKey})");
        if (IsThereAlreadyAGroup())
        {
            if (TryFindAndSwapExisingKeyGroup(key, dataKey))
            {
                var existingGroup = _groups[_currentGroupIndex];
                EnterGroup(existingGroup);
                _currentSlotIndex += MovableGroup.MetadataSize;
                return;
            }
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
            ElementsCount: 0
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
        if (ComposeConstants.Logging)
            Log("After Swap");

        return true;
    }

    #endregion


    #region Remember

    public Optional<T> Read<T>()
    {
        if (ComposeConstants.Logging)
            Log($"Read<T>()");
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return _slots.GetAsOptional<T>(_currentSlotIndex);
    }

    public Optional<T> ReadAsStruct<T>()
    {
        if (ComposeConstants.Logging)
            Log($"ReadAsStruct<T>()");
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return _slots.GetAsStruct<T>(_currentSlotIndex);
    }

    public bool ReadAndWrite<T>(T value)
    {
        if (ComposeConstants.Logging)
            Log($"ReadAndWrite<T>()");
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

    public bool ReadAndWriteAsStruct<T>(T value)
    {
        if (ComposeConstants.Logging)
            Log($"ReadAndWriteAsStruct<T>()");
        if (!IsThereAlreadyASlot())
        {
            _slots.InsertAsStruct(_currentSlotIndex, value);
            _currentSlotIndex++;
            return true;
        }

        var result = !_slots.GetAsStruct<T>(_currentSlotIndex).Equals(value);
        _slots.SetAsStruct(_currentSlotIndex, value);
        _currentSlotIndex++;
        return result;
    }

    public void Write<T>(T value)
    {
        if (ComposeConstants.Logging)
            Log($"Write<T>()");
        if (!IsThereAlreadyASlot())
        {
            _slots.Insert(_currentSlotIndex, value);
            _currentSlotIndex++;
            return;
        }

        _slots[_currentSlotIndex] = value;
        _currentSlotIndex++;
    }

    public void WriteAsStruct<T>(T value)
    {
        if (ComposeConstants.Logging)
            Log($"WriteAsStruct<T>()");
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
        if (ComposeConstants.Logging)
            Log($"IncrementSlotIndex()");
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
        _slots.SetVisualElement(_currentParentSlotIndex, visualElement);
        var currentParent = CurrentParent();
        if (currentParent.ElementsCount == 1)
            return;
        currentParent = currentParent with { ElementsCount = 1 };
        _groups[_currentParentIndex] = currentParent;
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
        if (ComposeConstants.Logging)
            Log($"StartLocalGroup({key})");
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
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
            ElementsCount: 0
        );
        _enteredLocalGroups.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertCompositionLocalMap(_currentSlotIndex, map);
        EnterGroup(newGroup);
        _currentSlotIndex += LocalGroup.MetadataSize;
    }

    public void EndLocalGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"EndLocalGroup({key})");
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
            if (_slots[i] is IComposeDisposable disposable)
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

    private bool ResetTo(
        int groupIndex,
        CompositionLocalMap? compositionLocalMap,
        VisualElement? element
    )
    {
        if (ComposeConstants.Logging)
            LogWarning($"ResetTo({groupIndex})");
        var group = _groups[groupIndex];
        if (ComposeConstants.Assertions)
        {
            if (group.Type != ComposeGroupType.Restart)
                throw new InvalidOperationException($"Trying to restart non-restart group: {group}");
            if (!group.AnchorId.IsValid)
                throw new InvalidOperationException($"Group {groupIndex} has invalid AnchorId!");
            if (!group.DataAnchorId.IsValid)
                throw new InvalidOperationException($"Group {groupIndex} has invalid DataAnchorId!");
        }

        if (!group.DataAnchorId.IsValid)
            return false;
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
        return true;
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
        return ResetTo(LogicalGroupIndex(anchor.Location), compositionLocalMap, element);
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

        if (ComposeConstants.Logging)
            LogWarning($"Remove existing group at {_currentGroupIndex}");
        CleanupGroups(_currentGroupIndex, group.Size);
        _groups.RemoveRange(_currentGroupIndex, group.Size);
        CleanupSlots(_currentSlotIndex, group.SlotsSize);
        _slots.RemoveRange(_currentSlotIndex, group.SlotsSize);

        if (_pendingOffsets.IsNotEmpty())
        {
            var oldOffset = _pendingOffsets.Pop();
            _pendingOffsets.Push(
                new ComposeGroupOffset(
                    GroupOffset: oldOffset.GroupOffset - group.Size,
                    SlotOffset: oldOffset.SlotOffset - group.SlotsSize
                )
            );
        }

        return false;
    }

    private AnchorId GetOrAllocateParentAnchorForNonRestartGroup()
    {
        if (ComposeConstants.ParentAnchorsForEveryone)
            return GetOrAllocateParentAnchor();
        return AnchorId.None;
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
            if (ComposeConstants.Logging)
                Log($"_groups.RemoveRange({_currentGroupIndex}, {groupsToRemove})");
            CleanupGroups(_currentGroupIndex, groupsToRemove);
            _groups.RemoveRange(_currentGroupIndex, groupsToRemove);
        }

        if (slotsToRemove > 0)
        {
            if (ComposeConstants.Logging)
                Log($"_slots.RemoveRange({_currentSlotIndex}, {slotsToRemove})");
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

    public string Format()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"CURRENT_ELEMENT_INDEX: {_currentElementIndex}");
        builder.AppendLine("Groups:");
        builder.AppendLine(
            _groups.ToString(_currentParentIndex, _currentGroupIndex, _groupsAnchors, _slotsAnchors, _slots)
        );

        builder.AppendLine("Slots:");
        builder.AppendLine(_slots.Format(_currentSlotIndex));

        // builder.AppendLine("Groups Anchors:");
        // builder.AppendLine(_groupsAnchors.ToString());
        // builder.AppendLine("Slots Anchors:");
        // builder.AppendLine(_slotsAnchors.ToString());

        return builder.ToString();
    }

    public string SlotsToString()
    {
        return _slots.Format(_currentSlotIndex);
    }
    
    public void WriteSlotsToFile(TextWriter writer)
    {
        _slots.WriteToFile(_currentSlotIndex, writer);
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
            if (slot is IComposeDisposable disposable)
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

    public void WriteToFile(TextWriter writer)
    {
        writer.WriteLine($"CURRENT_ELEMENT_INDEX: {_currentElementIndex}");
        writer.WriteLine("Groups:");
        _groups.WriteToFile(_currentParentIndex, _currentGroupIndex, _groupsAnchors, _slotsAnchors, _slots, writer);

        writer.WriteLine("Slots:");
        _slots.WriteToFile(_currentSlotIndex, writer);
    }
}

internal readonly record struct ComposeGroupEntry(
    int GroupIndex,
    int SlotIndex
);

internal readonly record struct ComposeGroupOffset(
    int GroupOffset,
    int SlotOffset
);