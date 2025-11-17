using StableCollections;

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

    private readonly IMutableStableDictionary<ResolvedComposeKey, bool> _calledThisStep =
        IMutableStableDictionary.Create<ResolvedComposeKey, bool>();

    public ResolvedComposeKey ResolveKey(ComposeKey originalKey)
    {
        if (_invocationCount.TryGet(originalKey, out var count))
        {
            var resolvedKey = ResolvedComposeKey.Create(originalKey, count.Count);
            count.Count++;
            _calledThisStep[resolvedKey] = true;
            return resolvedKey;
        }

        var newResolvedKey = ResolvedComposeKey.Create(originalKey, 0);
        _invocationCount[originalKey] = new ComposeInvocationCount
        {
            Count = 1
        };
        _calledThisStep[newResolvedKey] = true;
        return newResolvedKey;
    }

    public bool CalledThisStep(ResolvedComposeKey key)
    {
        return _calledThisStep.GetOrDefault(key, false);
    }

    public void Reset()
    {
        foreach (var composeInvocationCount in _invocationCount.Values) composeInvocationCount.Count = 0;

        _calledThisStep.Clear();
    }
}