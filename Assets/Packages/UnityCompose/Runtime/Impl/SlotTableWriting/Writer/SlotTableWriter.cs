// #define LOGGING

#define ASSERTIONS
#define PARENT_ANCHORS_FOR_EVERYONE

using System;
using System.Collections.Generic;
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
    private readonly Stack<int> _enteredRestartGroupIndices = new();
    private readonly Stack<int> _enteredRestartGroupSlotIndices = new();

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
            if (existingGroup.Key == key)
            {
                _enteredRestartGroupSlotIndices.Push(_currentSlotIndex);
                _enteredRestartGroupIndices.Push(_currentGroupIndex);
                EnterGroup();
                _currentSlotIndex += Slots.RestartGroupHeaderSize;
                return;
            }
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Restart,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 1,
            SlotsSize: Slots.RestartGroupHeaderSize,
            AnchorId: AnchorId.None,
            DataAnchorId: AnchorId.None,
            ElementIndex: _currentElementIndex,
            ElementsCount: 0
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertPreviousState(_currentSlotIndex);
        _slots.InsertRestartScope(_currentSlotIndex);
        _enteredRestartGroupSlotIndices.Push(_currentSlotIndex);
        _enteredRestartGroupIndices.Push(_currentGroupIndex);
        EnterGroup();
        _currentSlotIndex += Slots.RestartGroupHeaderSize;
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

    public void SkipToGroupEnd()
    {
#if LOGGING
        Log("SkipToGroupEnd()");
#endif
        var parent = CurrentParent();
        _currentGroupIndex += parent.Size - 1;
        _currentSlotIndex += parent.SlotsSize - RestartGroup.MetadataSize;
        _currentElementIndex += parent.ElementsCount - 1;
    }

    public ComposeRestartScope? GetRestartScope()
    {
        if (_enteredRestartGroupSlotIndices.IsEmpty())
            return null;
        return _slots.GetRestartScope(_enteredRestartGroupSlotIndices.Peek());
    }

    public ComposeRestartScope? RequireRestartScope()
    {
        var restartScope = GetRestartScope();
        if (restartScope != null)
            return restartScope;
        if (_enteredRestartGroupIndices.IsEmpty())
            return null;
        var enteredRestartGroupIndex = _enteredRestartGroupIndices.Peek();
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
                DataAnchorId = _slotsAnchors.AllocateAnchor(_enteredRestartGroupSlotIndices.Peek())
            };
            _groups[enteredRestartGroupIndex] = restartGroup;
        }

        restartScope = new ComposeRestartScope(restartGroup.AnchorId, this);
        _slots.SetRestartScope(_enteredRestartGroupSlotIndices.Peek(), restartScope);
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
        _enteredRestartGroupIndices.Pop();
        _enteredRestartGroupSlotIndices.Pop();
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

            EnterGroup();
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Replace,
            ParentAnchorId: GetOrAllocateParentAnchorForReplaceGroup(),
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
#if ASSERTIONS
            var existingGroup = _groups[_currentGroupIndex];
            if (existingGroup.Key != key)
                throw new InvalidOperationException($"Found {existingGroup.Key} instead of {key}!");
            if (existingGroup.Type != ComposeGroupType.Reusable)
                throw new InvalidOperationException($"Found {existingGroup.Type} group instead of Reusable group!");
#endif
            EnterGroup();
            _currentSlotIndex += Slots.ReusableGroupHeaderSize;
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Reusable,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 1,
            SlotsSize: 2,
            AnchorId: _groupsAnchors.AllocateAnchor(_currentGroupIndex),
            DataAnchorId: _slotsAnchors.AllocateAnchor(_currentSlotIndex),
            ElementIndex: _currentElementIndex,
            ElementsCount: 0
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertVisualElement(_currentSlotIndex, null);
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

    public Optional<T> ReadAsStruct<T>() where T : struct
    {
#if LOGGING
        Log($"ReadAsStruct<T>()");
#endif
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return _slots.GetStruct<T>(_currentSlotIndex);
    }

    public void Write<T>(T value)
    {
#if LOGGING
        Log($"Write<T>()");
#endif
        if (!IsThereAlreadyASlot())
        {
            _slots.Insert(_currentSlotIndex, value);
            return;
        }

        _slots[_currentSlotIndex] = value;
    }

    public void WriteAsStruct<T>(T value) where T : struct
    {
#if LOGGING
        Log($"WriteAsStruct<T>()");
#endif
        if (!IsThereAlreadyASlot())
        {
            _slots.InsertAsStruct(_currentSlotIndex, value);
            return;
        }

        _slots.SetAsStruct(_currentSlotIndex, value);
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
    }

    public int GetCurrentElementIndex() => _currentElementIndex;

    public void EnterVisualElement()
    {
        _enteredElementIndices.Push(_currentElementIndex);
        _currentElementIndex = 0;
    }

    #endregion


    #region CompositionLocal

    public T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        return defaultValueFactory(); // BRUH
    }

    public void SetCompositionLocal(IImmutableStableList<CompositionLocalProvides> providers)
    {
        // BRUH
    }

    #endregion


    #region Restarting

    public void Clear()
    {
        _groups.Clear();
        _slots.Clear();
        _slotsAnchors.Clear();
        _groupsAnchors.Clear();
        _currentParentIndex = -1;
        _currentGroupIndex = 0;
        _currentSlotIndex = 0;
        _enteredParentsIndices.Clear();
    }

    public void ResetTo(int groupIndex)
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
        _currentElementIndex = group.ElementIndex;
        _currentSlotIndex = _slotsAnchors[group.DataAnchorId].Index;
        _currentParentIndex = -1;
    }

    public void ResetTo(AnchorId groupAnchor)
    {
        ResetTo(_groupsAnchors[groupAnchor].Index);
    }

    public void ResetToOutOfBounds()
    {
        _enteredParentsIndices.Clear();
        _invalidationRoot = -1;
        _currentGroupIndex = _groups.Count;
        _currentSlotIndex = _slots.Count;
        _currentParentIndex = -1;
    }

    internal ComposeGroup GetGroup(AnchorId groupAnchor)
    {
        return _groups[_groupsAnchors[groupAnchor].Index];
    }

    #endregion

    private AnchorId GetOrAllocateParentAnchorForReplaceGroup()
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
            return _groups.Count > 0;
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
            return _slots.Count > 0;
        var parent = CurrentParent();
        if (parent.SlotsSize == 0)
            return false;
        return _currentSlotIndex < _currentParentSlotIndex + parent.SlotsSize;
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
            currentParent = currentParent with { Size = newSize, SlotsSize = newSlotsSize };
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
        var i = 0;
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