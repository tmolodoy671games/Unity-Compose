using System;
using System.Collections;
using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    private class DisposableEffectScopeImpl : IDisposableEffectScope
    {
        public static readonly DisposableEffectScopeImpl Instance = new();

        private DisposableEffectScopeImpl()
        {
        }

        public IDisposable OnDispose(Action onDispose)
        {
            return new CallbackDisposableImpl(onDispose);
        }
    }

    private class CallbackDisposableImpl : IDisposable
    {
        private bool _isDisposed;
        private readonly Action _onDispose;

        public CallbackDisposableImpl(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;
            _onDispose();
        }
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect<TKey>(
        TKey key,
        Func<IEnumerator> coroutine
    )
    {
        Remember(
            key: key,
            defaultValueFactory: () => ComposeInvalidator.StartCoroutineAsDisposable(coroutine())
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect<TKey>(
        TKey key,
        Action block
    )
    {
        Remember(
            key: key,
            defaultValueFactory: () =>
            {
                block();
                return string.Empty;
            }
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect<TKey>(
        TKey key,
        TimeSpan delay,
        Action block
    )
    {
        Remember(
            key: key,
            defaultValueFactory: () => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block))
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect<TKey>(
        TKey key,
        float delay,
        Action block
    )
    {
        Remember(
            key: key,
            defaultValueFactory: () =>
                ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block))
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void DisposableEffect<TKey>(
        TKey key,
        Func<IDisposableEffectScope, IDisposable> effect
    )
    {
        Remember(
            key: key,
            defaultValueFactory: () => effect(DisposableEffectScopeImpl.Instance)
        );
    }

    private static IEnumerator RunDelayed(TimeSpan delay, Action action)
    {
        yield return new WaitForSeconds(delay.TotalSeconds.ToFloat());
        action();
    }
}