using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Core;

internal class SlotWriter
{
    private readonly SlotTable _table;
    private ComposeGroup? _currentParent;
    private int _currentIndex;
    private int _currentElementIndex;
    private ComposeGroup? _recompositionRoot;

    public SlotWriter()
    {
        _table = new SlotTable(this);
        _currentParent = _table.Root;
        _currentIndex = 0;
    }

    private ComposeGroup CurrentParent => _currentParent.NotNull();

    #region Root

    public bool StartRootGroup(VisualElement element)
    {
        Log($"StartRootGroup()");
        EnterReusableGroup(_table.Root);
        _currentElementIndex = 0;
        CurrentParent.CastTo<ReusableComposeGroup>().Element = element;
        return false;
    }

    public void EndRootGroup(Action restart)
    {
        Log("EndRootGroup()");
        var parent = CurrentParent.CastTo<ReusableComposeGroup>();
        parent.Restart = restart;
        RemoveChildren(_currentIndex, parent.Children.Count - _currentIndex);
        ExitReusableGroup(parent);
    }

    #endregion

    #region Reusable

    public bool StartReusableGroup<T>(int key, T state, object? objectKey)
    {
        Log($"StartReusableGroup({key})");
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        var matchingGroupIndex = FindMatchingReusableGroupIndex(key, objectKey);
        if (matchingGroupIndex >= 0)
        {
            var removedCount = RemoveChildren(_currentIndex, matchingGroupIndex - _currentIndex);
            if (removedCount != 0)
                Debug.Log($"{key}: Found existing group at {matchingGroupIndex} vs {_currentIndex}");
            _currentIndex = matchingGroupIndex - removedCount;
            var existingGroup = (ReusableComposeGroup<T>)currentParent.Children[_currentIndex];
            existingGroup.IndexInParent = _currentIndex;
            existingGroup.ElementIndex = _currentElementIndex;
            if (_recompositionRoot == null && EqualityUtils.FastEquals(existingGroup.PreviousState, state))
            {
                SkipReusableGroup(existingGroup);
                return true;
            }

            if (currentParent == _recompositionRoot)
                _recompositionRoot = null;
            existingGroup.PreviousState = state;
            EnterReusableGroup(existingGroup);

            return false;
        }

        var newGroup = new ReusableComposeGroup<T>(key, currentParent, state, this)
        {
            IndexInParent = _currentIndex
        };
        var children = currentParent.Children
            .Select(it => it.Key)
            .ToImmutableStableList();
        currentParent.Children.Insert(_currentIndex, newGroup);
        newGroup.ElementIndex = _currentElementIndex;
        EnterReusableGroup(newGroup);
        return false;
    }

    public void EndReusableGroup(Action restart)
    {
        Log($"EndReusableGroup({CurrentParent.Key})");
        var parent = CurrentParent.CastTo<ReusableComposeGroup>();
        parent.Restart = restart;
        RemoveChildren(_currentIndex, parent.Children.Count - _currentIndex);
        ExitReusableGroup(parent);
    }

