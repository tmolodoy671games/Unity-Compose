using System;
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
    private readonly SlotTable _table = new();
    private ComposeGroup? _currentParent;
    private int _currentIndex;
    private int _currentElementIndex;

    public SlotWriter()
    {
        _currentParent = _table.Root;
        _currentIndex = 0;
    }

    private ComposeGroup CurrentParent => _currentParent.NotNull();

    #region Root

    public bool StartRootGroup(VisualElement element)
    {
        EnterReusableGroup(_table.Root);
        _currentElementIndex = 0;
        CurrentParent.CastTo<ReusableComposeGroup>().Element = element;
        return false;
    }

    public void EndRootGroup(Action restart)
    {
        var parent = CurrentParent.CastTo<ReusableComposeGroup>();
        parent.Restart = restart;
        RemoveChildren(_currentIndex, parent.Children.Count - _currentIndex);
        ExitReusableGroup(parent);
    }

    #endregion

    #region Reusable

    public bool StartReusableGroup<T>(int key, T state)
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        var matchingGroupIndex = FindMatchingGroupIndex(key);
        if (matchingGroupIndex >= 0)
        {
            var removedCount = RemoveChildren(_currentIndex, matchingGroupIndex - _currentIndex);
            _currentIndex = matchingGroupIndex - removedCount;
            var existingGroup = (ReusableComposeGroup<T>)currentParent.Children[_currentIndex];
            existingGroup.IndexInParent = _currentIndex;
            // if (EqualityUtils.FastEquals(existingGroup.PreviousState, state))
            // {
            //     existingGroup.ElementIndex = _currentElementIndex;
            //     SkipReusableGroup(existingGroup);
            // }
            // else
            // {
                existingGroup.PreviousState = state;
                existingGroup.ElementIndex = _currentElementIndex;
                EnterReusableGroup(existingGroup);
            // }

            return false;
        }

        var newGroup = new ReusableComposeGroup<T>(key, currentParent, state, this)
        {
            IndexInParent = _currentIndex
        };
        currentParent.Children.Insert(_currentIndex, newGroup);
        newGroup.ElementIndex = _currentElementIndex;
        EnterReusableGroup(newGroup);
        return false;
    }

    public void EndReusableGroup(Action restart)
    {
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

    #endregion

    #region Replaceable

    public void StartReplaceableGroup<TKey, TValue>(int key)
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        var matchingGroupIndex = FindMatchingGroupIndex(key);
        if (matchingGroupIndex >= 0)
        {
            var removedCount = RemoveChildren(_currentIndex, matchingGroupIndex - _currentIndex);
            _currentIndex = matchingGroupIndex - removedCount;
            var existingGroup = currentParent.Children[matchingGroupIndex];
            existingGroup.IndexInParent = _currentIndex;
            EnterReplaceableGroup(existingGroup);
            return;
        }

        var newGroup = new ReplaceableComposeGroup<TKey, TValue>(key, currentParent)
        {
            IndexInParent = _currentIndex
        };
        currentParent.Children.Insert(_currentIndex, newGroup);
        EnterReplaceableGroup(newGroup);
    }

    public TValue ReadValue<TKey, TValue>()
    {
        var currentParent = CurrentParent.CastTo<ReplaceableComposeGroup<TKey, TValue>>();
        return currentParent.Value;
    }

    public Optional<TKey> ReadAndSetKey<TKey, TValue>(TKey key)
    {
        var currentParent = CurrentParent.CastTo<ReplaceableComposeGroup<TKey, TValue>>();
        var result = currentParent.CacheKey;
        currentParent.CacheKey = key;
        return result;
    }

    public void Write<TKey, TValue>(TValue value)
    {
        var currentParent = CurrentParent.CastTo<ReplaceableComposeGroup<TKey, TValue>>();
        currentParent.Value = value;
    }

    private void EnterReplaceableGroup(ComposeGroup group)
    {
        _currentParent = group;
        _currentIndex = 0;
    }

    public void EndReplaceableGroup()
    {
        ExitReplaceableGroup(CurrentParent);
    }

    private void ExitReplaceableGroup(ComposeGroup group)
    {
        _currentParent = group.Parent;
        _currentIndex = group.IndexInParent + 1;
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
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        return currentParent;
    }

    public void Clear()
    {
        _table.Root.Children.Clear();
    }

    public void ResetTo(ReusableComposeGroup group)
    {
        Debug.Log($"ResetTo({group})");
        _currentParent = group.Parent;
        _currentIndex = group.IndexInParent;
        _currentElementIndex = group.ElementIndex;
        Debug.Log($"_currentParent={_currentParent}, _currentIndex={_currentIndex}, _currentElementIndex={_currentElementIndex}");
    }

    public void ResetToRoot()
    {
        ResetTo(_table.Root);
    }

    #endregion

    #region Common

    private int FindMatchingGroupIndex(int key)
    {
        var currentParent = CurrentParent.CastTo<ReusableComposeGroup>();
        var children = currentParent.Children;
        var childrenCount = children.Count;
        for (var i = _currentIndex; i < childrenCount; i++)
        {
            var group = children[i];
            if (group.Key == key)
                return i;
        }

        return -1;
    }

    private int RemoveChildren(int startIndex, int count)
    {
        if (count == 0)
            return 0;
        var parent = CurrentParent.CastTo<ReusableComposeGroup>();
        var children = parent.Children;
        for (var i = 0; i < count; i++)
            children[i].Dispose();
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
}