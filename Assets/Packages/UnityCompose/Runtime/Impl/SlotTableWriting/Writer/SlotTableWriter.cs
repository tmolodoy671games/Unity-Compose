// #define LOGGING

#define ASSERTIONS
#define PARENT_ANCHORS_FOR_EVERYONE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharpExtensions;
using StableCollections;
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
    private readonly List<ComposeGroupOffset> _pendingOffsets = new();
    private readonly Stack<ComposeGroupEntry> _enteredModifierGroups = new();
    private readonly Stack<ComposeGroupEntry> _enteredKeyGroups = new();

    private readonly Stack<VisualElement> _enteredElements = new();
    private VisualElement? _rootVisualElement;

    private readonly Stack<ModifiersPair> _enteredModifiersPairs = new();
    private ModifiersStatePair? _rootModifiers;

    private readonly Stack<CompositionLocalMapEntry> _enteredCompositionLocalMaps = new();
    private readonly List<ICompositionLocalProviders> _enteredProvides = new();
    private CompositionLocalMap? _rootCompositionLocalMap = null;

    private int _currentGroupIndex = 0;
    private int _currentParentIndex = -1;
    private int _currentSlotIndex = 0;
    private int _currentParentSlotIndex = -1;
    private int _invalidationRoot = -1;
    private int _currentElementIndex = 0;
    private int _alreadyRemovedGroups = 0;
    private int _alreadyRemovedSlots = 0;

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
#if ASSERTIONS
            if (existingGroup.Key != key)
                throw new InvalidOperationException($"Found {existingGroup.Key} instead of {key}!");
            if (existingGroup.Type != ComposeGroupType.Restart)
                throw new InvalidOperationException($"Found {existingGroup.Type} instead of RestartGroup!");
