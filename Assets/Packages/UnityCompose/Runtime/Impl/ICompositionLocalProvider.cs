using StableCollections;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal interface ICompositionLocalProvider
{
    bool TryGet<TValue>(
        ICompositionLocal<TValue> compositionLocal,
        out IMutableState<object?> state
    );

    void Update(IStableList<CompositionLocalProvides> provides);
}

internal class CompositionLocalProvider : ICompositionLocalProvider
{
    private readonly IMutableStableDictionary<ICompositionLocal, IMutableState<object?>> _customValues =
        IMutableStableDictionary.Create<ICompositionLocal, IMutableState<object?>>();

    public bool TryGet<TValue>(
        ICompositionLocal<TValue> compositionLocal,
        out IMutableState<object?> state
    )
    {
        if (_customValues.TryGet(compositionLocal, out state))
            return true;
        return false;
    }

    public void Update(IStableList<CompositionLocalProvides> provides)
    {
        for (var i = 0; i < provides.Count; i++)
        {
            var provider = provides[i];
            if (_customValues.TryGet(provider.CompositionLocal, out IMutableState<object?> state))
                state.Value = provider.Value;
            else
                _customValues[provider.CompositionLocal] = MutableStateOf(provider.Value, true);
        }
    }
}