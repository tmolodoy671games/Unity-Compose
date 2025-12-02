using System;
using System.Collections.Generic;
using System.Linq;
using StableCollections;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal class CompositionLocalMap
{
    private record CompositionLocalEntry(
        IMutableState<object?> State,
        CompositionLocalMap Source
    );

    private readonly CompositionLocalMap? _parent;
    private int _lastParentVersion;
    private readonly bool _isOriginal;
    private Dictionary<ICompositionLocal, CompositionLocalEntry> _customValues;
    private IImmutableStableList<CompositionLocalProvides>? _previousProviders;
    private int _version;

    public CompositionLocalMap(CompositionLocalMap? parent)
    {
        _parent = parent;
        _customValues = parent != null
            ? parent._customValues
            : new Dictionary<ICompositionLocal, CompositionLocalEntry>();
        _isOriginal = parent == null;
        _lastParentVersion = parent?._version ?? -1;
    }

    public T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        if (_customValues.TryGetValue(compositionLocal, out var state))
            return (T)state.State.Value!;
        return defaultValueFactory();
    }

    public void Update(IImmutableStableList<CompositionLocalProvides> providers)
    {
        if (Equals(_previousProviders, providers))
            return;
        SyncCustomValues();
        _previousProviders = providers;
        foreach (var provider in providers)
        {
            if (_customValues.TryGetValue(provider.CompositionLocal, out var entry) && entry.Source == this)
                entry.State.Value = provider.Value;
            else
                _customValues[provider.CompositionLocal] =
                    new CompositionLocalEntry(MutableStateOf(provider.Value), this);
        }

        _version++;
    }

    private void SyncCustomValues()
    {
        if (_parent == null) return;
        if (!_isOriginal)
        {
            _customValues = new Dictionary<ICompositionLocal, CompositionLocalEntry>(_parent._customValues);
            _lastParentVersion = _parent._version;
            return;
        }

        if (_lastParentVersion != _parent._version)
        {
            if (_previousProviders == null)
                return;
            // TODO Provided values removal.
            _lastParentVersion = _parent._version;
        }
    }
}