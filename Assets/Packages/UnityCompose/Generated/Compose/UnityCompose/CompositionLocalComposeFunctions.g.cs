#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    public static void __CompositionLocalProvider<T1>(CompositionLocalProvides<T1> provides1, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __content) = (provides1, content);
        var __isCreated = __composer.StartRestartGroup(636805308);
        var __dirty = __changed;
        if ((__changed & 0b_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_10 : 0b_00_01;
        if ((__changed & 0b_11_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00 : 0b_01_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_1);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            __composer.StartReplaceGroup(2087134422);
            content();
            __composer.EndReplaceGroup(2087134422);
            composer.EndLocalGroup(123_1);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01;
        __composer.EndRestartGroup(636805308, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __content) = (provides1, provides2, content);
        var __isCreated = __composer.StartRestartGroup(2075615182);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00 : 0b_01_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_2);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            __composer.StartReplaceGroup(1805106646);
            content();
            __composer.EndReplaceGroup(1805106646);
            composer.EndLocalGroup(123_2);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(2075615182, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2, T3>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __provides3, __content) = (provides1, provides2, provides3, content);
        var __isCreated = __composer.StartRestartGroup(150642065);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(provides3) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_3);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            map.Set(provides3);
            __composer.StartReplaceGroup(1730004743);
            content();
            __composer.EndReplaceGroup(1730004743);
            composer.EndLocalGroup(123_3);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01;
        __composer.EndRestartGroup(150642065, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2, T3, T4>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __provides3, __provides4, __content) = (provides1, provides2, provides3, provides4, content);
        var __isCreated = __composer.StartRestartGroup(850864666);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_00_00_10 : 0b_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_00_00_10_00 : 0b_00_00_00_01_00;
        if ((__changed & 0b_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(provides3) ? 0b_00_00_10_00_00 : 0b_00_00_01_00_00;
        if ((__changed & 0b_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(provides4) ? 0b_00_10_00_00_00 : 0b_00_01_00_00_00;
        if ((__changed & 0b_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00_00_00 : 0b_01_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_4);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            map.Set(provides3);
            map.Set(provides4);
            __composer.StartReplaceGroup(2015657451);
            content();
            __composer.EndReplaceGroup(2015657451);
            composer.EndLocalGroup(123_4);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01;
        __composer.EndRestartGroup(850864666, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2, T3, T4, T5>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __content) = (provides1, provides2, provides3, provides4, provides5, content);
        var __isCreated = __composer.StartRestartGroup(1390334920);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_00_00_00_10 : 0b_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_00_00_00_10_00 : 0b_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(provides3) ? 0b_00_00_00_10_00_00 : 0b_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(provides4) ? 0b_00_00_10_00_00_00 : 0b_00_00_01_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides5) ? 0b_00_10_00_00_00_00 : 0b_00_01_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00_00_00_00 : 0b_01_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_5);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            map.Set(provides3);
            map.Set(provides4);
            map.Set(provides5);
            __composer.StartReplaceGroup(770323757);
            content();
            __composer.EndReplaceGroup(770323757);
            composer.EndLocalGroup(123_5);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01;
        __composer.EndRestartGroup(1390334920, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, content);
        var __isCreated = __composer.StartRestartGroup(732686622);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_00_00_00_00_10 : 0b_00_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_00_00_00_00_10_00 : 0b_00_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(provides3) ? 0b_00_00_00_00_10_00_00 : 0b_00_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(provides4) ? 0b_00_00_00_10_00_00_00 : 0b_00_00_00_01_00_00_00;
        if ((__changed & 0b_00_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides5) ? 0b_00_00_10_00_00_00_00 : 0b_00_00_01_00_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides6) ? 0b_00_10_00_00_00_00_00 : 0b_00_01_00_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00_00_00_00_00 : 0b_01_00_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_6);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            map.Set(provides3);
            map.Set(provides4);
            map.Set(provides5);
            map.Set(provides6);
            __composer.StartReplaceGroup(1292491345);
            content();
            __composer.EndReplaceGroup(1292491345);
            composer.EndLocalGroup(123_6);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01_01;
        __composer.EndRestartGroup(732686622, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, CompositionLocalProvides<T7> provides7, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, content);
        var __isCreated = __composer.StartRestartGroup(398130733);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_00_00_00_00_00_10 : 0b_00_00_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_00_00_00_00_00_10_00 : 0b_00_00_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(provides3) ? 0b_00_00_00_00_00_10_00_00 : 0b_00_00_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(provides4) ? 0b_00_00_00_00_10_00_00_00 : 0b_00_00_00_00_01_00_00_00;
        if ((__changed & 0b_00_00_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides5) ? 0b_00_00_00_10_00_00_00_00 : 0b_00_00_00_01_00_00_00_00;
        if ((__changed & 0b_00_00_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides6) ? 0b_00_00_10_00_00_00_00_00 : 0b_00_00_01_00_00_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides7) ? 0b_00_10_00_00_00_00_00_00 : 0b_00_01_00_00_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00_00_00_00_00_00 : 0b_01_00_00_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_7);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            map.Set(provides3);
            map.Set(provides4);
            map.Set(provides5);
            map.Set(provides6);
            map.Set(provides7);
            __composer.StartReplaceGroup(1413997839);
            content();
            __composer.EndReplaceGroup(1413997839);
            composer.EndLocalGroup(123_7);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01_01_01;
        __composer.EndRestartGroup(398130733, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, CompositionLocalProvides<T7> provides7, CompositionLocalProvides<T8> provides8, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, content);
        var __isCreated = __composer.StartRestartGroup(56820169);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_00_00_00_00_00_00_10 : 0b_00_00_00_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_00_00_00_00_00_00_10_00 : 0b_00_00_00_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(provides3) ? 0b_00_00_00_00_00_00_10_00_00 : 0b_00_00_00_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_00_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(provides4) ? 0b_00_00_00_00_00_10_00_00_00 : 0b_00_00_00_00_00_01_00_00_00;
        if ((__changed & 0b_00_00_00_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides5) ? 0b_00_00_00_00_10_00_00_00_00 : 0b_00_00_00_00_01_00_00_00_00;
        if ((__changed & 0b_00_00_00_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides6) ? 0b_00_00_00_10_00_00_00_00_00 : 0b_00_00_00_01_00_00_00_00_00;
        if ((__changed & 0b_00_00_11_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides7) ? 0b_00_00_10_00_00_00_00_00_00 : 0b_00_00_01_00_00_00_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides8) ? 0b_00_10_00_00_00_00_00_00_00 : 0b_00_01_00_00_00_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00_00_00_00_00_00_00 : 0b_01_00_00_00_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_8);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            map.Set(provides3);
            map.Set(provides4);
            map.Set(provides5);
            map.Set(provides6);
            map.Set(provides7);
            map.Set(provides8);
            __composer.StartReplaceGroup(623892538);
            content();
            __composer.EndReplaceGroup(623892538);
            composer.EndLocalGroup(123_8);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01_01_01_01;
        __composer.EndRestartGroup(56820169, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8, T9>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, CompositionLocalProvides<T7> provides7, CompositionLocalProvides<T8> provides8, CompositionLocalProvides<T9> provides9, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, content);
        var __isCreated = __composer.StartRestartGroup(2060835784);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_00_00_00_00_00_00_00_10 : 0b_00_00_00_00_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_00_00_00_00_00_00_00_10_00 : 0b_00_00_00_00_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_00_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(provides3) ? 0b_00_00_00_00_00_00_00_10_00_00 : 0b_00_00_00_00_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_00_00_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(provides4) ? 0b_00_00_00_00_00_00_10_00_00_00 : 0b_00_00_00_00_00_00_01_00_00_00;
        if ((__changed & 0b_00_00_00_00_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides5) ? 0b_00_00_00_00_00_10_00_00_00_00 : 0b_00_00_00_00_00_01_00_00_00_00;
        if ((__changed & 0b_00_00_00_00_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides6) ? 0b_00_00_00_00_10_00_00_00_00_00 : 0b_00_00_00_00_01_00_00_00_00_00;
        if ((__changed & 0b_00_00_00_11_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides7) ? 0b_00_00_00_10_00_00_00_00_00_00 : 0b_00_00_00_01_00_00_00_00_00_00;
        if ((__changed & 0b_00_00_11_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides8) ? 0b_00_00_10_00_00_00_00_00_00_00 : 0b_00_00_01_00_00_00_00_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides9) ? 0b_00_10_00_00_00_00_00_00_00_00 : 0b_00_01_00_00_00_00_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00_00_00_00_00_00_00_00 : 0b_01_00_00_00_00_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01_01_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_9);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            map.Set(provides3);
            map.Set(provides4);
            map.Set(provides5);
            map.Set(provides6);
            map.Set(provides7);
            map.Set(provides8);
            map.Set(provides9);
            __composer.StartReplaceGroup(469841377);
            content();
            __composer.EndReplaceGroup(469841377);
            composer.EndLocalGroup(123_9);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01_01_01_01_01;
        __composer.EndRestartGroup(2060835784, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, CompositionLocalProvides<T7> provides7, CompositionLocalProvides<T8> provides8, CompositionLocalProvides<T9> provides9, CompositionLocalProvides<T10> provides10, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __provides10, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, provides10, content);
        var __isCreated = __composer.StartRestartGroup(1885066283);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(provides1) ? 0b_00_00_00_00_00_00_00_00_00_00_10 : 0b_00_00_00_00_00_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_00_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(provides2) ? 0b_00_00_00_00_00_00_00_00_00_10_00 : 0b_00_00_00_00_00_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_00_00_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(provides3) ? 0b_00_00_00_00_00_00_00_00_10_00_00 : 0b_00_00_00_00_00_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_00_00_00_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(provides4) ? 0b_00_00_00_00_00_00_00_10_00_00_00 : 0b_00_00_00_00_00_00_00_01_00_00_00;
        if ((__changed & 0b_00_00_00_00_00_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides5) ? 0b_00_00_00_00_00_00_10_00_00_00_00 : 0b_00_00_00_00_00_00_01_00_00_00_00;
        if ((__changed & 0b_00_00_00_00_00_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides6) ? 0b_00_00_00_00_00_10_00_00_00_00_00 : 0b_00_00_00_00_00_01_00_00_00_00_00;
        if ((__changed & 0b_00_00_00_00_11_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides7) ? 0b_00_00_00_00_10_00_00_00_00_00_00 : 0b_00_00_00_00_01_00_00_00_00_00_00;
        if ((__changed & 0b_00_00_00_11_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides8) ? 0b_00_00_00_10_00_00_00_00_00_00_00 : 0b_00_00_00_01_00_00_00_00_00_00_00;
        if ((__changed & 0b_00_00_11_00_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides9) ? 0b_00_00_10_00_00_00_00_00_00_00_00 : 0b_00_00_01_00_00_00_00_00_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(provides10) ? 0b_00_10_00_00_00_00_00_00_00_00_00 : 0b_00_01_00_00_00_00_00_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00_00_00_00_00_00_00_00_00 : 0b_01_00_00_00_00_00_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01_01_01_01_01_01)
        {
            var composer = __composer;
            composer.StartLocalGroup(123_10);
            var map = composer.RequireCompositionLocalMap();
            map.Set(provides1);
            map.Set(provides2);
            map.Set(provides3);
            map.Set(provides4);
            map.Set(provides5);
            map.Set(provides6);
            map.Set(provides7);
            map.Set(provides8);
            map.Set(provides9);
            map.Set(provides10);
            __composer.StartReplaceGroup(325177385);
            content();
            __composer.EndReplaceGroup(325177385);
            composer.EndLocalGroup(123_10);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01_01_01_01_01_01;
        __composer.EndRestartGroup(1885066283, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __provides10, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}