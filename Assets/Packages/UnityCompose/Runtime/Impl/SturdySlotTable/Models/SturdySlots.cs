using System;
using System.Collections;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;

internal class SturdySlots : IDisposable, IEnumerable<object?>
{
    private class NothingImpl
    {
        public override string ToString() => "Nothing";
    }
    
    private static readonly ObjectPool<SturdySlots> Pool = new(
        factory: () => new SturdySlots(),
        onInit: it => it._isDisposed = false
    );

    public static SturdySlots Get() => Pool.Get();

    private static readonly object Nothing = new NothingImpl();

    private bool _isDisposed;
    private readonly IMutableStableList<object?> _slots = MutableStableListOf<object?>();

    private SturdySlots()
    {
    }

    public int Count => _slots.Count;

    public void Insert(int index, object? value)
    {
        AssertNotDisposed();
        _slots.Insert(index, value);
    }

    public void InsertAsStruct<T>(int index, T value)
    {
        AssertNotDisposed();
        _slots.Insert(index, MutableSlotEntry.Get(value));
    }

    public void Add(object? value)
    {
        AssertNotDisposed();
        _slots.Add(value);
    }

    // public void AddNothing()
    // {
    //     AssertNotDisposed();
    //     _slots.Add(Nothing);
    // }

    public void AddAsStruct<T>(T value)
    {
        AssertNotDisposed();
        _slots.Add(MutableSlotEntry.Get(value));
    }

    public Optional<T> Get<T>(int index)
    {
        AssertNotDisposed();
        var value = _slots[index];
        if (value == Nothing)
            return default;
        try
        {
            return (T)value!;
        }
        catch (InvalidCastException)
        {
            Debug.LogError($"Trying to cast {value} to {typeof(T)}!");
            throw;
        }
    }

    public Optional<T> GetAsStruct<T>(int index)
    {
        AssertNotDisposed();
        var value = _slots[index];
        if (value == Nothing)
            return default;
        return ((MutableSlotEntry<T>)value!).Value;
    }

    public void Set(int index, object? value)
    {
        AssertNotDisposed();
        var existingValue = _slots[index];
        if (existingValue is IComposeDisposable composeDisposable)
            composeDisposable.Dispose();
        _slots[index] = value;
    }

    public void SetAsStruct<T>(int index, T value)
    {
        AssertNotDisposed();
        var entry = _slots[index].NotNull().CastTo<MutableSlotEntry<T>>();
        if (entry.Value is IComposeDisposable composeDisposable)
            composeDisposable.Dispose();
        entry.Value = value;
    }
    
    public void Clear() => _slots.Clear();

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        foreach (var slot in _slots)
        {
            if (slot is IComposeDisposable composeDisposable)
                composeDisposable.Dispose();
        }
        _slots.Clear();
        Pool.Return(this);
    }

    public void Trim(int slotsCount)
    {
        if (_slots.Count ==  slotsCount)
            return;
        if (slotsCount > _slots.Count)
            throw new ArgumentException("slotsCount > _slots.Count");
        for (var i = slotsCount; i < _slots.Count; i++)
        {
            if (_slots[i] is IComposeDisposable composeDisposable)
                composeDisposable.Dispose();
        }
        _slots.RemoveRange(slotsCount, _slots.Count - slotsCount);
    }

    private void AssertNotDisposed()
    {
        if (_isDisposed)
            throw new InvalidOperationException("Trying to access disposed SturdySlots!");
    }

    public IEnumerator<object?> GetEnumerator()
    {
        return _slots.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return _slots.ToString();
    }
}