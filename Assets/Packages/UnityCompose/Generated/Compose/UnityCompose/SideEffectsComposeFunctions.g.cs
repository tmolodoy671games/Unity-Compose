using System;
using System.Collections;
using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityEngine;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, Func<IEnumerator> coroutine)
    {
        CurrentComposer.Remember<TKey, IDisposable>(key: key, defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(coroutine()));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, Action block)
    {
        CurrentComposer.Remember(key: key, defaultValueFactory: _ =>
        {
            block();
            return string.Empty;
        });
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block)
    {
        CurrentComposer.Remember(key: key, defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block)));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, float delay, Action block)
    {
        CurrentComposer.Remember(key: key, defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block)));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposable> effect)
    {
        CurrentComposer.Remember(key: key, defaultValueFactory: _ => effect(DisposableEffectScopeImpl.Instance));
    }
}