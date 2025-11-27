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
        var _ = CurrentComposer.HasRememberedValue<TKey?, System.IDisposable>(-1204554348, key) ? CurrentComposer.RememberedValue<TKey?, System.IDisposable>() : CurrentComposer.WriteValue<TKey?, System.IDisposable>(() => ComposeInvalidator.StartCoroutineAsDisposable(coroutine()));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, Action block)
    {
        var _ = CurrentComposer.HasRememberedValue<TKey?, string>(308921892, key) ? CurrentComposer.RememberedValue<TKey?, string>() : CurrentComposer.WriteValue<TKey?, string>(() =>
        {
            block();
            return string.Empty;
        });
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block)
    {
        var _ = CurrentComposer.HasRememberedValue<TKey?, System.IDisposable>(-912873467, key) ? CurrentComposer.RememberedValue<TKey?, System.IDisposable>() : CurrentComposer.WriteValue<TKey?, System.IDisposable>(() => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block)));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, float delay, Action block)
    {
        var _ = CurrentComposer.HasRememberedValue<TKey?, System.IDisposable>(-1321647203, key) ? CurrentComposer.RememberedValue<TKey?, System.IDisposable>() : CurrentComposer.WriteValue<TKey?, System.IDisposable>(() => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block)));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposable> effect)
    {
        var _ = CurrentComposer.HasRememberedValue<TKey?, System.IDisposable>(-627405140, key) ? CurrentComposer.RememberedValue<TKey?, System.IDisposable>() : CurrentComposer.WriteValue<TKey?, System.IDisposable>(() => effect(DisposableEffectScopeImpl.Instance));
    }
}