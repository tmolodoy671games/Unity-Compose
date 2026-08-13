using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IState<out T>
{
    T Value { get; }
    T GetValue();
}

public interface IMutableState
{
}

public interface IMutableState<T> : IState<T>, IMutableState
{
    new T Value { get; set; }
}

public abstract class BaseMutableStateImpl : IMutableState
{
    private readonly IMutableStableSet<ComposeRestartScope> _scopes = MutableStableSetOf<ComposeRestartScope>();

    public readonly bool Log;

    protected BaseMutableStateImpl(bool log = false)
    {
        Log = log;
    }

    protected void Capture()
    {
        Composer.Current?.Capture(this);
    }

    protected void Notify()
    {
        if (Log)
            Debug.Log($"{this}.Notify()");
        foreach (var group in _scopes)
            group.RequestRestart();
    }

    internal bool Add(ComposeRestartScope restartScope)
    {
        var result = _scopes.Add(restartScope);
        if (!result)
            restartScope.Add(this);
        return result;
    }

    internal void Remove(ComposeRestartScope restartScope)
    {
        _scopes.Remove(restartScope);
    }
}

internal class MutableStateImpl<T> : BaseMutableStateImpl, IMutableState<T>
{
    public MutableStateImpl(T value, bool log = false) : base(log)
    {
        _value = value;
    }

    private T _value;

    public T Value
    {
        get
        {
            Capture();
            return _value;
        }
        set
        {
            if (EqualityComparer<T>.Default.Equals(_value, value)) return;
            _value = value;
            Notify();
        }
    }

    public T GetValue() => _value;

    public override string ToString()
    {
        return $"MutableState({_value})";
    }
}