using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal class CompositionLocalMap : IComposeDisposable
{
    private readonly record struct ProvidedValue(
        IMutableState State,
        bool IsInherited
    );

    private static readonly ObjectPool<CompositionLocalMap> _pool = new(() => new CompositionLocalMap());

    private readonly Dictionary<ICompositionLocal, ProvidedValue> _customValues = new();
    private bool _isDisposed;

    public static CompositionLocalMap Get()
    {
        var result = ComposeConstants.Pooling ? _pool.Get() : new CompositionLocalMap();
        result._isDisposed = false;
        return result;
    }

    private CompositionLocalMap()
    {
    }

    public T Get<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        if (_customValues.TryGetValue(compositionLocal, out var providedValue) &&
            providedValue.State is IMutableState<T> mutableState)
            return mutableState.Value;
        return defaultValueFactory();
    }

    public CompositionLocalMap Copy()
    {
        var result = _pool.Get();
        foreach (var pair in _customValues)
            result._customValues[pair.Key] = pair.Value with { IsInherited = true };
        return result;
    }

    public void Set<T>(ICompositionLocal<T> compositionLocal, T customValue)
    {
        if (_customValues.TryGetValue(compositionLocal, out var providedValue) &&
            providedValue is { IsInherited: false, State: IMutableState<T> mutableState })
            mutableState.Value = customValue;
        else
            _customValues[compositionLocal] = new ProvidedValue(LocalMutableStateOf(customValue), false);
    }

    public void Set<T>(CompositionLocalProvides<T> provides)
    {
        Set(provides.CompositionLocal, provides.Value);
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        _customValues.Clear();
        _pool.Return(this);
    }

    public override string ToString()
    {
        return $"CompositionLocalMap{_customValues.ToImmutableStableDictionary()}";
    }
}