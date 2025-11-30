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
        var _ = CurrentComposer.HasRememberedValue<TKey?, System.IDisposable>(-482592511, key) ? CurrentComposer.RememberedValue<TKey?, System.IDisposable>() : CurrentComposer.WriteValue<TKey?, System.IDisposable>(() => ComposeInvalidator.StartCoroutineAsDisposable(coroutine()));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, Action block)
    {
        var _ = CurrentComposer.HasRememberedValue<TKey?, string>(1173313406, key) ? CurrentComposer.RememberedValue<TKey?, string>() : CurrentComposer.WriteValue<TKey?, string>(() =>
        {
            block();
            return string.Empty;
        });
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block)
    {
        var _ = CurrentComposer.HasRememberedValue<TKey?, System.IDisposable>(691399711, key) ? CurrentComposer.RememberedValue<TKey?, System.IDisposable>() : CurrentComposer.WriteValue<TKey?, System.IDisposable>(() => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block)));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, float delay, Action block)
    {
        var _ = CurrentComposer.HasRememberedValue<TKey?, System.IDisposable>(-1957850529, key) ? CurrentComposer.RememberedValue<TKey?, System.IDisposable>() : CurrentComposer.WriteValue<TKey?, System.IDisposable>(() => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block)));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposable> effect)
    {
        var _ = CurrentComposer.HasRememberedValue<TKey?, System.IDisposable>(281098790, key) ? CurrentComposer.RememberedValue<TKey?, System.IDisposable>() : CurrentComposer.WriteValue<TKey?, System.IDisposable>(() => effect(DisposableEffectScopeImpl.Instance));
    }
}