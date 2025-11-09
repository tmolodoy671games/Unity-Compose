// ReSharper disable CheckNamespace

using System;
using System.Collections;
using StableCollections;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    private class ComposeCoroutineScopeImpl : IComposeCoroutineScope, IDisposable
    {
        private readonly IMutableStableList<IDisposable> _disposables = IMutableStableList.Create<IDisposable>();

        public void StartCoroutine(IEnumerator coroutine)
        {
            _disposables.Add(ComposeInvalidator.StartCoroutineAsDisposable(coroutine));
        }
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }

    [Composable]
    public static IComposeCoroutineScope RememberCoroutineScope()
    {
        return Remember(() => new ComposeCoroutineScopeImpl());
    }
}

public interface IComposeCoroutineScope
{
    void StartCoroutine(IEnumerator coroutine);
}