// ReSharper disable CheckNamespace

using System;
using System.Collections;
using Compose.Net;

namespace UnityCompose;

public static partial class UnityComposeFunctions
{
    [Composable]
    public static void LaunchedEffect<TKey>(
        TKey key,
        Func<IEnumerator> coroutine
    )
    {
        DisposableEffect(
            key: key,
            effect: () =>
            {
                var coroutineInstance = ComposeInvalidatorHolder.Instance.StartCoroutine(coroutine());
                return OnDispose(() => ComposeInvalidatorHolder.Instance.StopCoroutine(coroutineInstance));
            }
        );
    }
}