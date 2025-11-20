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
        CurrentComposer.Remember<TKey, IDisposable>(
            key: key,
            defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(coroutine())
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect<TKey>(
        TKey key,
        Action block
    )
    {
        CurrentComposer.Remember(
            key: key,
            defaultValueFactory: _ =>
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
        CurrentComposer.Remember(
            key: key,
            defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block))
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect<TKey>(
        TKey key,
        float delay,
        Action block
    )
    {
        CurrentComposer.Remember(
            key: key,
            defaultValueFactory: _ =>
                ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block))
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void DisposableEffect<TKey>(
        TKey key,
        Func<IDisposableEffectScope, IDisposable> effect
    )
    {
        CurrentComposer.Remember(
            key: key,
            defaultValueFactory: _ => effect(DisposableEffectScopeImpl.Instance)
        );
    }

    private static IEnumerator RunDelayed(TimeSpan delay, Action action)
    {
        yield return new WaitForSeconds(delay.TotalSeconds.ToFloat());
        action();
    }
}