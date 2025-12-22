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
    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, Func<IEnumerator> coroutine)
    {
        var(__key, __coroutine) = (key, coroutine);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1604925717);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __coroutine)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<System.IDisposable>() : __composer.UpdateRememberedValue<System.IDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(coroutine()));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1604925717, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __coroutine));
    }

    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-545396455);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __delay, __block)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<System.IDisposable>() : __composer.UpdateRememberedValue<System.IDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block)));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-545396455, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block));
    }

    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, float delay, Action block)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(758945577);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __delay, __block)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<System.IDisposable>() : __composer.UpdateRememberedValue<System.IDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block)));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(758945577, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block));
    }

    [Composable]
    private static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposable> effect)
    {
        var(__key, __effect) = (key, effect);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-2032713307);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __effect)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<System.IDisposable>() : __composer.UpdateRememberedValue<System.IDisposable>(effect(DisposableEffectScopeImpl.Instance));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-2032713307, __isRestarted)?.UpdateScope(() => __DisposableEffect(__key, __effect));
    }
}