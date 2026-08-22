using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpExtensions;
using StableCollections;
using UnityEditor.Graphs;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;

internal class SturdyComposeGroup : IDisposable
{
    private static readonly ObjectPool<SturdyComposeGroup> Pool = new(
        factory: () => new SturdyComposeGroup(),
        onInit: it => it._isDisposed = false
    );

    private static readonly ObjectPool<IMutableStableList<SturdyComposeGroup>> ChildrenPool = new(
        factory: () => MutableStableListOf<SturdyComposeGroup>()
    );

    public static SturdyComposeGroup Get(
        int key,
        SturdyComposeGroupType type,
        SturdyComposeGroup? parent
    )
    {
        var result = Pool.Get();
        result.Key = key;
        result.Type = type;
        result.Parent = parent;
        return result;
    }

    private bool _isDisposed;
    private IMutableStableList<SturdyComposeGroup>? _children;
    private SturdySlots? _slots;

    public int Key { get; private set; }
    public SturdyComposeGroupType Type { get; private set; }
    public SturdyComposeGroup? Parent { get; set; }
    public int ElementsCount { get; set; }
    public int ChildrenCount => _children != null ? _children.Count : 0;
    public int SlotsCount => _slots != null ? _slots.Count : 0;
    public object? Metadata { private get; set; }

    public IMutableStableList<SturdyComposeGroup> Children
    {
        get
        {
            AssertNotDisposed();
            _children ??= ChildrenPool.Get();
            return _children;
        }
    }

    public SturdySlots Slots
    {
        get
        {
            AssertNotDisposed();
            _slots ??= SturdySlots.Get();
            return _slots;
        }
    }

    private SturdyComposeGroup()
    {
    }

    public T GetMetadata<T>()
    {
        if (Metadata is not T)
            Debug.LogError($"{this}: {Metadata} is not {typeof(T).Name}!");
        return (T)Metadata!;
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        if (_children != null)
        {
            foreach (var child in _children)
                child.Dispose();
            _children.Clear();
            ChildrenPool.Return(_children);
        }

        _slots?.Dispose();
        if (Metadata is IComposeDisposable composeDisposable)
            composeDisposable.Dispose();
        Metadata = null;
        Key = 0;
        Type = SturdyComposeGroupType.Replace;
        ElementsCount = 0;
        Parent = null;
        _children = null;
        _slots = null;
        Pool.Return(this);
    }

    public void Trim(int groupsCount, int slotsCount)
    {
        if (_children != null && _children.Count != groupsCount)
        {
            if (groupsCount > _children.Count)
                throw new ArgumentException("groupsCount > _children.Count");
            for (var i = groupsCount; i < _children.Count; i++)
                _children[i].Dispose();
            _children.RemoveRange(groupsCount, _children.Count - groupsCount);
        }

        _slots?.Trim(slotsCount);
    }

    private void AssertNotDisposed()
    {
        if (_isDisposed)
            throw new InvalidOperationException("Trying to access disposed SturdySlots!");
    }

    public string Format(string indent, SturdyComposeGroup? currentParent, int currentGroupIndex, int currentSlotIndex)
    {
        var result = new StringBuilder();
        result.Append(indent + $"(Key: {Key}, Type: {Type}, ElementsCount: {ElementsCount})");
        if (currentParent == this)
            result.Append(" << CURRENT_PARENT");
        result.AppendLine();
        if (Metadata != null)
            result.AppendLine(indent + $" Metadata: {Metadata}");
        result.AppendLine(indent + " Slots:");
        var i = 0;
        foreach (var slot in _slots ?? Enumerable.Empty<object?>())
        {
            result.Append(indent + "  ");
            result.Append(slot?.ToString() ?? "Null");
            if (i == currentSlotIndex && currentParent == this)
                result.Append(" << CURRENT_SLOT");
            result.AppendLine();
            i++;
        }

        if (currentSlotIndex == (_slots?.Count ?? 0) && currentParent == this)
            result.AppendLine(indent + " << CURRENT_SLOT");

        result.AppendLine(indent + " Children:");
        i = 0;
        foreach (var child in _children ?? Enumerable.Empty<SturdyComposeGroup>())
        {
            result.Append(child.Format(indent + "   ", currentParent, currentGroupIndex, currentSlotIndex));
            if (i == currentGroupIndex && currentParent == this)
                result.Append(" << CURRENT_GROUP");
            // result.AppendLine();
            i++;
        }

        if (currentGroupIndex == (_children?.Count ?? 0) && currentParent == this)
            result.AppendLine(indent + " << CURRENT_GROUP");

        return result.ToString();
    }
}

internal enum SturdyComposeGroupType
{
    Replace,
    Restart,
    Reusable,
    Local,
    Movable,
}