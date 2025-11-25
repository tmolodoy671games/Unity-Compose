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
    private enum GroupType
    {
        Reusable,
        Replaceable,
    }

    private readonly List<ComposeGroup> _groups;
    private readonly Stack<CompositionLocalMap> _compositionLocalMaps = new();

    private int _currentParentIndex = -1;
    private int _currentGroupIndex;
    private int _currentElementIndex;


    public SlotWriter(SlotTable table)
    {
        _groups = table.Groups;
        _currentGroupIndex = 0;
    }

    public int CurrentGroupIndex => _currentGroupIndex;
    public int ParentGroupIndex => _currentParentIndex;
    public bool IsInCompositionContext => _currentParentIndex != -1;

    private ComposeGroup ParentGroup
    {
        get
        {
            try
            {
                return _groups[_currentParentIndex];
            }
            catch (Exception e)
            {
                Debug.LogError($"{_currentParentIndex} vs {_groups.Count}");
                throw e;
            }
        }
    }

    #region Reusable Group

    public void StartReusableGroup<T>(int key, T state, VisualElement? element = null)
    {
        _currentGroupIndex = FindMatchingKeyIndex<T, T>(key, GroupType.Reusable);

        var currentGroup = _currentGroupIndex < _groups.Count
            ? _groups[_currentGroupIndex]
            : Optional.Empty<ComposeGroup>();
        if (currentGroup.HasValue && currentGroup.Value.IsSameReusableGroup<T>(key))
        {
            EnterReusableGroup();
            return;
        }

        // Write new group
        var newGroup = new ComposeGroup(
            ParentIndex: ParentGroupIndex,
            Key: key,
            Size: 1,
            State: new IComposeGroupState.Reusable<T>(this, state)
            {
                Element = element
            }
        );
        _groups.Insert(_currentGroupIndex, newGroup);
        EnterReusableGroup();
    }

    public void EndReusableGroup(Action restart)
    {
        var parentGroupIndex = _currentParentIndex;
        var parentGroup = ParentGroup;
        var newSize = _currentGroupIndex - parentGroupIndex;
        var oldSize = parentGroup.Size;

        var currentGroupState = ParentGroup.State.CastTo<IComposeGroupState.Reusable>();
        currentGroupState.RestartScope.GroupIndex = parentGroupIndex;
        currentGroupState.RestartScope.Restart = restart;

        if (currentGroupState.Element != null)
            _currentElementIndex = currentGroupState.ElementIndex + 1;

        var newElementsCount = _currentElementIndex - currentGroupState.ElementIndex;
        var anyFieldChanged = newSize != oldSize;
        if (anyFieldChanged)
        {
            parentGroup = parentGroup with
            {
                Size = newSize
            };
            _groups[parentGroupIndex] = parentGroup;
        }

        currentGroupState.ElementsCount = newElementsCount;
        
        _currentParentIndex = _groups[_currentParentIndex].ParentIndex;
    }

    private void EnterReusableGroup()
    {
        _currentElementIndex = _groups[_currentGroupIndex].State.CastTo<IComposeGroupState.Reusable>().ElementIndex;
        _currentParentIndex = _currentGroupIndex;
        _currentGroupIndex++;
    }

    #endregion

    #region Replaceable Group

    public void StartReplaceableGroup<TKey, TValue>(int key)
    {
        _currentGroupIndex = FindMatchingKeyIndex<TKey, TValue>(key, GroupType.Replaceable);

        var currentGroup = _currentGroupIndex < _groups.Count
            ? _groups[_currentGroupIndex]
            : Optional.Empty<ComposeGroup>();
        if (currentGroup.HasValue && currentGroup.Value.Key == key)
        {
            EnterReplaceableGroup();
            return;
        }

        var newGroup = new ComposeGroup(
            ParentIndex: ParentGroupIndex,
            Key: key,
            Size: 1,
            State: new IComposeGroupState.Replaceable<TKey, TValue>()
        );
        _groups.Insert(_currentGroupIndex, newGroup);

        EnterReplaceableGroup();
    }

    private void EnterReplaceableGroup()
    {
        _currentParentIndex = _currentGroupIndex;
        _currentGroupIndex++;
    }

    public IComposeGroupState.Replaceable<TKey, TValue> Read<TKey, TValue>()
    {
        var existingValue = ParentGroup;
        return existingValue.State.CastTo<IComposeGroupState.Replaceable<TKey, TValue>>();
    }

    public void Write<TKey, TValue>(TValue value)
    {
        var rememberedValue = ParentGroup.State.CastTo<IComposeGroupState.Replaceable<TKey, TValue>>();
        rememberedValue.Value = value;
    }

    public void EndReplaceableGroup()
    {
        _currentParentIndex = _groups[_currentParentIndex].ParentIndex;
    }

    #endregion

    #region Elements

    public int GetElementIndex()
    {
        var currentGroup = ParentGroup;
        return currentGroup.State.CastTo<IComposeGroupState.Reusable>().ElementIndex;
    }

    public TVisualElement? ReadVisualElement<TVisualElement>() where TVisualElement : VisualElement =>
        ParentGroup.State.CastTo<IComposeGroupState.Reusable>().Element as TVisualElement;

    public void WriteVisualElement<TVisualElement>(TVisualElement element) where TVisualElement : VisualElement =>
        ParentGroup.State.CastTo<IComposeGroupState.Reusable>().Element = element;

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
        var metadata = ParentGroup.State.CastTo<IComposeGroupState.Reusable>();
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
        _currentElementIndex = group.State.CastTo<IComposeGroupState.Reusable>().ElementIndex;
    }

    public ComposeGroupRestartScope? GetRestartScope()
    {
        if (!IsInCompositionContext)
            return null;
        return ParentGroup.State.CastTo<IComposeGroupState.Reusable>().RestartScope;
    }

    #endregion

    private int FindMatchingKeyIndex<T1, T2>(int key, GroupType groupType)
    {
        var parentGroupIndex = ParentGroupIndex;
        if (parentGroupIndex < 0)
            return _currentGroupIndex;
        var parent = ParentGroup;
        var maxIndex = parentGroupIndex + parent.Size - 1;
        if (maxIndex >= _groups.Count)
        {
            Debug.LogError($"Something went wrong: maxIndex={maxIndex}, groups.count={_groups.Count}");
        }
        var removeCount = 0;
        for (var i = _currentGroupIndex; i <= maxIndex;)
        {
            var group = _groups[i];
            var isMatching = groupType switch
            {
                GroupType.Reusable => group.IsSameReusableGroup<T1>(key),
                GroupType.Replaceable => group.IsSameReplaceableGroup<T1, T2>(key),
                _ => throw new ArgumentOutOfRangeException(nameof(groupType), groupType, null)
            };
            if (isMatching)
            {
                return i;
            }

            removeCount++;
            i += group.Size;
        }

        return _currentGroupIndex;
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