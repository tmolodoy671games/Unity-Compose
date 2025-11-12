using System;
using System.Collections;
using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __LaunchedEffect(object? key, IEnumerator coroutine, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.LaunchedEffect(new RememberId(filePath, lineNumber), key, coroutine);
    }

    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __LaunchedEffect(object? key, Action block, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.LaunchedEffect(new RememberId(filePath, lineNumber), key, block);
    }

    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __LaunchedEffect(object? key, TimeSpan delay, Action block, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.LaunchedEffect(new RememberId(filePath, lineNumber), key, RunDelayed(delay, block));
    }

    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __DisposableEffect(object? key, Func<IDisposableEffectScope, IDisposable> effect, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.DisposableEffect(new RememberId(filePath, lineNumber), key, Remember<global::System.Func<global::System.IDisposable>>(effect, () => effect(DisposableEffectScopeImpl.Instance)));
    }
}