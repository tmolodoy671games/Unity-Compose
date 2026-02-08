#nullable enable
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
        __composer.StartRestartGroup(344620497);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __coroutine)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(coroutine()));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(344620497, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __coroutine));
    }

    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, Action block)
    {
        var(__key, __block) = (key, block);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(716928859);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __block)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<string>() : __composer.UpdateRememberedValue<string>(() =>
            {
                block();
                return string.Empty;
            });
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(716928859, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __block));
    }

    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1549277972);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __delay, __block)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block)));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1549277972, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block));
    }

    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, float delay, Action block)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1767914563);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __delay, __block)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block)));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1767914563, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block));
    }

    [Composable]
    private static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposableEffectResult> effect)
    {
        var(__key, __effect) = (key, effect);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1073799068);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __effect)))
        {
            var _ = !__composer.Changed(key) ? __composer.RememberedValue<UnityCompose.IDisposableEffectResult>() : __composer.UpdateRememberedValue<UnityCompose.IDisposableEffectResult>(effect(DisposableEffectScopeImpl.Instance));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1073799068, __isRestarted)?.UpdateScope(() => __DisposableEffect(__key, __effect));
    }
}