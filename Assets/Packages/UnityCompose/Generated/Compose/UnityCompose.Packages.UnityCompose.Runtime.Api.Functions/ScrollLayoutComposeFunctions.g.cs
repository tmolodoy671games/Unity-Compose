#nullable enable
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Api.Functions;
public static partial class ComposeFunctions
{
    public static void __ScrollableColumn(ComposableContent content, float elasticity = -1f, long elasticAnimationIntervalMs = -1, float scrollOffset = -1, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __elasticity, __elasticAnimationIntervalMs, __scrollOffset, __modifier) = (content, elasticity, elasticAnimationIntervalMs, scrollOffset, modifier);
        var __isCreated = __composer.StartRestartGroup(910354089);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_00_10 : 0b_00_00_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_01;
        }

        if ((__changed & 0b_00_00_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(elasticity) ? 0b_00_00_00_10_00 : 0b_00_00_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_01_00;
        }

        if ((__changed & 0b_00_00_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(elasticAnimationIntervalMs) ? 0b_00_00_10_00_00 : 0b_00_00_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01_00_00;
        }

        if ((__changed & 0b_00_11_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(scrollOffset) ? 0b_00_10_00_00_00 : 0b_00_01_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00_00_00;
        }

        if ((__changed & 0b_11_00_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00 : 0b_01_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01)
        {
            __ScrollableLayout(mode: ScrollViewMode.Vertical, elasticity: elasticity, elasticAnimationIntervalMs: elasticAnimationIntervalMs, scrollOffset: scrollOffset >= 0 ? new Vector2(0, scrollOffset) : default, modifier: modifier, content: content, __composer: __composer, __changed: 0b_00_00_00_00_01_00 | (__dirty & 0b_00_00_00_00_00_11) | ((__dirty & 0b_00_00_00_00_11_00) << 2) | ((__dirty & 0b_00_00_00_11_00_00) << 2) | ((__dirty & 0b_00_11_00_00_00_00) << 2));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01;
        __composer.EndRestartGroup(910354089, __isRestarted)?.UpdateScope(() => __ScrollableColumn(__content, __elasticity, __elasticAnimationIntervalMs, __scrollOffset, __modifier, __composer, __dirtyRestart));
    }

    private static void __ScrollableLayout(ComposableContent content, ScrollViewMode mode, float elasticity, long elasticAnimationIntervalMs, Optional<Vector2> scrollOffset, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __mode, __elasticity, __elasticAnimationIntervalMs, __scrollOffset, __modifier) = (content, mode, elasticity, elasticAnimationIntervalMs, scrollOffset, modifier);
        var __isCreated = __composer.StartRestartGroup(176979557);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_00_00_10 : 0b_00_00_00_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_00_01;
        }

        if ((__changed & 0b_00_00_00_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(mode) ? 0b_00_00_00_00_10_00 : 0b_00_00_00_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_01_00;
        }

        if ((__changed & 0b_00_00_00_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(elasticity) ? 0b_00_00_00_10_00_00 : 0b_00_00_00_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_01_00_00;
        }

        if ((__changed & 0b_00_00_11_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(elasticAnimationIntervalMs) ? 0b_00_00_10_00_00_00 : 0b_00_00_01_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01_00_00_00;
        }

        if ((__changed & 0b_00_11_00_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(scrollOffset) ? 0b_00_10_00_00_00_00 : 0b_00_01_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00_00_00_00;
        }

        if ((__changed & 0b_11_00_00_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00_00 : 0b_01_00_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00_00_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01)
        {
            __ReusableComposeView<ScrollView>(initializer: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_00_11_00) == 0b_00_00_00_00_10_00).ChangedAsFlag((__dirty & 0b_00_00_00_11_00_00) == 0b_00_00_00_10_00_00).ChangedAsFlag((__dirty & 0b_00_00_11_00_00_00) == 0b_00_00_10_00_00_00).ChangedAsFlag((__dirty & 0b_00_11_00_00_00_00) == 0b_00_10_00_00_00_00).Get() ? __composer.RememberedValue<global::System.Action<global::UnityEngine.UIElements.ScrollView>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityEngine.UIElements.ScrollView>>(it =>
            {
                it.mode = mode;
                if (elasticity >= 0)
                    it.elasticity = elasticity;
                if (elasticAnimationIntervalMs >= 0)
                    it.elasticAnimationIntervalMs = elasticAnimationIntervalMs;
                if (scrollOffset.HasValue)
                    it.scrollOffset = scrollOffset.Value;
            })), modifier: modifier, content: content, __composer: __composer, __changed: ((__dirty & 0b_11_00_00_00_00_00) >> 10) | ((__dirty & 0b_00_00_11) << 4));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01;
        __composer.EndRestartGroup(176979557, __isRestarted)?.UpdateScope(() => __ScrollableLayout(__content, __mode, __elasticity, __elasticAnimationIntervalMs, __scrollOffset, __modifier, __composer, __dirtyRestart));
    }
}