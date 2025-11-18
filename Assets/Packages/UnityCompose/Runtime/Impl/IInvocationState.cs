using StableCollections;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal interface IInvocationState
{
    ResolvedComposeKey ResolveKey(ComposeKey originalKey);
    void Reset();
}

internal class InvocationState : IInvocationState
{
    private readonly IMutableStableDictionary<ComposeKey, ComposeInvocationCount> _invocationCount =
        IMutableStableDictionary.Create<ComposeKey, ComposeInvocationCount>();

    public ResolvedComposeKey ResolveKey(ComposeKey originalKey)
    {
        if (_invocationCount.TryGet(originalKey, out var count))
        {
            var resolvedKey = ResolvedComposeKey.Create(originalKey, count.Count);
            count.Count++;
            return resolvedKey;
        }

        var newResolvedKey = ResolvedComposeKey.Create(originalKey, 0);
        _invocationCount[originalKey] = new ComposeInvocationCount
        {
            Count = 1
        };
        return newResolvedKey;
    }

    public void Reset()
    {
        foreach (var composeInvocationCount in _invocationCount.Values) composeInvocationCount.Count = 0;
    }

    public override string ToString()
    {
        return $"InvocationState({_invocationCount})";
    }
}