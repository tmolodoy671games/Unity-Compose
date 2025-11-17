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
    public static void LaunchedEffect<TKey>(
        TKey key,
        Func<IEnumerator> coroutine,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.Remember(
            key: new ComposeKey(
                FileName: filePath,
                MemberName: memberName,
                LineNumber: lineNumber
            ),
            compareKey: key,
            defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(coroutine())
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect<TKey>(
        TKey key,
        Action block,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.Remember(
            key: new ComposeKey(
                FileName: filePath,
                MemberName: memberName,
                LineNumber: lineNumber
            ),
            compareKey: key,
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
        Action block,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.Remember(
            key: new ComposeKey(
                FileName: filePath,
                MemberName: memberName,
                LineNumber: lineNumber
            ),
            compareKey: key,
            defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block))
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void LaunchedEffect<TKey>(
        TKey key,
        float delay,
        Action block,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.Remember(
            key: new ComposeKey(
                FileName: filePath,
                MemberName: memberName,
                LineNumber: lineNumber
            ),
            compareKey: key,
            defaultValueFactory: _ =>
                ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block))
        );
    }

    [Composable, DontGenerateComposeGroups]
    public static void DisposableEffect<TKey>(
        TKey key,
        Func<IDisposableEffectScope, IDisposable> effect,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CurrentComposer.Remember(
            key: new ComposeKey(filePath, memberName, lineNumber),
            compareKey: key,
            defaultValueFactory: _ => effect(DisposableEffectScopeImpl.Instance)
        );
    }

    private static IEnumerator RunDelayed(TimeSpan delay, Action action)
    {
        yield return new WaitForSeconds(delay.TotalSeconds.ToFloat());
        action();
    }
}