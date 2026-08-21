using System;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable;

internal class SturdySlotTableWriterImpl
{
    private SturdyComposeGroup _root = SturdyComposeGroup.Get().Also(it =>
    {
        it.Key = 123;
        it.Type = SturdyComposeGroupType.Replace;
    });

    private SturdyComposeGroup? _currentParent;
    private int _currentGroupIndex;
    private int _currentSlotIndex;
    private SturdyComposeGroup? _invalidationRoot;

    private readonly IMutableStableStack<SturdyComposeGroup> _enteredRestartGroups =
        MutableStableStackOf<SturdyComposeGroup>();

    private readonly IMutableStableStack<VisualElement> _enteredVisualElements = MutableStableStackOf<VisualElement>();
    private readonly IMutableStableStack<int> _enteredGroupIndices = MutableStableStackOf<int>();
    private readonly IMutableStableStack<int> _enteredSlotIndices = MutableStableStackOf<int>();

    private SturdyComposeGroup RequireCurrentParent() => _currentParent.NotNull();

    #region Restart Group

    public bool StartRestartGroup(int key)
    {
        if (!EnterOrCreateGroup(key, SturdyComposeGroupType.Restart))
            return false;

        var currentParent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get();
        newGroup.Key = key;
        newGroup.Type = SturdyComposeGroupType.Restart;
        newGroup.Parent = currentParent;
        newGroup.Slots.AddNothing();
        currentParent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        return true;
    }

    public bool IsInInvalidationRoot() => _invalidationRoot == _currentParent;

    public void SkipToGroupEnd()
    {
    }

    public IComposeRestartScope? GetRestartScope()
    {
        if (_enteredRestartGroups.IsEmpty())
            return null;
        var enteredRestartGroup = _enteredRestartGroups.Peek();
        return enteredRestartGroup.Slots.Get<IComposeRestartScope>(0).GetOrDefault(null!);
    }

    public IComposeRestartScope? RequireRestartScope()
    {
        var restartScope = GetRestartScope();
        if (restartScope != null)
            return restartScope;
        if (_enteredRestartGroups.IsEmpty())
            return null;
        var restartGroup = _enteredRestartGroups.Peek();
        var newScope = SturdyComposeRestartScope.Get();
        newScope.Group = restartGroup;
        newScope.VisualElement = _enteredVisualElements.IsNotEmpty() ? _enteredVisualElements.Peek() : null;
        restartGroup.Slots.Set(0, newScope);
        return restartScope;
    }

    public void EndRestartGroup(int key)
    {
        ExitGroup(key);
        _enteredRestartGroups.Pop();
    }

    #endregion

    #region Replace Group

    public bool StartReplaceGroup(int key)
    {
        if (!EnterOrCreateGroup(key, SturdyComposeGroupType.Replace))
            return false;

        var currentParent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get();
        newGroup.Key = key;
        newGroup.Type = SturdyComposeGroupType.Replace;
        newGroup.Parent = currentParent;
        currentParent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        return true;
    }

    public void EndReplaceGroup(int key)
    {
        ExitGroup(key);
    }

    #endregion

    #region Reusable Group

    public bool StartReusableGroup(int key)
    {
        if (!EnterOrCreateGroup(key, SturdyComposeGroupType.Reusable))
            return false;

        var currentParent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get();
        newGroup.Key = key;
        newGroup.Type = SturdyComposeGroupType.Reusable;
        newGroup.Parent = currentParent;
        newGroup.Slots.AddNothing();
        currentParent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
        return true;
    }

    public void EndReusableGroup(int key)
    {
        ExitGroup(key);
    }

    #endregion

    #region Movable Group

    public void StartMovableGroup<T>(int key, T dataKey)
    {
        if (!EnterOrCreateMovableGroup(key, SturdyComposeGroupType.Movable)) return;
        var currentParent = RequireCurrentParent();
        var newGroup = SturdyComposeGroup.Get();
        newGroup.Key = key;
        newGroup.Type = SturdyComposeGroupType.Movable;
        newGroup.Parent = currentParent;
        newGroup.Slots.Add(dataKey);
        currentParent.Children.Insert(_currentGroupIndex, newGroup);
        EnterGroup(newGroup);
    }

    public void EndMovableGroup(int key)
    {
        ExitGroup(key);
    }

    #endregion

    #region Remember

    public Optional<T> Read<T>()
    {
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return RequireCurrentParent().Slots.Get<T>(_currentSlotIndex);
    }

    public Optional<T> ReadAsStruct<T>()
    {
        if (!IsThereAlreadyASlot())
            return Optional.Empty<T>();
        return RequireCurrentParent().Slots.GetAsStruct<T>(_currentSlotIndex);
    }
    
    public bool ReadAndWrite<T>(T value)
    {
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
        _currentSlotIndex++;
    }

    #endregion

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
            EqualityUtils.FastEquals(existingGroup.Slots.Get<T>(0), dataKey)
        )
        {
            existingGroup.Parent = currentParent;
            EnterGroup(existingGroup);
            return false;
        }

        // Finding out of place group
        for (var i = _currentGroupIndex; i < currentParent.Children.Count; i++)
        {
            var outOfPlaceExistingGroup = currentParent.Children[i];
            if (
                outOfPlaceExistingGroup != null &&
                outOfPlaceExistingGroup.Key == key &&
                outOfPlaceExistingGroup.Type == SturdyComposeGroupType.Movable &&
                EqualityUtils.FastEquals(outOfPlaceExistingGroup.Slots.Get<T>(0), dataKey)
            )
            {
                outOfPlaceExistingGroup.Parent = currentParent;
                (currentParent.Children[i], currentParent.Children[_currentGroupIndex]) = (
                    currentParent.Children[_currentGroupIndex], currentParent.Children[i]);
                EnterGroup(outOfPlaceExistingGroup);
                return false;
            }
        }

        return true;
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
        _currentParent = currentParent.Parent;
        _currentGroupIndex = _enteredGroupIndices.Pop();
        _currentSlotIndex = _enteredSlotIndices.Pop();
    }

    private bool IsThereAlreadyASlot()
    {
        return RequireCurrentParent().Slots.Count > _currentSlotIndex;
    }
}