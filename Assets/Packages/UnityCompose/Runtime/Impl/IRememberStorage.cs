using System;
using StableCollections;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal interface IRememberStorage : IDisposable
{
    TValue Get<TKey, TValue>(ComposeKey key, TKey compareKey, Func<TKey, TValue> defaultValueFactory);

    void Reset();
}

internal class RememberStorage : IRememberStorage
{
    private readonly IInvocationState _invocationState = new InvocationState();

    private readonly IMutableStableDictionary<ResolvedComposeKey, IComposeRememberState> _rememberStates =
        IMutableStableDictionary.Create<ResolvedComposeKey, IComposeRememberState>();

    public TValue Get<TKey, TValue>(ComposeKey key, TKey compareKey, Func<TKey, TValue> defaultValueFactory)
    {
        var resolvedKey = _invocationState.ResolveKey(key);
        if (_rememberStates.TryGet(resolvedKey, out var cachedRememberState))
        {
            if (cachedRememberState is ComposeRememberState<TKey, TValue> castedCachedRememberState)
            {
                castedCachedRememberState.InvokedThisStep = true;
                return castedCachedRememberState.Get(compareKey, defaultValueFactory);
            }

            cachedRememberState.Dispose();
        }
        var newValue = defaultValueFactory(compareKey);
        var newRememberState = new ComposeRememberState<TKey, TValue>(compareKey, newValue);
        newRememberState.InvokedThisStep = true;
        _rememberStates[resolvedKey] = newRememberState;
        return newValue;
    }

    public void Reset()
    {
        foreach (var rememberState in _rememberStates.ToImmutableStableList())
        {
            if (!rememberState.Value.InvokedThisStep)
            {
                _rememberStates.Remove(rememberState.Key);
                if (rememberState.Value is IDisposable disposable)
                    disposable.Dispose();
            }
            else
                rememberState.Value.InvokedThisStep = false;
        }

        _invocationState.Reset();
    }

    public void Dispose()
    {
        foreach (var rememberState in _rememberStates)
        {
            if (rememberState.Value is IDisposable disposable)
                disposable.Dispose();
        }
    }
}