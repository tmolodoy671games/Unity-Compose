#nullable enable
using System;
using System.Collections;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    public static ScrollState __RememberScrollState(float initialValue = 0, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_11) == 0)
        {
            __dirty |= __composer.Changed(initialValue) ? 0b_10 : 0b_01;
        }
        else
        {
            __dirtyRestart |= 0b_01;
        }

        return (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ScrollState>() : __composer.UpdateRememberedValue<global::UnityCompose.ScrollState>(new ScrollState(initialValue)));
    }

    public static void __ScrollableColumn(ScrollState state, ComposableContent content, float scrollStrength = 1f, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__state, __content, __scrollStrength, __modifier) = (state, content, scrollStrength, modifier);
        var __isCreated = __composer.StartRestartGroup(1253564660);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(state) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_01;
        }

        if ((__changed & 0b_00_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01_00;
        }

        if ((__changed & 0b_00_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(scrollStrength) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00_00;
        }

        if ((__changed & 0b_11_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01)
        {
            __ReusableComposeView<ScrollableColumn>(initializer: (!__composer.Changed() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.ScrollableColumn>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.ScrollableColumn>>(it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = Align.FlexStart;
                it.style.justifyContent = Justify.FlexStart;
            })), modifier: modifier.OrEmpty().Clip().OnGloballyPositioned((!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it =>
            {
                if (!float.IsNaN(it.Height))
                    state.ViewportSize = it.Height;
            }))).OnVerticalScroll((!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_11_00_00) == 0b_00_10_00_00).Get() ? __composer.RememberedValue<global::System.Action<float>>() : __composer.UpdateRememberedValue<global::System.Action<float>>(it => state.AnimateScrollBy(scrollStrength * DefaultMultiplier * it)))), content: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_11_00) == 0b_00_00_10_00).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __ReusableComposeView<ScrollableColumnContent>(initializer: (!__composer.Changed() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.ScrollableColumnContent>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.ScrollableColumnContent>>(it =>
                {
                    it.style.flexDirection = FlexDirection.Column;
                    it.style.alignItems = Align.FlexStart;
                    it.style.justifyContent = Justify.FlexStart;
                })), modifier: Modifier.Offset(y: -state.Value.Px()).OnGloballyPositioned((!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it =>
                {
                    if (!float.IsNaN(it.Height))
                        state.ContentSize = it.Height;
                }))), content: content, __composer: __composer, __changed: ((__dirty & 0b_00_11_00) << 2));
            })), __composer: __composer, __changed: 0b_00_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01;
        __composer.EndRestartGroup(1253564660, __isRestarted)?.UpdateScope(() => __ScrollableColumn(__state, __content, __scrollStrength, __modifier, __composer, __dirtyRestart));
    }

    public static void __ScrollableRow(ScrollState state, ComposableContent content, float scrollStrength = 1f, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__state, __content, __scrollStrength, __modifier) = (state, content, scrollStrength, modifier);
        var __isCreated = __composer.StartRestartGroup(152078748);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(state) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_01;
        }

        if ((__changed & 0b_00_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01_00;
        }

        if ((__changed & 0b_00_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(scrollStrength) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00_00;
        }

        if ((__changed & 0b_11_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01)
        {
            __ReusableComposeView<ScrollableRow>(initializer: (!__composer.Changed() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.ScrollableRow>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.ScrollableRow>>(it =>
            {
                it.style.flexDirection = FlexDirection.Column;
                it.style.alignItems = Align.FlexStart;
                it.style.justifyContent = Justify.FlexStart;
            })), modifier: modifier.OrEmpty().Clip().OnGloballyPositioned((!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => state.ViewportSize = it.Width))).OnVerticalScroll((!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_11_00_00) == 0b_00_10_00_00).Get() ? __composer.RememberedValue<global::System.Action<float>>() : __composer.UpdateRememberedValue<global::System.Action<float>>(it => state.AnimateScrollBy(scrollStrength * DefaultMultiplier * it)))).OnHorizontalScroll((!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_11_00_00) == 0b_00_10_00_00).Get() ? __composer.RememberedValue<global::System.Action<float>>() : __composer.UpdateRememberedValue<global::System.Action<float>>(it => state.AnimateScrollBy(scrollStrength * DefaultMultiplier * it)))), content: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_11_00) == 0b_00_00_10_00).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __ReusableComposeView<ScrollableRowContent>(initializer: (!__composer.Changed() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.ScrollableRowContent>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.ScrollableRowContent>>(it =>
                {
                    it.style.flexDirection = FlexDirection.Row;
                    it.style.alignItems = Align.FlexStart;
                    it.style.justifyContent = Justify.FlexStart;
                })), modifier: Modifier.Offset(x: -state.Value.Px()).OnGloballyPositioned((!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => state.ContentSize = it.Width))), content: content, __composer: __composer, __changed: ((__dirty & 0b_00_11_00) << 2));
            })), __composer: __composer, __changed: 0b_00_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01;
        __composer.EndRestartGroup(152078748, __isRestarted)?.UpdateScope(() => __ScrollableRow(__state, __content, __scrollStrength, __modifier, __composer, __dirtyRestart));
    }
}