#endif
            _enteredRestartGroups.Push(
                new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex)
            );
            EnterGroup(existingGroup);
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
                AnchorId = _groupsAnchors.AllocateAnchor((enteredRestartGroupIndex))
            };
            _groups[enteredRestartGroupIndex] = restartGroup;
        }

        if (!restartGroup.DataAnchorId.IsValid)
        {
            restartGroup = restartGroup with
            {
                DataAnchorId = _slotsAnchors.AllocateAnchor((enteredRestartGroupSlotIndex))
            };
            _groups[enteredRestartGroupIndex] = restartGroup;
        }

        restartScope = ComposeRestartScope.Get(
            groupAnchor: restartGroup.AnchorId,
            writer: this,
            compositionLocalMap: RequireCompositionLocalMap(),
            element: GetParentVisualElement(),
            modifiers: RequireModifiersStatePair()
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
                throw new InvalidOperationException(
                    $"Found {existingGroup.Type}({existingGroup.Key}) group instead of replace group!");
#endif
            if (existingGroup.Key != key)
            {
                existingGroup = existingGroup with { Key = key };
                _groups[_currentGroupIndex] = existingGroup;
            }

            EnterGroup(existingGroup);
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Replace,
            ParentAnchorId: GetOrAllocateParentAnchorForNonRestartGroup(),
            Size: 1,
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
#if ASSERTIONS
        if (parent.Key != key)
            throw new InvalidOperationException($"Mismatching ending group key: {key} vs {parent.Key}!");
#endif
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
#if ASSERTIONS
            if (existingGroup.Key != key)
                throw new InvalidOperationException($"Found {existingGroup.Key} instead of {key}!");
            if (existingGroup.Type != ComposeGroupType.Reusable)
                throw new InvalidOperationException($"Found {existingGroup.Type} group instead of Reusable group!");
#endif
            EnterGroup(existingGroup);
            _currentSlotIndex += ReusableGroup.MetadataSize;
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Reusable,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 1,
            SlotsSize: 1,
            AnchorId: AnchorId.None,
            DataAnchorId: _slotsAnchors.AllocateAnchor(_currentSlotIndex),
            ElementIndex: _currentElementIndex,
            ElementsCount: 1,
            ContainsKeyGroups: false,
            CalledThisComposition: true
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertVisualElement(_currentSlotIndex);
        EnterGroup(newGroup);
        _currentSlotIndex += ReusableGroup.MetadataSize;
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
        _enteredElements.Pop();
    }

    #endregion


    #region Key Group

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

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Movable,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 1,
            SlotsSize: MovableGroup.MetadataSize,
            AnchorId: AnchorId.None,
            DataAnchorId: _slotsAnchors.AllocateAnchor((_currentSlotIndex)),
            ElementIndex: _currentElementIndex,
            ElementsCount: 0,
            ContainsKeyGroups: false,
            CalledThisComposition: true
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertKey(_currentSlotIndex, dataKey);
        EnterGroup(newGroup);
        _currentSlotIndex += MovableGroup.MetadataSize;
    }

    public void EndMovableGroup(int key)
    {
        var currentParent = CurrentParent();
#if ASSERTIONS
        if (currentParent.Key != key)
            throw new InvalidOperationException($"Mismatching ending group key: {key} vs {currentParent.Key}!");
#endif
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
        var endIndex = _currentParentIndex + parent.Size;
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
        // Debug.Log($"{key}, {dataKey}: {existingGroupIndex}");
        if (existingGroupIndex < 0)
            return false;
        if (existingGroupIndex == _currentGroupIndex)
            return true;
        var existingGroup = _groups[existingGroupIndex];
        var existingGroupSlotIndex = LogicalSlotIndex(_groups[existingGroupIndex].DataAnchorId);
        var currentGroup = _groups[_currentGroupIndex];

        // FileLog("1. Before Move");

        _groups.Swap(
            sourceIndex: existingGroupIndex,
            sourceCount: existingGroup.Size,
            targetIndex: _currentGroupIndex,
            targetCount: currentGroup.Size
        );
        // FileLog("2. After Move Groups");

        // Debug.Log($"SlotsMove({existingGroupSlotIndex}, {_currentSlotIndex}, {existingGroup.SlotsSize})");
        _slots.Swap(
            sourceIndex: existingGroupSlotIndex,
            sourceCount: existingGroup.SlotsSize,
            targetIndex: _currentSlotIndex,
            targetCount: currentGroup.SlotsSize
        );
        // FileLog("2. After Move Slots");

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

        if (_slots[_currentSlotIndex] is IDisposable existingDisposable)
            existingDisposable.Dispose();
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

        if (_slots[_currentSlotIndex] is IDisposable existingDisposable)
            existingDisposable.Dispose();
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
#if ASSERTIONS
            if (existingGroup.Type != ComposeGroupType.Local)
                throw new InvalidOperationException($"Found {existingGroup.Type} group instead of local group!");
            if (existingGroup.Key != key)
                throw new InvalidOperationException($"Found {existingGroup.Key} group instead of {key}!");
#endif
            _enteredLocalGroups.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
            EnterGroup(existingGroup);
            _currentSlotIndex += LocalGroup.MetadataSize;
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Local,
            ParentAnchorId: GetOrAllocateParentAnchorForNonRestartGroup(),
            Size: 1,
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
        _slots.InsertCompositionLocalMap(_currentSlotIndex);
        EnterGroup(newGroup);
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
        var map = GetCurrentLocalGroupCompositionLocalMap();
        if (map != null)
            _enteredCompositionLocalMaps.Pop();
        else if (_enteredProvides.IsNotEmpty())
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
        return map.Get(compositionLocal, defaultValueFactory);
    }

    public void SetCompositionLocal(ICompositionLocalProviders providers)
    {
        var map = GetCurrentLocalGroupCompositionLocalMap();
        if (map != null)
        {
            providers.Apply(map);
            _enteredCompositionLocalMaps.Push(new CompositionLocalMapEntry(_enteredLocalGroups.Peek().GroupIndex, map));
            return;
        }

        _enteredProvides.Add(providers);
    }

    private CompositionLocalMap? GetCurrentLocalGroupCompositionLocalMap()
    {
        if (_enteredLocalGroups.IsEmpty())
            return null;
        return _slots.GetCompositionLocalMap(_enteredLocalGroups.Peek().SlotIndex);
    }

    private CompositionLocalMap? RequireCompositionLocalMap()
    {
        var map = GetCurrentLocalGroupCompositionLocalMap();
        if (map != null)
            return map;
        if (map == null && _enteredProvides.IsEmpty())
            return _rootCompositionLocalMap;
        var (groupIndex, slotIndex) = _enteredLocalGroups.Peek();
        var parentDictionary = _enteredCompositionLocalMaps.IsNotEmpty()
            ? _enteredCompositionLocalMaps.Peek().Map
            : _rootCompositionLocalMap;
        map = parentDictionary != null
            ? parentDictionary.Copy()
            : CompositionLocalMap.Get();
        // Log("RequireCompositionLocalMap: " + map.GetHashCode());
        foreach (var provider in _enteredProvides)
            provider.Apply(map);
        _slots.SetCompositionLocalMap(slotIndex, map);
        _enteredProvides.RemoveAt(_enteredProvides.Count - 1);
        _enteredCompositionLocalMaps.Push(new CompositionLocalMapEntry(groupIndex, map));
        return map;
    }

    #endregion


    #region Modifiers

    public void StartModifierGroup(int key)
    {
#if LOGGING
        Log($"StartModifierGroup({key})");
#endif
        if (IsThereAlreadyAGroup())
        {
            var existingGroup = _groups[_currentGroupIndex];
#if ASSERTIONS
            if (existingGroup.Key != key)
                throw new InvalidOperationException($"Found {existingGroup.Key} instead of {key}!");
            if (existingGroup.Type != ComposeGroupType.Modifier)
                throw new InvalidOperationException($"Found {existingGroup.Type} group instead of Modifier group!");
#endif
            _enteredModifierGroups.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
            EnterGroup(existingGroup);
            _currentSlotIndex += ModifierGroup.MetadataSize;
            return;
        }

        var newGroup = new ComposeGroup(
            Key: key,
            Type: ComposeGroupType.Modifier,
            ParentAnchorId: GetOrAllocateParentAnchor(),
            Size: 1,
            SlotsSize: 1,
            AnchorId: AnchorId.None,
            DataAnchorId: AnchorId.None,
            ElementIndex: _currentElementIndex,
            ElementsCount: 1,
            ContainsKeyGroups: false,
            CalledThisComposition: true
        );
        _enteredModifierGroups.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
        _groups.Insert(_currentGroupIndex, newGroup);
        _slots.InsertModifiersStatePair(_currentSlotIndex);
        EnterGroup(newGroup);
        _currentSlotIndex += ModifierGroup.MetadataSize;
    }

    public void EndModifierGroup(int key)
    {
#if LOGGING
        Log($"EndModifierGroup({key})");
#endif
        var parent = CurrentParent();
#if ASSERTIONS
        if (parent.Key != key)
            throw new InvalidOperationException($"Mismatching ending group key: {key} vs {parent.Key}!");
#endif
        ExitGroup(parent);
        _enteredModifierGroups.Pop();
    }

    public void PushModifiers(IModifier? before, IModifier? after)
    {
        _enteredModifiersPairs.Push(new ModifiersPair(before, after));
        var slotIndex = _enteredModifierGroups.Peek().SlotIndex;
        var pair = _slots.GetModifiersStatePair(slotIndex);
        pair?.Update(new ModifiersPair(before, after));
    }

    public ModifiersPair GetModifiers()
    {
        if (_enteredModifierGroups.IsEmpty())
            return _rootModifiers != null ? _rootModifiers.ToModifiersPair() : new ModifiersPair();
        var slotIndex = _enteredModifierGroups.Peek().SlotIndex;
        var pair = _slots.GetModifiersStatePair(slotIndex);
        if (pair == null)
        {
            pair = new ModifiersStatePair();
            pair.Update(_enteredModifiersPairs.Peek());
            _slots.SetModifiersStatePair(slotIndex, pair);
        }

        return pair.ToModifiersPair();
    }

    private ModifiersStatePair? RequireModifiersStatePair()
    {
        if (_enteredModifierGroups.IsEmpty())
            return _rootModifiers;
        var slotIndex = _enteredModifierGroups.Peek().SlotIndex;
        var pair = _slots.GetModifiersStatePair(slotIndex);
        if (pair == null)
        {
            pair = new ModifiersStatePair();
            _slots.SetModifiersStatePair(slotIndex, pair);
        }

        return pair;
    }

    #endregion


    #region Restarting

    public void Clear()
    {
        _groups.Clear();
        _slots.Clear();
        _groupsAnchors.Clear();
        _slotsAnchors.Clear();

        _enteredParents.Clear();
        _enteredElementIndices.Clear();
        _enteredRestartGroups.Clear();
        _enteredLocalGroups.Clear();
        _enteredModifierGroups.Clear();
        _pendingOffsets.Clear();

        _enteredCompositionLocalMaps.Clear();
        _enteredProvides.Clear();
        _enteredElements.Clear();
        _enteredModifiersPairs.Clear();
        _rootVisualElement = null;
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

    public void ResetTo(
        int groupIndex,
        CompositionLocalMap? compositionLocalMap,
        VisualElement? element,
        ModifiersStatePair? modifiers
    )
    {
#if LOGGING
        Log($"ResetTo({groupIndex})");
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
        _alreadyRemovedGroups = 0;
        _alreadyRemovedSlots = 0;
        _rootVisualElement = element;
        _rootModifiers = modifiers;

        _enteredParents.Clear();
        _enteredElementIndices.Clear();
        _enteredRestartGroups.Clear();
        _enteredLocalGroups.Clear();
        _enteredModifierGroups.Clear();

        _enteredCompositionLocalMaps.Clear();
        _enteredProvides.Clear();
        _pendingOffsets.Clear();
        _enteredElements.Clear();
        _enteredModifiersPairs.Clear();
    }

    public void ResetTo(
        AnchorId groupAnchor,
        CompositionLocalMap? compositionLocalMap,
        VisualElement? element,
        ModifiersStatePair? modifiers
    )
    {
        if (!groupAnchor.IsValid)
            return;
        var anchor = _groupsAnchors[groupAnchor];
        if (!anchor.IsValid)
            return;
        _composer.SetAsCurrentComposer();
        ResetTo(LogicalGroupIndex(anchor.Location), compositionLocalMap, element, modifiers);
    }

    public void ReleaseCurrentComposer()
    {
        _composer.ResetAsCurrentComposer();
    }

    #endregion


    #region Utils

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
        var newAnchor = _groupsAnchors.AllocateAnchor((_currentParentIndex));
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
        return _currentGroupIndex < _currentParentIndex + parent.Size +
            _pendingOffsets.GetOrDefault(_pendingOffsets.Count - 1, new ComposeGroupOffset(0, 0)).GroupOffset;
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
        return _currentSlotIndex < _currentParentSlotIndex + parent.SlotsSize +
            _pendingOffsets.GetOrDefault(_pendingOffsets.Count - 1, new ComposeGroupOffset(0, 0)).SlotOffset;
    }

    private void EnterGroup(ComposeGroup group)
    {
        SyncElementIndex(group, canReinsert: true);
        _currentParentIndex = _currentGroupIndex;
        _enteredParents.Push(new ComposeGroupEntry(_currentGroupIndex, _currentSlotIndex));
        _pendingOffsets.Add(new ComposeGroupOffset(0, 0));
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
            CleanupGroups(_currentGroupIndex, currentGroupSizeOffset);
            _groups.RemoveRange(_currentGroupIndex, currentGroupSizeOffset);
            _alreadyRemovedGroups += currentGroupSizeOffset;
        }

        if (slotsSizeOffset < 0)
        {
            var currentSlotsSizeOffset = -slotsSizeOffset - _alreadyRemovedSlots;
#if LOGGING
            Log($"_slots.RemoveRange({_currentSlotIndex}, {currentSlotsSizeOffset})");
#endif
            CleanupSlots(_currentSlotIndex, currentSlotsSizeOffset);
            _slots.RemoveRange(_currentSlotIndex, currentSlotsSizeOffset);
            _alreadyRemovedSlots += currentSlotsSizeOffset;
        }

        if (_enteredParents.Count == 1)
        {
            ShiftAncestorsGroupSizes(groupSizeOffset);
            ShiftAncestorsSlotSizes(slotsSizeOffset);
            ShiftAncestorsElementsCounts(elementsCountOffset);
            _alreadyRemovedGroups = 0;
            _alreadyRemovedSlots = 0;
        }

        _enteredParents.Pop();
        var newParent = _enteredParents.PeekOrDefault(new ComposeGroupEntry(-1, -1));
        _currentParentIndex = newParent.GroupIndex;
        _currentParentSlotIndex = newParent.SlotIndex;
        _pendingOffsets.RemoveAt(_pendingOffsets.Count - 1);
        if (_pendingOffsets.IsNotEmpty())
        {
            var oldOffsets = _pendingOffsets[^1];
            _pendingOffsets.RemoveAt(_pendingOffsets.Count - 1);
            _pendingOffsets.Add(
                new ComposeGroupOffset(
                    oldOffsets.GroupOffset + groupSizeOffset,
                    oldOffsets.SlotOffset + slotsSizeOffset
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

        builder.AppendLine("Slots:");
        builder.AppendLine(_slots.ToString(_currentSlotIndex));

        builder.AppendLine("Groups Anchors:");
        builder.AppendLine(_groupsAnchors.ToString());
        builder.AppendLine("Slots Anchors:");
        builder.AppendLine(_slotsAnchors.ToString());

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
        for (var i = startIndex; i < startIndex + count; i++)
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
        for (var i = startIndex; i < startIndex + count; i++)
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

    private void Log(object? message)
    {
        var formattedMessage = message + "\n\n" + ToString();
        Debug.Log(formattedMessage);
    }

    private void FileLog(string fileName)
    {
        var formattedMessage = fileName + ":\n\n" + ToString();
        Debug.Log(formattedMessage);
        SimpleLogger.ReplaceLog(fileName, formattedMessage);
    }

    #endregion
}

internal readonly record struct ComposeGroupEntry(
    int GroupIndex,
    int SlotIndex
);

internal readonly record struct CompositionLocalMapEntry(
    int GroupIndex,
    CompositionLocalMap Map
);

internal readonly record struct ComposeGroupOffset(
    int GroupOffset,
    int SlotOffset
);

public static class SimpleLogger
{
    private static readonly string logFilePath = Path.Combine(Application.dataPath, "logs.txt");

    private static readonly string divider =
        "\n\n-----------------------------------------------------------------------------------------------------------------\n\n";

    public static void Log(object? message)
    {
        // Check if file exists
        if (!File.Exists(logFilePath))
        {
            File.WriteAllText(logFilePath, message?.ToString() ?? "");
        }
        else
        {
            File.AppendAllText(logFilePath, divider + message);
        }
    }

    public static void ReplaceLog(string fileName, object? message)
    {
        File.WriteAllText(Path.Combine(Application.dataPath, fileName + ".txt"), message?.ToString() ?? "");
    }
}