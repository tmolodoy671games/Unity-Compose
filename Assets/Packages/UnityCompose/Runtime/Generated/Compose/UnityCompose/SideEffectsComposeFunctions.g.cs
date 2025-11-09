using System;
using System.Collections;
using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityEngine;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __LaunchedEffect(object? key, IEnumerator coroutine, [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.LaunchedEffect(lineNumber, key, coroutine);
    }

    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __LaunchedEffect(object? key, Action body, [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.LaunchedEffect(lineNumber, key, body);
    }

    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __LaunchedEffect(object? key, TimeSpan delay, Action body, [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.LaunchedEffect(lineNumber, key, RunDelayed(delay, body));
    }

    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __DisposableEffect(object? key, Func<IDisposableEffectScope, IDisposable> body, [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.DisposableEffect(lineNumber, key, Remember<global::System.Func<global::System.IDisposable>>(body, () => body(DisposableEffectScopeImpl.Instance)));
    }
}