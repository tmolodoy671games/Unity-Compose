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
    public static void __LaunchedEffect<TKey>(TKey key, Func<IEnumerator> coroutine, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __coroutine) = (key, coroutine);
        var __isCreated = __composer.StartRestartGroup(344620497);
        var __dirty = __changed;
        if ((__changed & 0b_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_10 : 0b_00_01;
        if ((__changed & 0b_11_00) == 0)
            __dirty |= __composer.Changed(coroutine) ? 0b_10_00 : 0b_01_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var _ = (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11) == 0b_00_10).Get() ? __composer.RememberedValue<global::UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<global::UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsComposeDisposable(coroutine())));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01;
        __composer.EndRestartGroup(344620497, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __coroutine, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __LaunchedEffect<TKey>(TKey key, Action block, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __block) = (key, block);
        var __isCreated = __composer.StartRestartGroup(1894485109);
        var __dirty = __changed;
        if ((__changed & 0b_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_10 : 0b_00_01;
        if ((__changed & 0b_11_00) == 0)
            __dirty |= __composer.Changed(block) ? 0b_10_00 : 0b_01_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var _ = (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11) == 0b_00_10).Get() ? __composer.RememberedValue<string>() : __composer.UpdateRememberedValue<string>(() =>
            {
                block();
                return string.Empty;
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01;
        __composer.EndRestartGroup(1894485109, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __block, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __TestLaunchedEffect<TKey>(TKey key, Action block, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __block) = (key, block);
        var __isCreated = __composer.StartRestartGroup(996962608);
        var __dirty = __changed;
        if ((__changed & 0b_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_10 : 0b_00_01;
        if ((__changed & 0b_11_00) == 0)
            __dirty |= __composer.Changed(block) ? 0b_10_00 : 0b_01_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var _ = (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11) == 0b_00_10).Get() ? __composer.RememberedValue<string>() : __composer.UpdateRememberedValue<string>(() =>
            {
                block();
                return string.Empty;
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01;
        __composer.EndRestartGroup(996962608, __isRestarted)?.UpdateScope(() => __TestLaunchedEffect(__key, __block, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __isCreated = __composer.StartRestartGroup(171949224);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(delay) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(block) ? 0b_10_00_00 : 0b_01_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var _ = (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).Get() ? __composer.RememberedValue<global::UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<global::UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsComposeDisposable(RunDelayed(delay, block))));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(171949224, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __LaunchedEffect<TKey>(TKey key, float delay, Action block, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __isCreated = __composer.StartRestartGroup(1924862291);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(delay) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(block) ? 0b_10_00_00 : 0b_01_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var _ = (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).Get() ? __composer.RememberedValue<global::UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<global::UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsComposeDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block))));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(1924862291, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposableEffectResult> effect, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __effect) = (key, effect);
        var __isCreated = __composer.StartRestartGroup(2059008237);
        var __dirty = __changed;
        if ((__changed & 0b_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_10 : 0b_00_01;
        if ((__changed & 0b_11_00) == 0)
            __dirty |= __composer.Changed(effect) ? 0b_10_00 : 0b_01_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var _ = (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11) == 0b_00_10).Get() ? __composer.RememberedValue<global::UnityCompose.IDisposableEffectResult>() : __composer.UpdateRememberedValue<global::UnityCompose.IDisposableEffectResult>(effect(DisposableEffectScopeImpl.Instance)));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01;
        __composer.EndRestartGroup(2059008237, __isRestarted)?.UpdateScope(() => __DisposableEffect(__key, __effect, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}