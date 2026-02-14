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
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_11) == 0)
        {
            __dirty |= __composer.Changed(key) ? 0b_00_10 : 0b_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_01;
        }

        if ((__changed & 0b_11_00) == 0)
        {
            __dirty |= __composer.Changed(coroutine) ? 0b_10_00 : 0b_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var _ = (!__composer.Changed(key) ? __composer.RememberedValue<UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(coroutine())));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(344620497, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __coroutine, __composer, __dirtyRestart));
    }

    public static void __LaunchedEffect<TKey>(TKey key, Action block, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __block) = (key, block);
        var __isCreated = __composer.StartRestartGroup(716928859);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_11) == 0)
        {
            __dirty |= __composer.Changed(key) ? 0b_00_10 : 0b_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_01;
        }

        if ((__changed & 0b_11_00) == 0)
        {
            __dirty |= __composer.Changed(block) ? 0b_10_00 : 0b_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var _ = (!__composer.Changed(key) ? __composer.RememberedValue<string>() : __composer.UpdateRememberedValue<string>(() =>
            {
                block();
                return string.Empty;
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(716928859, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __block, __composer, __dirtyRestart));
    }

    public static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __isCreated = __composer.StartRestartGroup(1549277972);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(key) ? 0b_00_00_10 : 0b_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01;
        }

        if ((__changed & 0b_00_11_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(delay) ? 0b_00_10_00 : 0b_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00;
        }

        if ((__changed & 0b_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(block) ? 0b_10_00_00 : 0b_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var _ = (!__composer.Changed(key) ? __composer.RememberedValue<UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block))));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1549277972, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block, __composer, __dirtyRestart));
    }

    public static void __LaunchedEffect<TKey>(TKey key, float delay, Action block, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __delay, __block) = (key, delay, block);
        var __isCreated = __composer.StartRestartGroup(1767914563);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(key) ? 0b_00_00_10 : 0b_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01;
        }

        if ((__changed & 0b_00_11_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(delay) ? 0b_00_10_00 : 0b_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00;
        }

        if ((__changed & 0b_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(block) ? 0b_10_00_00 : 0b_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var _ = (!__composer.Changed(key) ? __composer.RememberedValue<UnityCompose.IComposeDisposable>() : __composer.UpdateRememberedValue<UnityCompose.IComposeDisposable>(ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block))));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1767914563, __isRestarted)?.UpdateScope(() => __LaunchedEffect(__key, __delay, __block, __composer, __dirtyRestart));
    }

    public static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposableEffectResult> effect, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __effect) = (key, effect);
        var __isCreated = __composer.StartRestartGroup(1073799068);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_11) == 0)
        {
            __dirty |= __composer.Changed(key) ? 0b_00_10 : 0b_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_01;
        }

        if ((__changed & 0b_11_00) == 0)
        {
            __dirty |= __composer.Changed(effect) ? 0b_10_00 : 0b_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var _ = (!__composer.Changed(key) ? __composer.RememberedValue<UnityCompose.IDisposableEffectResult>() : __composer.UpdateRememberedValue<UnityCompose.IDisposableEffectResult>(effect(DisposableEffectScopeImpl.Instance)));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1073799068, __isRestarted)?.UpdateScope(() => __DisposableEffect(__key, __effect, __composer, __dirtyRestart));
    }
}