    public VisualElement? GetVisualElement()
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        return currentParent.Element;
    }

    public void SetVisualElement(VisualElement element)
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        currentParent.ElementsCount = 1;
        currentParent.Element = element;
        _currentElementIndex = 0;
        foreach (var ancestor in currentParent.Ancestors())
        {
            if (ancestor.Element != null)
                break;
            ancestor.ElementsCount += 1;
        }
    }

    public int GetElementIndex()
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        return currentParent.ElementIndex;
    }

    private void EnterReusableGroup(ReusableComposeGroup group)
    {
        _currentParent = group;
        _currentIndex = 0;
        if (group.Element != null)
            _currentElementIndex = 0;
    }

    private void ExitReusableGroup(ReusableComposeGroup group)
    {
        _currentParent = group.Parent;
        _currentIndex = group.IndexInParent + 1;
        _currentElementIndex = group.ElementIndex + group.ElementsCount;
    }

    private void SkipReusableGroup(ReusableComposeGroup group)
    {
        _currentIndex++;
        _currentElementIndex += group.ElementsCount;
    }

    private int FindMatchingReusableGroupIndex(int key, object? objectKey)
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        var children = currentParent.Children;
        var childrenCount = children.Count;
        var hasCustomKey = objectKey != null;
        for (var i = _currentIndex; i < childrenCount; i++)
        {
            var group = children[i];
            var isSameGroup = group.Key == key &&
                              group is ReusableComposeGroup reusableGroup &&
                              (!hasCustomKey || objectKey!.Equals(reusableGroup.ObjectKey));
            if (isSameGroup)
                return i;
        }

        return -1;
    }

    #endregion

    #region Replaceable

    public void StartReplaceableGroup<TKey, TValue>(int key)
    {
        Log($"StartReplaceableGroup({key})");
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        var matchingGroupIndex = FindMatchingReplaceableGroupIndex(key);
        if (matchingGroupIndex >= 0)
        {
            var removedCount = RemoveChildren(_currentIndex, matchingGroupIndex - _currentIndex);
            _currentIndex = matchingGroupIndex - removedCount;
            var existingGroup = currentParent.Children[_currentIndex];

            existingGroup.IndexInParent = _currentIndex;
            if (key == 857455746)
                Debug.Log("Enter");
            EnterReplaceableGroup(existingGroup);
            return;
        }

        if (key == 857455746)
            Debug.Log(
                $"Insert 857455746 into {currentParent.Key} {currentParent.Children.Select(it => it.Key).ToImmutableStableList()}");
        var newGroup = new ReplaceableComposeGroup<TKey, TValue>(key, currentParent)
        {
            IndexInParent = _currentIndex
        };
        currentParent.Children.Insert(_currentIndex, newGroup);
        if (key == 857455746)
            Debug.Log(currentParent.Children.Select(it => it.Key).ToImmutableStableList());
        EnterReplaceableGroup(newGroup);
    }

    public TValue ReadValue<TKey, TValue>()
    {
        var currentParent = CurrentParent.CastTo<ReplaceableComposeGroup<TKey, TValue>>();
        return currentParent.Value;
    }

    public Optional<TKey> ReadKey<TKey, TValue>(TKey key)
    {
        var currentParent = CurrentParent.CastTo<ReplaceableComposeGroup<TKey, TValue>>();
        var result = currentParent.CacheKey;
        currentParent.CacheKey = key;
        return result;
    }

    public void SetKey<TKey, TValue>(TKey key)
    {
        var currentParent = CurrentParent.CastTo<ReplaceableComposeGroup<TKey, TValue>>();
        currentParent.CacheKey = key;
    }

    public void Write<TKey, TValue>(TValue value)
    {
        var currentParent = CurrentParent.CastTo<ReplaceableComposeGroup<TKey, TValue>>();
        currentParent.Value = value;
    }

    public int CurrentParentKey() => CurrentParent.Key;

    private void EnterReplaceableGroup(ComposeGroup group)
    {
        _currentParent = group;
        _currentIndex = 0;
    }

    public void EndReplaceableGroup()
    {
        Log($"EndReplaceableGroup({CurrentParent.Key})");
        ExitReplaceableGroup(CurrentParent);
    }

    private void ExitReplaceableGroup(ComposeGroup group)
    {
        _currentParent = group.Parent;
        _currentIndex = group.IndexInParent + 1;
    }

    private int FindMatchingReplaceableGroupIndex(int key)
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        var children = currentParent.Children;
        var childrenCount = children.Count;
        for (var i = _currentIndex; i < childrenCount; i++)
        {
            var group = children[i];
            if (group.Key == key && group is ReplaceableComposeGroup)
                return i;
        }

        return -1;
    }

    #endregion


    #region CompositionLocal

    public void UpdateCompositionLocal(IImmutableStableList<CompositionLocalProvides> provides)
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        currentParent.CompositionLocalMap.Update(provides);
    }

    public Optional<T> GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal)
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        return currentParent.CompositionLocalMap.Get(compositionLocal);
    }

    #endregion


    #region Restarting

    public ReusableComposeGroup? GetRestartScope()
    {
        return _currentParent?.CastToOrNull<ReusableComposeGroup>();
    }

    public void Clear()
    {
        _table.Root.Children.Clear();
    }

    public void ResetTo(ReusableComposeGroup group)
    {
        _currentParent = group.Parent;
        _currentIndex = group.IndexInParent;
        _currentElementIndex = group.ElementIndex;
        _recompositionRoot = group.Parent;
    }

    public void ResetToRoot()
    {
        ResetTo(_table.Root);
    }

    #endregion


    #region Common

    private int RemoveChildren(int startIndex, int count)
    {
        if (count == 0)
            return 0;
        var parent = CurrentParent.CastTo<ReusableComposeGroup>();
        var children = parent.Children;
        var endIndexNonInc = startIndex + count;
        for (var i = startIndex; i < endIndexNonInc; i++)
            children[i].Dispose();
        // if (parent.Key == 1729294312)
        Debug.Log($"{parent.Key}: Remove({startIndex}, {count})");
        children.RemoveRange(startIndex, count);
        return count;
    }

    #endregion


    public bool IsInCompositionContext()
    {
        return _currentParent != null;
    }

    public override string ToString()
    {
        return _table.ToString(_currentParent, _currentIndex);
    }

    private void Log(string message)
    {
        Debug.Log(message + $" _currentIndex={_currentIndex}, _currentParent={_currentParent}");
    }
}