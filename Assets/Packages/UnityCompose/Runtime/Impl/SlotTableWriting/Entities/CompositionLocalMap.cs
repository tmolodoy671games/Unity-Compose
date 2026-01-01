using System;
using System.Collections.Generic;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal class CompositionLocalMap : IDisposable
{
    private static readonly NewObjectPool<CompositionLocalMap> _pool = new(() => new CompositionLocalMap());
    
    private readonly Dictionary<ICompositionLocal, IMutableState> _customValues = new();
    
    public static CompositionLocalMap Get() => _pool.Get();
    
    private CompositionLocalMap() {}

    public T Get<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        if (_customValues.TryGetValue(compositionLocal, out var state) && state is IMutableState<T> mutableState)
            return mutableState.Value;
        return defaultValueFactory();
    }

    public CompositionLocalMap Copy()
    {
        var result = _pool.Get();
        foreach (var pair in _customValues)
            result._customValues[pair.Key] = pair.Value;
        return result;
    }

    public void Set<T>(ICompositionLocal<T> compositionLocal, T customValue)
    {
        if (_customValues.TryGetValue(compositionLocal, out var state) && state is IMutableState<T> mutableState)
            mutableState.Value = customValue;
        else
            _customValues[compositionLocal] = MutableStateOf(customValue);
    }
    
    public void Dispose()
    {
        _customValues.Clear();
        _pool.Return(this);
    }

    public override string ToString()
    {
        return $"CompositionLocalMap{_customValues.ToImmutableStableDictionary().ToString()}";
    }
}