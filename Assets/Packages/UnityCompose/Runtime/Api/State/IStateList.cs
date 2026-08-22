using System;
using System.Collections;
using System.Collections.Generic;
using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IStateList<T> : IStableList<T>
{
}

public interface IMutableStateList<T> : IStateList<T>, IMutableStableList<T>
{
}

internal class MutableStateListImpl<T> : BaseMutableStateImpl, IMutableStateList<T>
{
    private readonly IMutableStableList<T> _mutableList;

    public MutableStateListImpl()
    {
        _mutableList = MutableStableListOf<T>();
    }

    public MutableStateListImpl(params T[] values)
    {
        _mutableList = MutableStableListOf(values);
    }

    public MutableStateListImpl(IEnumerable<T> values)
    {
        _mutableList = IMutableStableList.Create(values);
    }

    public int Count
    {
        get
        {
            Capture();
            return _mutableList.Count;
        }
    }
    
    public T this[int index]
    {
        get
        {
            Capture();
            return _mutableList[index];
        }
        set
        {
            if (Equals(_mutableList[index], value)) return;
            _mutableList[index] = value;
            Notify();
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        Capture();
        return _mutableList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        Capture();
        return GetEnumerator();
    }

    public bool Add(T item)
    {
        _mutableList.Add(item);
        Notify();
        return true;
    }

    public int RemoveAll(Func<T, bool> predicate)
    {
        var result = _mutableList.RemoveAll(predicate);
        if (result != 0)
            Notify();
        return result;
    }

    public int AddRange(IEnumerable<T> items)
    {
        var result = _mutableList.AddRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int AddRange(params T[] items)
    {
        var result = _mutableList.AddRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int RemoveRange(IEnumerable<T> items)
    {
        var result = _mutableList.RemoveRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int RemoveRange(params T[] items)
    {
        var result = _mutableList.RemoveRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public bool RemoveFirst(Func<T, bool> predicate)
    {
        var result = _mutableList.RemoveFirst(predicate);
        if (result)
            Notify();
        return result;
    }

    public void RemoveRange(int index, int count)
    {
        _mutableList.RemoveRange(index, count);
        Notify();
    }

    public void Reverse()
    {
        if (_mutableList.IsEmpty())
            return;
        _mutableList.Reverse();
        Notify();
    }

    public void Sort(IComparer<T> comparer)
    {
        if (_mutableList.IsEmpty())
            return;
        _mutableList.Sort(comparer);
        Notify();
    }

    public void Sort(Comparison<T> comparison)
    {
        if (_mutableList.IsEmpty())
            return;
        _mutableList.Sort(comparison);
        Notify();
    }

    public void SortBy<T1>(Func<T, T1> selector) where T1 : IComparable<T1>
    {
        if (_mutableList.IsEmpty())
            return;
        _mutableList.SortBy(selector);
        Notify();
    }

    public void Clear()
    {
        _mutableList.Clear();
        Notify();
    }

    public bool Contains(T item)
    {
        Capture();
        return _mutableList.Contains(item);
    }

    public bool Remove(T item)
    {
        var result = _mutableList.Remove(item);
        if (result)
            Notify();
        return result;
    }

    public int IndexOf(T item)
    {
        Capture();
        return _mutableList.IndexOf(item);
    }

    IStableList<T>.Enumerator IStableList<T>.GetEnumerator()
    {
        return _mutableList.GetEnumerator();
    }

    public void Insert(int index, T item)
    {
        _mutableList.Insert(index, item);
        Notify();
    }

    public void RemoveAt(int index)
    {
        _mutableList.RemoveAt(index);
        Notify();
    }

    public override string ToString()
    {
        Capture();
        return _mutableList.ToString();
    }
}