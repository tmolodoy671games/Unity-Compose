// #define LOGGING
// #define ASSERTIONS

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

    private int _currentGroupIndex = 0;
    private int _currentParentIndex = -1;
    private int _currentSlotIndex = 0;
    private int _currentParentSlotIndex = -1;
    private int _invalidationRoot = -1;
    private int _currentElementIndex = 0;

    public SlotTableWriter(SlotTable.Models.SlotTable table)
    {
        _groups = new Groups(table.Groups);
        _slots = new Slots(table.Slots);
        _groupsAnchors = new Anchors(table.GroupsAnchors);
        _slotsAnchors = new Anchors(table.GroupsAnchors);
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
        _slots.InsertPreviousState(_currentSlotIndex, ComposeEmptySlot.Instance);
        _slots.InsertRestartScope(_currentSlotIndex, null);
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

    public void UpdatePreviousState<T>(T state)
    {
        _slots.SetPreviousState(_currentParentSlotIndex, state);
    }

    public void SkipToGroupEnd()
    {
        var parent = CurrentParent();
        _currentGroupIndex += parent.Size;
        _currentSlotIndex += parent.SlotsSize;
        _currentElementIndex += parent.ElementsCount;
    }

    public ComposeRestartScope? GetRestartScope()
    {
        return null; // BRUH
    }

    public ComposeRestartScope? RequireRestartScope()
    {
        return GetRestartScope();
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
        if (parent.Key == _invalidationRoot)
            _invalidationRoot = -1;
        ExitGroup(parent);
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
            if (existingGroup.Type != ComposeGroupType.Restart)
                throw new InvalidOperationException("Found Restart group instead of replace group!");
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
        if (_currentSlotIndex == _slots.Count - 1)
            return Optional.Empty<T>();
        return _slots.GetAsMutableState<T>(_currentSlotIndex);
    }

    public void Write<T>(T value)
    {
#if LOGGING
        Log($"Write<T>()");
#endif
        if (_currentSlotIndex >= _slots.Count)
        {
            _slots.InsertAsMutableState(_currentSlotIndex, value);
            return;
        }

        _slots.SetAsMutableState(_currentSlotIndex, value);
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
        _invalidationRoot = groupIndex;
        _currentGroupIndex = groupIndex;
        _currentElementIndex = group.ElementIndex;
        _currentSlotIndex = _slotsAnchors[group.AnchorId].Index;
        _currentParentIndex = group.ParentAnchorId.IsValid ? _groupsAnchors[group.ParentAnchorId].Index : -1;
        // if (group.ParentAnchorId.IsValid)
        //     _enteredParentsIndices.Push(_groupsAnchors[group.ParentAnchorId].Index);
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
        if (_currentParentIndex < 0)
            return false;
        var parent = CurrentParent();
        if (parent.Size == 0)
            return false;
        return _currentGroupIndex < _currentParentIndex + parent.Size;
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

        _enteredParentsIndices.Pop();
        _enteredParentsSlotIndices.Pop();
        _currentParentIndex = _enteredParentsIndices.PeekOrDefault(-1);
        _currentParentSlotIndex = _enteredParentsSlotIndices.PeekOrDefault(-1);

        if (_enteredParentsIndices.Count == 0)
        {
            ShiftGroupsAnchors(_currentGroupIndex + 1, groupSizeOffset);
            ShiftSlotsAnchors(_currentSlotIndex + 1, slotsSizeOffset);
            ShiftAncestorsGroupSizes(groupSizeOffset);
            ShiftAncestorsSlotSizes(slotsSizeOffset);
            ShiftAncestorsElementsCounts(elementsCountOffset);
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"ENTERED_PARENTS: [{_enteredParentsIndices.JoinToString()}]");
        builder.AppendLine();
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
        while (ancestorIndex >= 0)
        {
            var ancestor = _groups[ancestorIndex];
            ancestor = ancestor with { Size = ancestor.Size + offset };
            _groups[ancestorIndex] = ancestor;
            ancestorIndex = _groupsAnchors[ancestor.ParentAnchorId].Index;
        }
    }

    private void ShiftAncestorsSlotSizes(int offset)
    {
        if (offset == 0) return;
        var ancestorIndex = _currentParentIndex;
        while (ancestorIndex >= 0)
        {
            var ancestor = _groups[ancestorIndex];
            ancestor = ancestor with { SlotsSize = ancestor.SlotsSize + offset };
            _groups[ancestorIndex] = ancestor;
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
            ancestorIndex = _groupsAnchors[ancestor.ParentAnchorId].Index;
        }
    }

    private void Log(string message)
    {
        Debug.Log(message + "\n" + ToString());
    }
}