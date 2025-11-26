using System.Collections.Generic;
using SharpExtensions;
using StableCollections;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

public class CompositionLocalMap
{
    private readonly CompositionLocalMap? _parent;
    private readonly Dictionary<ICompositionLocal, IMutableState<object?>> _customValues = new();

    public CompositionLocalMap(CompositionLocalMap? parent)
    {
        _parent = parent;
    }

    public Optional<T> Get<T>(ICompositionLocal<T> compositionLocal)
    {
        if (_customValues.TryGetValue(compositionLocal, out var value))
            return (T)value.Value!;
        if (_parent != null)
            return _parent.Get(compositionLocal);
        return Optional.Empty<T>();
    }

    public void Update(IImmutableStableList<CompositionLocalProvides> provides)
    {
        for (var i = 0; i < provides.Count; i++)
        {
            var provider = provides[i];
            if (_customValues.TryGetValue(provider.CompositionLocal, out var value))
                value.Value = value.Value;
            else
                _customValues[provider.CompositionLocal] = MutableStateOf(provider.Value, true);
        }
    }
}