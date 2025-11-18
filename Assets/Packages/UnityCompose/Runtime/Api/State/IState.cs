using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IState<out T>
{
    T Value { get; }
}

public interface IMutableState<T> : IState<T>
{
    new T Value { get; set; }
}

public abstract class BaseMutableStateImpl
{
    private readonly IMutableStableSet<IComposeGroup> _groups = IMutableStableSet.Create<IComposeGroup>();
    private readonly bool _isCompositionLocal;

    protected BaseMutableStateImpl(bool isCompositionLocal = false)
    {
        _isCompositionLocal = isCompositionLocal;
    }

    protected void Capture()
    {
        CurrentComposer.Capture(this);
    }

    protected void Notify()
    {
        foreach (var group in _groups)
        {
            if (_isCompositionLocal)
                ComposeInvalidator.RequestInstantInvalidate(group);
            else
                ComposeInvalidator.RequestInvalidate(group);
        }
    }

    internal void Add(IComposeGroup group) => _groups.Add(group);
}

internal class MutableStateImpl<T> : BaseMutableStateImpl, IMutableState<T>
{
    public MutableStateImpl(T value, bool isCompositionLocal = false) : base(isCompositionLocal)
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
        var valueStr = _value?.ToString() ?? "null";
        return $"MutableState({valueStr})";
    }
}