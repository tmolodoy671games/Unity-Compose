using System.Collections.Generic;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IState<out T>
{
    T Value { get; }
}

public interface IMutableState
{
}

public interface IMutableState<T> : IState<T>, IMutableState
{
    new T Value { get; set; }
}

public abstract class BaseMutableStateImpl
{
    private readonly HashSet<ComposeRestartScope> _scopes = new();
    private readonly bool _isCompositionLocal;
    public readonly bool Log;

    protected BaseMutableStateImpl(bool isCompositionLocal = false, bool log = false)
    {
        _isCompositionLocal = isCompositionLocal;
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
        {
            if (_isCompositionLocal)
                ComposeInvalidator.RequestInstantInvalidate(group);
            else
                ComposeInvalidator.RequestInvalidate(group);
        }
    }

    internal bool Add(ComposeRestartScope restartScope) => _scopes.Add(restartScope);
}

internal class MutableStateImpl<T> : BaseMutableStateImpl, IMutableState<T>
{
    public MutableStateImpl(T value, bool isCompositionLocal = false, bool log = false) : base(isCompositionLocal, log)
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

    public override string ToString()
    {
        return $"MutableState({_value})";
    }
}