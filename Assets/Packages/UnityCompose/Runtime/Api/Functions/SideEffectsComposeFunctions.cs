using System;
using System.Collections;
using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
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
    public static void LaunchedEffect(
        object? key,
        IEnumerator coroutine,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.LaunchedEffect(new RememberId(filePath, lineNumber), key, coroutine);
    }
        
    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect(
        object? key,
        Action block,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.LaunchedEffect(new RememberId(filePath, lineNumber), key, block);
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect(
        object? key,
        TimeSpan delay,
        Action block,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.LaunchedEffect(new RememberId(filePath, lineNumber), key, RunDelayed(delay, block));
    }

    [Composable, DontGenerateComposeGroups]
    public static void DisposableEffect(
        object? key,
        Func<IDisposableEffectScope, IDisposable> effect,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.DisposableEffect(
            new RememberId(filePath, lineNumber),
            key,
            () => effect(DisposableEffectScopeImpl.Instance)
        );
    }

    private static IEnumerator RunDelayed(TimeSpan delay, Action action)
    {
        yield return new WaitForSeconds(delay.TotalSeconds.ToFloat());
        action();
    }
}