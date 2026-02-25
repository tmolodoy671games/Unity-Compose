// ReSharper disable CheckNamespace

using System;
using System.Collections;
using StableCollections;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    private class ComposeCoroutineScopeImpl : IComposeCoroutineScope, IComposeDisposable
    {
        private readonly IMutableStableList<IDisposable> _disposables =
            IMutableStableList.Create<IDisposable>();

        public IDisposable StartCoroutine(IEnumerator coroutine)
        {
            var disposable = ComposeInvalidator.StartCoroutineAsDisposable(coroutine);
            _disposables.Add(disposable);
            return disposable;
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
    IDisposable StartCoroutine(IEnumerator coroutine);
}