using System;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using StableCollections;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal class CompositionLocalMap
{
    private readonly CompositionLocalMap? _parent;
    private Dictionary<ICompositionLocal, object?> _providedValues;
    private IImmutableStableList<CompositionLocalProvides> _provides;

    public CompositionLocalMap(
        CompositionLocalMap? parent,
        IImmutableStableList<CompositionLocalProvides> provides
    )
    {
        _provides = provides;
        _providedValues = provides
            .ToDictionary(
                static it => it.CompositionLocal,
                static it => it.Value
            );
        _parent = parent;
    }

    public T Get<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        if (_providedValues.TryGetValue(compositionLocal, out var value))
            return (T)value!;
        if (_parent != null)
            return _parent.Get(compositionLocal, defaultValueFactory);
        return defaultValueFactory();
    }

    public void Update(IImmutableStableList<CompositionLocalProvides> provides)
    {
        if (_provides.Equals(provides))
            return;
        _provides = provides;
        _providedValues = provides
            .ToDictionary(
                static it => it.CompositionLocal,
                static it => it.Value
            );
    }

    private IImmutableStableDictionary<ICompositionLocal, object?> LogMap()
    {
        return (_parent?.LogMap()).OrEmpty().Union(_providedValues).ToImmutableStableDictionary();
    }

    public override string ToString()
    {
        return LogMap().ToString();
    }
}