using System;
using System.Collections;
using System.Collections.Generic;
using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IStateSet<T> : IStableSet<T>
{
}

public interface IMutableStateSet<T> : IStateSet<T>, IMutableStableSet<T>
{
}

internal class MutableStateSetImpl<T> : BaseMutableStateImpl, IMutableStateSet<T>
{
    private readonly IMutableStableSet<T> _set;

    public MutableStateSetImpl()
    {
        _set = IMutableStableSet.Create<T>();
    }

    public MutableStateSetImpl(params T[] values)
    {
        _set = IMutableStableSet.Create(values);
    }

    public MutableStateSetImpl(IEnumerable<T> values)
    {
        _set = IMutableStableSet.Create(values);
    }

    public int Count
    {
        get
        {
            Capture();
            return _set.Count;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        Capture();
        return _set.GetEnumerator();
    }

    public bool Add(T item)
    {
        var result = _set.Add(item);
        if (result)
            Notify();
        return result;
    }

    public bool Remove(T item)
    {
        var result = _set.Remove(item);
        if (result)
            Notify();
        return result;
    }

    public bool RemoveFirst(Func<T, bool> predicate)
    {
        var result = _set.RemoveFirst(predicate);
        if (result)
            Notify();
        return result;
    }

    public int RemoveAll(Func<T, bool> predicate)
    {
        var result = _set.RemoveAll(predicate);
        if (result != 0)
            Notify();
        return result;
    }

    public int AddRange(IEnumerable<T> items)
    {
        var result = _set.AddRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int AddRange(params T[] items)
    {
        var result = _set.AddRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int RemoveRange(IEnumerable<T> items)
    {
        var result = _set.RemoveRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int RemoveRange(params T[] items)
    {
        var result = _set.RemoveRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int ExceptWith(IEnumerable<T> other)
    {
        var result = _set.ExceptWith(other);
        if (result != 0)
            Notify();
        return result;
    }

    public int IntersectWith(IEnumerable<T> other)
    {
        var result = _set.IntersectWith(other);
        if (result != 0)
            Notify();
        return result;
    }

    public int UnionWith(IEnumerable<T> other)
    {
        var result = _set.UnionWith(other);
        if (result != 0)
            Notify();
        return result;
    }

    public int SymmetricExceptWith(IEnumerable<T> other)
    {
        var result = _set.SymmetricExceptWith(other);
        if (result != 0)
            Notify();
        return result;
    }

    public void Clear()
    {
        var result = _set.IsNotEmpty();
        if (!result)
            return;
        _set.Clear();
        Notify();
    }

    public bool Contains(T item)
    {
        Capture();
        return _set.Contains(item);
    }

    public override string ToString()
    {
        Capture();
        return _set.ToString();
    }
}