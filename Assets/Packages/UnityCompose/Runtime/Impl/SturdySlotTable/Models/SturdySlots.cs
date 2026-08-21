using System;
using System.Collections.Generic;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;

internal class SturdySlots : IDisposable
{
    public static SturdySlots Get() => new();
    
    private static readonly object Nothing = new();

    private bool _isDisposed;
    private readonly List<object?> _slots = new();

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
    
    public void AddNothing()
    {
        AssertNotDisposed();
        _slots.Add(Nothing);
    }

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
        return (T)value!;
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
        _slots[index] = value;
    }

    public void SetAsStruct<T>(int index, T value)
    {
        AssertNotDisposed();
        var entry = _slots[index].NotNull().CastTo<MutableSlotEntry<T>>();
        entry.Value = value;
    }

    public void Dispose()
    {
        foreach (var slot in _slots)
        {
            if (slot is IComposeDisposable composeDisposable)
                composeDisposable.Dispose();
        }
    }

    private void AssertNotDisposed()
    {
        if (_isDisposed)
            throw new InvalidOperationException("Trying to access disposed SturdySlots!");
    }
}