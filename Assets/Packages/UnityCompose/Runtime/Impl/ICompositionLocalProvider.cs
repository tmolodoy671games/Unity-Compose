using System;
using StableCollections;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal interface ICompositionLocalProvider
{
    TValue Get<TValue>(ICompositionLocal<TValue> compositionLocal, Func<TValue> defaultValueFactory);

    void Update(IStableList<CompositionLocalProvides> provides);
}

internal class CompositionLocalProvider : ICompositionLocalProvider
{
    private readonly IMutableStableDictionary<ICompositionLocal, IMutableState<object?>> _customValues =
        IMutableStableDictionary.Create<ICompositionLocal, IMutableState<object?>>();

    public TValue Get<TValue>(ICompositionLocal<TValue> compositionLocal, Func<TValue> defaultValueFactory)
    {
        if (_customValues.TryGet(compositionLocal, out var cachedValue))
            return (TValue)cachedValue.Value!;
        return defaultValueFactory();
    }

    public void Update(IStableList<CompositionLocalProvides> provides)
    {
        foreach (var provide in provides)
        {
            if (_customValues.TryGet(provide.CompositionLocal, out var state))
                state.Value = provide.Value;
            else
            {
                _customValues[provide.CompositionLocal] = MutableStateOf(provide.Value);
            }
        }
    }
}