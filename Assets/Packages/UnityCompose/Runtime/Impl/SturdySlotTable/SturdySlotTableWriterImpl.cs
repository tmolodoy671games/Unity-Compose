using System;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable;

internal class SturdySlotTableWriterImpl : ISlotTableWriter
{
    private readonly SturdyComposeGroup _root = SturdyComposeGroup.Get(
        key: 123,
        type: SturdyComposeGroupType.Replace,
        parent: null
    );

    private readonly Composer _composer;
    private SturdyComposeGroup? _currentParent;
    private int _currentGroupIndex;
    private int _currentSlotIndex;
    private int _currentElementIndex;
    private SturdyComposeGroup? _invalidationRoot;

    private readonly IMutableStableStack<SturdyComposeGroup> _enteredRestartGroups =
        MutableStableStackOf<SturdyComposeGroup>();

    private readonly IMutableStableStack<SturdyComposeGroup> _enteredReusableGroups =
        MutableStableStackOf<SturdyComposeGroup>();

    private readonly IMutableStableStack<SturdyComposeGroup> _enteredLocalGroups =
        MutableStableStackOf<SturdyComposeGroup>();

    private readonly IMutableStableStack<int> _enteredGroupIndices = MutableStableStackOf<int>();
    private readonly IMutableStableStack<int> _enteredSlotIndices = MutableStableStackOf<int>();
    private readonly IMutableStableStack<VisualElement> _enteredElements = MutableStableStackOf<VisualElement>();
    private readonly IMutableStableStack<int> _enteredElementIndices = MutableStableStackOf<int>();

    public SturdySlotTableWriterImpl(Composer composer)
    {
        _currentParent = _root;
        _composer = composer;
    }

    private SturdyComposeGroup RequireCurrentParent() => _currentParent.NotNull();

    #region Restart Group

