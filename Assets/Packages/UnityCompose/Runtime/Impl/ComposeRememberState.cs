using System;
using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal interface IComposeRememberState : IDisposable
{
    bool InvokedThisStep { get; set; }
}

internal class ComposeRememberState<TKey, TValue> : IComposeRememberState
{
    private TKey _key;
    private TValue _value;

    public ComposeRememberState(TKey key, TValue value)
    {
        _key = key;
        _value = value;
    }

    public bool InvokedThisStep { get; set; }

    public TValue Get(TKey key, Func<TKey, TValue> defaultValueFactory)
    {
        InvokedThisStep = true;
        if (EqualityUtils.FastEquals(_key, key))
            return _value;
        _key = key;
        _value = defaultValueFactory(key);
        return _value;
    }

    public void Dispose()
    {
        if (_value is IDisposable disposable)
            disposable.Dispose();
    }
}