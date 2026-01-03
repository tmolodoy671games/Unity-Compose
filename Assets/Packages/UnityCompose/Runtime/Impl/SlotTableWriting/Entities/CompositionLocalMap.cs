using System;
using System.Collections.Generic;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal class CompositionLocalMap : IDisposable
{
    private readonly record struct ProvidedValue(
        IMutableState State,
        bool IsInherited
    );

    private static readonly NewObjectPool<CompositionLocalMap> _pool = new(() => new CompositionLocalMap());

    private readonly Dictionary<ICompositionLocal, ProvidedValue> _customValues = new();

    public static CompositionLocalMap Get() => _pool.Get();

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
            _customValues[compositionLocal] = new ProvidedValue(MutableStateOf(customValue), false);
    }

    public void Set<T>(CompositionLocalProvides<T> provides)
    {
        Set(provides.CompositionLocal, provides.Value);
    }

    public void Dispose()
    {
        foreach (var customValue in _customValues.Values)
            customValue.State.ClearScopes();
        _customValues.Clear();
        _pool.Return(this);
    }

    public override string ToString()
    {
        return $"CompositionLocalMap{_customValues.ToImmutableStableDictionary().ToString()}";
    }
}