    public bool StartRestartGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"StartRestartGroup({key})");
        if (!EnterOrCreateGroup(key, SturdyComposeGroupType.Restart))
        {
            var parent = RequireCurrentParent();
            parent.GetMetadata<SturdyComposeRestartScope>().GroupIndex = _currentGroupIndex;
            _enteredRestartGroups.Push(parent);
            return false;
        }

        var currentParent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get(
            key: key,
            type: SturdyComposeGroupType.Restart,
            parent: currentParent
        );

        var newScope = SturdyComposeRestartScope.Get(
            group: newGroup,
            writer: this,
            visualElement: _enteredElements.PeekOrNull(),
            localGroup: _enteredLocalGroups.PeekOrNull()
        );
        newScope.GroupIndex = _currentGroupIndex;
        newGroup.Metadata = newScope;

        currentParent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        _enteredRestartGroups.Push(newGroup);
        return true;
    }

    public bool IsInInvalidationRoot() => _invalidationRoot == _currentParent;

    public void SkipToGroupEnd()
    {
        if (ComposeConstants.Logging)
            Log($"SkipToGroupEnd()");
        var parent = RequireCurrentParent();
        _currentElementIndex += parent.ElementsCount;
        _currentGroupIndex = parent.ChildrenCount;
        _currentSlotIndex = parent.SlotsCount;
    }

    public IComposeRestartScope? GetRestartScope()
    {
        if (_enteredRestartGroups.IsEmpty())
            return null;
        var enteredRestartGroup = _enteredRestartGroups.Peek();
        try
        {
            return enteredRestartGroup.GetMetadata<IComposeRestartScope>();
        }
        catch (Exception)
        {
            Debug.LogWarning($"Getting restart scope of {enteredRestartGroup.Key}: {enteredRestartGroup.Slots}");
            throw;
        }
    }

    public IComposeRestartScope? RequireRestartScope()
    {
        return GetRestartScope();
    }

    public void EndRestartGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"EndRestartGroup({key})");
        ExitGroup(key);
        _enteredRestartGroups.Pop();
    }

    #endregion

    #region Replace Group

    public void StartReplaceGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"StartReplaceGroup({key})");
        if (!EnterOrCreateGroup(key, SturdyComposeGroupType.Replace))
            return;

        var currentParent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get(
            key: key,
            type: SturdyComposeGroupType.Replace,
            parent: currentParent
        );
        currentParent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        return;
    }

    public void EndReplaceGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"EndReplaceGroup({key})");
        ExitGroup(key);
    }

    #endregion

    #region Reusable Group

    public void StartReusableGroup<T>(int key) where T : VisualElement, new()
    {
        if (ComposeConstants.Logging)
            Log(
                $"StartReusableGroup({key}): {GetParentVisualElement()?.Format()}: {_currentElementIndex}: {_enteredElementIndices}");
        if (!EnterOrCreateGroup(key, SturdyComposeGroupType.Reusable))
        {
            _enteredReusableGroups.Push(RequireCurrentParent());
            return;
        }

        var currentParent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get(
            key: key,
            type: SturdyComposeGroupType.Reusable,
            parent: currentParent
        );
        newGroup.ElementsCount = 1;
        newGroup.Metadata = ReusableComposeNode.Get<T>();
        currentParent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        _enteredReusableGroups.Push(newGroup);
        IncrementElementsCountRecursively(newGroup, 1);
    }

    public ReusableComposeNode<T> GetReusableNode<T>() where T : VisualElement, new()
    {
        var enteredReusableGroup = _enteredReusableGroups.Peek();
        return enteredReusableGroup.GetMetadata<ReusableComposeNode<T>>();
    }

    public VisualElement? GetParentVisualElement()
    {
        return _enteredElements.PeekOrNull();
    }

    public int GetCurrentElementIndex()
    {
        return _currentElementIndex;
    }

    public void WriteVisualElement(ComposeView visualElement)
    {
        GetReusableNode<ComposeView>().VisualElement = visualElement;
    }

    public void EnterVisualElement(VisualElement visualElement)
    {
        _enteredElements.Push(visualElement);
        _enteredElementIndices.Push(_currentElementIndex);
        _currentElementIndex = 0;
    }

    public void EndReusableGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log(
                $"EndReusableGroup({key}): {GetParentVisualElement()?.Format()}: {_currentElementIndex}: {_enteredElementIndices}");
        ExitGroup(key);
        _enteredElements.Pop();
        _currentElementIndex = _enteredElementIndices.PeekOrDefault(0);
        _enteredElementIndices.Pop();
        _currentElementIndex++;
    }

    #endregion

    #region Movable Group

    public void StartMovableGroup<T>(int key, T dataKey)
    {
        if (ComposeConstants.Logging)
            Log($"StartMovableGroup({key}");
        if (!EnterOrCreateMovableGroup(key, dataKey))
        {
            return;
        }

        var currentParent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get(
            key: key,
            type: SturdyComposeGroupType.Movable,
            parent: currentParent
        );
        newGroup.Metadata = dataKey;
        currentParent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
    }

    public void StartMovableGroup(int key)
    {
        throw new NotImplementedException();
    }

    public void EndMovableGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"EndMovableGroup({key})");
        ExitGroup(key);
    }

    #endregion

    #region Local Group

    public void StartLocalGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"StartLocalGroup({key})");
        if (!EnterOrCreateGroup(key, SturdyComposeGroupType.Local))
        {
            _enteredLocalGroups.Push(RequireCurrentParent());
            return;
        }

        var parent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get(
            key: key,
            type: SturdyComposeGroupType.Local,
            parent: parent
        );
        newGroup.Metadata = _enteredLocalGroups.IsNotEmpty()
            ? _enteredLocalGroups.Peek().GetMetadata<CompositionLocalMap>().Copy()
            : CompositionLocalMap.Get();

        parent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        _enteredLocalGroups.Push(newGroup);
        _enteredReusableGroups.Push(newGroup);
    }

    public void EndLocalGroup(int key)
    {
        if (ComposeConstants.Logging)
            Log($"EndLocalGroup({key})");
        ExitGroup(key);
        _enteredLocalGroups.Pop();
    }

    public T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        var map = GetCompositionLocalMap();
        return map == null ? defaultValueFactory() : map.Get(compositionLocal, defaultValueFactory);
    }

    public CompositionLocalMap? GetCompositionLocalMap()
    {
        if (_enteredLocalGroups.IsEmpty())
            return null;
        var map = _enteredLocalGroups.Peek().GetMetadata<CompositionLocalMap>();
        return map;
    }

    #endregion

    #region Remember

    public Optional<T> Read<T>()
    {
        if (ComposeConstants.Logging)
            Log($"Read<{typeof(T).Name}>() {_currentSlotIndex}");
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return RequireCurrentParent().Slots.Get<T>(_currentSlotIndex);
    }

    public Optional<T> ReadAsStruct<T>()
    {
        if (ComposeConstants.Logging)
            Log($"ReadAsStruct<{typeof(T).Name}>() {_currentSlotIndex}");
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return RequireCurrentParent().Slots.GetAsStruct<T>(_currentSlotIndex);
    }

    public bool ReadAndWrite<T>(T value)
    {
        if (ComposeConstants.Logging)
            Log($"ReadAndWrite<{typeof(T).Name}>({value}) {_currentSlotIndex}");
        var slots = RequireCurrentParent().Slots;
        if (!IsThereAlreadyASlot())
        {
            slots.Insert(_currentSlotIndex, value);
            _currentSlotIndex++;
            return true;
        }

        var result = !slots.Get<T>(_currentSlotIndex).Equals(value);
        if (result)
            slots.Set(_currentSlotIndex, value);

        _currentSlotIndex++;
        return result;
    }

    public bool ReadAndWriteAsStruct<T>(T value)
    {
        if (ComposeConstants.Logging)
            Log($"ReadAndWriteAsStruct<{typeof(T).Name}>({value}): {_currentSlotIndex}");
        var slots = RequireCurrentParent().Slots;
        if (!IsThereAlreadyASlot())
        {
            slots.InsertAsStruct(_currentSlotIndex, value);
            _currentSlotIndex++;
            return true;
        }

        var result = !slots.GetAsStruct<T>(_currentSlotIndex).Equals(value);
        slots.SetAsStruct(_currentSlotIndex, value);
        _currentSlotIndex++;
        return result;
    }

    public void Write<T>(T value)
    {
        if (ComposeConstants.Logging)
            Log($"Write<{typeof(T).Name}>({value}) {_currentSlotIndex}");
        var slots = RequireCurrentParent().Slots;
        if (!IsThereAlreadyASlot())
        {
            slots.Insert(_currentSlotIndex, value);
            _currentSlotIndex++;
            return;
        }

        slots.Set(_currentSlotIndex, value);
        _currentSlotIndex++;
    }

    public void WriteAsStruct<T>(T value)
    {
        if (ComposeConstants.Logging)
            Log($"WriteAsStruct<{typeof(T).Name}>({value})");
        var slots = RequireCurrentParent().Slots;
        if (!IsThereAlreadyASlot())
        {
            slots.InsertAsStruct(_currentSlotIndex, value);
            _currentSlotIndex++;
            return;
        }

        slots.SetAsStruct(_currentSlotIndex, value);
        _currentSlotIndex++;
    }

    public void IncrementSlotIndex()
    {
        if (ComposeConstants.Logging)
            Log($"IncrementSlotIndex()");
        _currentSlotIndex++;
    }

    #endregion

    public void ResetTo(
        SturdyComposeRestartScope scope
    )
    {
        var group = scope.Group.NotNull();
        _invalidationRoot = group;
        _currentParent = group.Parent;
        _currentGroupIndex = scope.GroupIndex;
        _currentSlotIndex = 0;
        // _currentElementIndex = scope.ElementIndex;
        _currentElementIndex = GetVisualElementInsertIndex(group);
        _enteredRestartGroups.Clear();
        _enteredReusableGroups.Clear();
        _enteredLocalGroups.Clear();
        _enteredGroupIndices.Clear();
        _enteredSlotIndices.Clear();
        _enteredElements.Clear();
        _enteredElementIndices.Clear();

        if (scope.VisualElement != null)
        {
            _enteredElements.Push(scope.VisualElement.NotNull());
            _enteredElementIndices.Push(_currentElementIndex);
        }

        if (scope.AncestorLocalGroup != null)
            _enteredLocalGroups.Push(scope.AncestorLocalGroup);
    }

    private static int GetVisualElementInsertIndex(SturdyComposeGroup group)
    {
        // if (group.Type != SturdyComposeGroupType.Restart)
        //     throw new ArgumentException("Expected RestartGroup.", nameof(group));

        var index = 0;
        var current = group;

        while (current.Parent is { } parent)
        {
            var currentIndex = parent.Children.IndexOf(current);

            for (var i = 0; i < currentIndex; i++)
            {
                index += parent.Children[i].ElementsCount;
            }

            if (parent.Type == SturdyComposeGroupType.Reusable)
                return index;

            current = parent;
        }

        return 0;
    }

    public void Clear()
    {
        foreach (var child in _root.Children)
            child.Dispose();
        _root.Children.Clear();
        _root.Slots.Clear();
        _enteredRestartGroups.Clear();
        _enteredReusableGroups.Clear();
        _enteredLocalGroups.Clear();
        _enteredGroupIndices.Clear();
        _enteredSlotIndices.Clear();
        _enteredElements.Clear();
        _enteredElementIndices.Clear();

        _currentParent = _root;
        _currentGroupIndex = 0;
        _currentSlotIndex = 0;
        _currentElementIndex = 0;
        _invalidationRoot = null;
    }

    public string Format()
    {
        return _root.Format("", _currentParent, _currentGroupIndex, _currentSlotIndex);
    }

    public void RequestCurrentComposer() => _composer.SetAsCurrentComposer();

    public void ReleaseCurrentComposer() => _composer.ResetAsCurrentComposer();

    private void Log(object? message = null)
    {
        Debug.Log(message + "\n" + Format());
    }

    private void LogWarning(object? message = null)
    {
        Debug.LogWarning(message + "\n" + Format());
    }


    // True if created
    private bool EnterOrCreateGroup(int key, SturdyComposeGroupType type)
    {
        var currentParent = RequireCurrentParent();
        var existingGroup = currentParent.Children!.GetOrDefault(_currentGroupIndex, null);
        if (existingGroup != null && existingGroup.Key == key && existingGroup.Type == type)
        {
            existingGroup.Parent = currentParent;
            EnterGroup(existingGroup);
            return false;
        }

        if (existingGroup != null)
        {
            IncrementElementsCountRecursively(existingGroup, -existingGroup.ElementsCount);
            existingGroup.Dispose();
            currentParent.Children.RemoveAt(_currentGroupIndex);
        }

        return true;
    }

    // True if created
    private bool EnterOrCreateMovableGroup<T>(int key, T dataKey)
    {
        var currentParent = RequireCurrentParent();
        var existingGroup = currentParent.Children!.GetOrDefault(_currentGroupIndex, null);
        if (
            existingGroup != null &&
            existingGroup.Key == key &&
            existingGroup.Type == SturdyComposeGroupType.Movable &&
            EqualityUtils.FastEquals(existingGroup.GetMetadata<T>(), dataKey)
        )
        {
            EnterGroup(existingGroup);
            return false;
        }

        // Finding out of place group
        for (var i = _currentGroupIndex + 1; i < currentParent.Children.Count; i++)
        {
            var outOfPlaceExistingGroup = currentParent.Children[i];
            if (
                outOfPlaceExistingGroup.Key == key &&
                outOfPlaceExistingGroup.Type == SturdyComposeGroupType.Movable &&
                EqualityUtils.FastEquals(outOfPlaceExistingGroup.GetMetadata<T>(), dataKey)
            )
            {
                var initialGroup = currentParent.Children[_currentGroupIndex];
                // outOfPlaceExistingGroup.Parent = currentParent;
                SwapVisualElements(outOfPlaceExistingGroup, initialGroup);
                (currentParent.Children[i], currentParent.Children[_currentGroupIndex]) = (
                    currentParent.Children[_currentGroupIndex], currentParent.Children[i]);
                EnterGroup(outOfPlaceExistingGroup);
                return false;
            }
        }

        return true;
    }

    private static void SwapVisualElements(SturdyComposeGroup first, SturdyComposeGroup second)
    {
        var firstIndex = first.Parent.NotNull().Children.IndexOf(first);
        var secondIndex = second.Parent.NotNull().Children.IndexOf(second);
        if (firstIndex > secondIndex)
        {
            // (firstIndex, secondIndex) = (secondIndex, firstIndex);
            (first, second) = (second, first);
        }

        var firstElementIndex = GetVisualElementInsertIndex(first);
        var secondElementIndex = GetVisualElementInsertIndex(second);
        if (firstElementIndex == secondElementIndex)
            return;
        var firstElements = GetChildElements(first);
        var secondElements = GetChildElements(second);
        if (firstElements.IsEmpty() && secondElements.IsEmpty())
            return;
        var parentElement = firstElements.IsNotEmpty() ? firstElements[0].parent : secondElements[0].parent;
        foreach (var element in firstElements)
            parentElement.Remove(element);
        foreach (var element in secondElements)
            parentElement.Remove(element);

        for (var i = 0; i < secondElements.Count; i++)
        {
            parentElement.Insert(firstElementIndex + i, secondElements[i]);
        }

        var newSecondElementIndex = secondElementIndex - firstElements.Count + secondElements.Count;

        for (var i = 0; i < firstElements.Count; i++)
            parentElement.Insert(newSecondElementIndex + i, firstElements[i]);
    }

    private static IStableList<VisualElement> GetChildElements(SturdyComposeGroup movableGroup)
    {
        var result = MutableStableListOf<VisualElement>();
        foreach (var child in movableGroup.Children)
        {
            var element = GetChildElementRecursively(child);
            if (element != null)
                result.Add(element);
        }

        return result;
    }

    private static VisualElement? GetChildElementRecursively(SturdyComposeGroup group)
    {
        if (group.Type == SturdyComposeGroupType.Reusable)
            return group.GetMetadata<ReusableComposeNode>().GetVisualElement();
        foreach (var child in group.Children)
        {
            var visualElement = GetChildElementRecursively(child);
            if (visualElement != null)
                return visualElement;
        }

        return null;
    }

    private void EnterGroup(SturdyComposeGroup group)
    {
        _currentParent = group;
        _enteredGroupIndices.Push(_currentGroupIndex);
        _enteredSlotIndices.Push(_currentSlotIndex);
        _currentGroupIndex = 0;
        _currentSlotIndex = 0;
    }

    private void ExitGroup(int key)
    {
        var currentParent = RequireCurrentParent();
        if (currentParent.Key != key)
            throw new InvalidOperationException("Trying to exit invalid group!");
        if (currentParent.Children.Count != _currentGroupIndex)
        {
            var increment = 0;
            for (var i = _currentGroupIndex; i < currentParent.Children.Count; i++)
                increment += currentParent.Children[i].ElementsCount;
            IncrementElementsCountRecursively(currentParent, -increment, includeSelf: true);
        }

        if (_currentSlotIndex > currentParent.SlotsCount)
            LogWarning($"Trying to exit invalid group {key}!");
        currentParent.Trim(_currentGroupIndex, _currentSlotIndex);

        _currentParent = currentParent.Parent;
        _currentGroupIndex = _enteredGroupIndices.Pop();
        _currentGroupIndex++;
        _currentSlotIndex = _enteredSlotIndices.Pop();
    }

    private static void IncrementElementsCountRecursively(SturdyComposeGroup group, int increment,
        bool includeSelf = false)
    {
        if (increment == 0)
            return;
        var currentGroup = includeSelf ? group : group.Parent;
        while (currentGroup != null && currentGroup.Type != SturdyComposeGroupType.Reusable)
        {
            currentGroup.ElementsCount += increment;
            currentGroup = currentGroup.Parent;
        }
    }

    private bool IsThereAlreadyASlot()
    {
        return RequireCurrentParent().Slots.Count > _currentSlotIndex;
    }
}

internal static class StableStackExtensions
{
    public static V PeekOrDefault<T, V>(this IStableStack<T> stack, V defaultValue) where T : V
    {
        return stack.IsNotEmpty() ? stack.Peek() : defaultValue;
    }

    public static T? PeekOrNull<T>(this IStableStack<T> stack)
    {
        return stack.IsNotEmpty() ? stack.Peek() : default;
    }
}