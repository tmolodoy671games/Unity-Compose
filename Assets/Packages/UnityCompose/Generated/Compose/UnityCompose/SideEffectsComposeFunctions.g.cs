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
        __composer.StartRestartGroup(1736125028);
        if (__composer.ShouldExecute((__key, __coroutine)))
        {
            var _ = !__composer.RememberedKeyChanged<TKey?>(-1204554348, key) ? __composer.RememberedValue<System.IDisposable>() : __composer.UpdateRememberedValue<System.IDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(coroutine()));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1736125028)?.UpdateScope(() => __LaunchedEffect(__key, __coroutine));
    }

    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, Action block)
    {
        var(__key, __block) = (key, block);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1078729898);
        if (__composer.ShouldExecute((__key, __block)))
        {
            var _ = !__composer.RememberedKeyChanged<TKey?>(308921892, key) ? __composer.RememberedValue<string>() : __composer.UpdateRememberedValue<string>(() =>
            {
                block();
                return string.Empty;
            });
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1078729898)?.UpdateScope(() => __LaunchedEffect(__key, __block));
    }

    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1631362445);
        if (__composer.ShouldExecute((__key, __delay, __block)))
        {
            var _ = !__composer.RememberedKeyChanged<TKey?>(-912873467, key) ? __composer.RememberedValue<System.IDisposable>() : __composer.UpdateRememberedValue<System.IDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block)));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1631362445)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block));
    }

    [Composable]
    private static void __LaunchedEffect<TKey>(TKey key, float delay, Action block)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(483082571);
        if (__composer.ShouldExecute((__key, __delay, __block)))
        {
            var _ = !__composer.RememberedKeyChanged<TKey?>(-1321647203, key) ? __composer.RememberedValue<System.IDisposable>() : __composer.UpdateRememberedValue<System.IDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block)));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(483082571)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block));
    }

    [Composable]
    private static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposable> effect)
    {
        var(__key, __effect) = (key, effect);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1949177568);
        if (__composer.ShouldExecute((__key, __effect)))
        {
            var _ = !__composer.RememberedKeyChanged<TKey?>(-627405140, key) ? __composer.RememberedValue<System.IDisposable>() : __composer.UpdateRememberedValue<System.IDisposable>(effect(DisposableEffectScopeImpl.Instance));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1949177568)?.UpdateScope(() => __DisposableEffect(__key, __effect));
    }
}