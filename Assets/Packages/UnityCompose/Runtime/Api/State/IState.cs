using System.Collections.Generic;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

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
    private readonly HashSet<ReusableComposeGroup> _scopes = new();
    private readonly bool _isCompositionLocal;
    private bool _log;

    protected BaseMutableStateImpl(bool isCompositionLocal = false, bool log = false)
    {
        _isCompositionLocal = isCompositionLocal;
        _log = log;
    }

    protected void Capture()
    {
        CurrentComposer.Capture(this, _log);
    }

    protected void Notify()
    {
        foreach (var group in _scopes)
        {
            if (_isCompositionLocal)
                ComposeInvalidator.RequestInstantInvalidate(group);
            else
                ComposeInvalidator.RequestInvalidate(group);
        }
    }

    internal bool Add(ReusableComposeGroup restartScope) => _scopes.Add(restartScope);
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
        var valueStr = _value?.ToString() ?? "null";
        return $"MutableState({valueStr})[{GetHashCode()}]";
    }
}