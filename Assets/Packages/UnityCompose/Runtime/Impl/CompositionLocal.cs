using StableCollections;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class CompositionLocal
{
    public readonly IMutableStableDictionary<ICompositionLocal, IMutableState<object?>> Provides =
        IMutableStableDictionary.Create<ICompositionLocal, IMutableState<object?>>();

    public CompositionLocal? Parent;
}