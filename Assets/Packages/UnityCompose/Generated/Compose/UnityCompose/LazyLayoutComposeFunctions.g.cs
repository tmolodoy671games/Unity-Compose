#nullable enable
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class ComposeFunctions
{
    public static LazyListState __RememberLazyListState(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        return (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.LazyListState>() : __composer.UpdateRememberedValue<global::UnityCompose.LazyListState>(new LazyListState(0f)));
    }

    public static void __LazyColumn(Action<ILazyListScope> content, LazyListState? state = null, float scrollStrength = 1f, Alignment.Horizontal? horizontalAlignment = null, Arrangement.Vertical? verticalArrangement = null, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __state, __scrollStrength, __horizontalAlignment, __verticalArrangement, __modifier) = (content, state, scrollStrength, horizontalAlignment, verticalArrangement, modifier);
        var __isCreated = __composer.StartRestartGroup(1400034430);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_00_00_10 : 0b_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(state) ? 0b_00_00_00_00_10_00 : 0b_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(scrollStrength) ? 0b_00_00_00_10_00_00 : 0b_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(horizontalAlignment) ? 0b_00_00_10_00_00_00 : 0b_00_00_01_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(verticalArrangement) ? 0b_00_10_00_00_00_00 : 0b_00_01_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00_00 : 0b_01_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01)
        {
            var resolvedState = state ?? (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.LazyListState>() : __composer.UpdateRememberedValue<global::UnityCompose.LazyListState>(new LazyListState(0f)));
            var scope = (!__composer.Changed<global::UnityCompose.LazyListState>(resolvedState!) ? __composer.RememberedValue<global::UnityCompose.LazyListScopeImpl>() : __composer.UpdateRememberedValue<global::UnityCompose.LazyListScopeImpl>(new LazyListScopeImpl(resolvedState)));
            __ReusableComposeView<LazyColumn>(initializer: (!__composer.Changed() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LazyColumn>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LazyColumn>>(it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = Align.FlexStart;
                it.style.justifyContent = Justify.FlexStart;
            })), modifier: modifier.OrEmpty().Clip().OnGloballyPositioned((!__composer.Changed<global::UnityCompose.LazyListState>(resolvedState!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => resolvedState.ViewportSize = it.Height))).OnVerticalScroll(onVerticalScroll: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_11_00_00) == 0b_00_00_00_10_00_00).Changed<global::UnityCompose.LazyListState>(resolvedState!).Get() ? __composer.RememberedValue<global::System.Action<float>>() : __composer.UpdateRememberedValue<global::System.Action<float>>(it => resolvedState.AnimateScrollBy(scrollStrength * DefaultScrollMultiplier * it)))), content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_00_11) == 0b_00_00_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_11_00_00_00) == 0b_00_00_10_00_00_00).ChangedAsFlag((__dirty & 0b_00_11_00_00_00_00) == 0b_00_10_00_00_00_00).Changed<global::UnityCompose.LazyListState>(resolvedState!).Changed<global::UnityCompose.LazyListScopeImpl>(scope!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __ReusableComposeView<LazyColumnContent>(initializer: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11_00_00_00) == 0b_00_00_10_00_00_00).ChangedAsFlag((__dirty & 0b_00_11_00_00_00_00) == 0b_00_10_00_00_00_00).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LazyColumnContent>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LazyColumnContent>>(it =>
                {
                    it.style.flexDirection = FlexDirection.Column;
                    it.style.alignItems = (horizontalAlignment ?? Alignment.Left).ToAlign();
                    it.style.justifyContent = (verticalArrangement ?? Arrangement.Top).ToJustify();
                })), modifier: Modifier.Offset(y: -resolvedState.Value.Dp()).OnGloballyPositioned((!__composer.Changed<global::UnityCompose.LazyListState>(resolvedState!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => resolvedState.ContentSize = it.Height))), content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_00_11) == 0b_00_00_00_00_00_10).Changed<global::UnityCompose.LazyListState>(resolvedState!).Changed<global::UnityCompose.LazyListScopeImpl>(scope!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __SideEffect((scope, content), (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_00_11) == 0b_00_00_00_00_00_10).Changed<global::UnityCompose.LazyListScopeImpl>(scope!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                    {
                        scope.Clear();
                        content(scope);
                    })), __composer: __composer, __changed: 0b_00_00);
                    var items = resolvedState.Items;
                    __composer.StartReplaceGroup(1777549368);
                    for (var i = 0; i < items.Count; i++)
                    {
                        var currentI = i;
                        var item = items[i];
                        Key(key: item.Key, content: (!__composer.BuildChanged().Changed<global::UnityCompose.LazyListState>(resolvedState!).Changed<int>(currentI!).Changed<global::UnityCompose.LazyListRecord>(item!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __composer.StartReplaceGroup(1344787836);
                            __ReusableComposeView<LazyListItem>(modifier: Modifier.OnLocallyPositioned((!__composer.BuildChanged().Changed<global::UnityCompose.LazyListState>(resolvedState!).Changed<int>(currentI!).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => resolvedState.SyncOffset(currentI, it.LocalTop)))), content: (!__composer.BuildChanged().Changed<int>(currentI!).Changed<global::UnityCompose.LazyListRecord>(item!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => item.Content(currentI))), __composer: __composer, __changed: 0b_01_00);
                            __composer.EndReplaceGroup(1344787836);
                        })));
                    }

                    __composer.EndReplaceGroup(1777549368);
                })), __composer: __composer, __changed: 0b_00_00_00);
            })), __composer: __composer, __changed: 0b_00_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01;
        __composer.EndRestartGroup(1400034430, __isRestarted)?.UpdateScope(() => __LazyColumn(__content, __state, __scrollStrength, __horizontalAlignment, __verticalArrangement, __modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __LazyRow(Action<ILazyListScope> content, LazyListState? state = null, float scrollStrength = 1f, Arrangement.Horizontal? horizontalArrangement = null, Alignment.Vertical? verticalAlignment = null, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __state, __scrollStrength, __horizontalArrangement, __verticalAlignment, __modifier) = (content, state, scrollStrength, horizontalArrangement, verticalAlignment, modifier);
        var __isCreated = __composer.StartRestartGroup(646078396);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_00_00_10 : 0b_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(state) ? 0b_00_00_00_00_10_00 : 0b_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(scrollStrength) ? 0b_00_00_00_10_00_00 : 0b_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(horizontalArrangement) ? 0b_00_00_10_00_00_00 : 0b_00_00_01_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(verticalAlignment) ? 0b_00_10_00_00_00_00 : 0b_00_01_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00_00 : 0b_01_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01)
        {
            var resolvedState = state ?? (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.LazyListState>() : __composer.UpdateRememberedValue<global::UnityCompose.LazyListState>(new LazyListState(0f)));
            var scope = (!__composer.Changed<global::UnityCompose.LazyListState>(resolvedState!) ? __composer.RememberedValue<global::UnityCompose.LazyListScopeImpl>() : __composer.UpdateRememberedValue<global::UnityCompose.LazyListScopeImpl>(new LazyListScopeImpl(resolvedState)));
            __ReusableComposeView<LazyRow>(initializer: (!__composer.Changed() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LazyRow>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LazyRow>>(it =>
            {
                it.style.flexDirection = FlexDirection.Column;
                it.style.alignItems = Align.FlexStart;
                it.style.justifyContent = Justify.FlexStart;
            })), modifier: modifier.OrEmpty().Clip().OnGloballyPositioned((!__composer.Changed<global::UnityCompose.LazyListState>(resolvedState!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => resolvedState.ViewportSize = it.Width))).OnVerticalScroll(onVerticalScroll: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_11_00_00) == 0b_00_00_00_10_00_00).Changed<global::UnityCompose.LazyListState>(resolvedState!).Get() ? __composer.RememberedValue<global::System.Action<float>>() : __composer.UpdateRememberedValue<global::System.Action<float>>(it => resolvedState.AnimateScrollBy(scrollStrength * DefaultScrollMultiplier * it)))), content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_00_11) == 0b_00_00_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_11_00_00_00) == 0b_00_00_10_00_00_00).ChangedAsFlag((__dirty & 0b_00_11_00_00_00_00) == 0b_00_10_00_00_00_00).Changed<global::UnityCompose.LazyListState>(resolvedState!).Changed<global::UnityCompose.LazyListScopeImpl>(scope!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __ReusableComposeView<LazyRowContent>(initializer: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11_00_00_00) == 0b_00_00_10_00_00_00).ChangedAsFlag((__dirty & 0b_00_11_00_00_00_00) == 0b_00_10_00_00_00_00).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LazyRowContent>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LazyRowContent>>(it =>
                {
                    it.style.flexDirection = FlexDirection.Row;
                    it.style.alignItems = (verticalAlignment ?? Alignment.Top).ToAlign();
                    it.style.justifyContent = (horizontalArrangement ?? Arrangement.Left).ToJustify();
                })), modifier: Modifier.Offset(x: -resolvedState.Value.Dp()).OnGloballyPositioned((!__composer.Changed<global::UnityCompose.LazyListState>(resolvedState!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => resolvedState.ContentSize = it.Width))), content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_00_11) == 0b_00_00_00_00_00_10).Changed<global::UnityCompose.LazyListState>(resolvedState!).Changed<global::UnityCompose.LazyListScopeImpl>(scope!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __SideEffect((scope, content), (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_00_11) == 0b_00_00_00_00_00_10).Changed<global::UnityCompose.LazyListScopeImpl>(scope!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                    {
                        scope.Clear();
                        content(scope);
                    })), __composer: __composer, __changed: 0b_00_00);
                    var items = resolvedState.Items;
                    __composer.StartReplaceGroup(1194866133);
                    for (var i = 0; i < items.Count; i++)
                    {
                        var currentI = i;
                        var item = items[i];
                        Key(key: item.Key, content: (!__composer.BuildChanged().Changed<global::UnityCompose.LazyListState>(resolvedState!).Changed<int>(currentI!).Changed<global::UnityCompose.LazyListRecord>(item!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __composer.StartReplaceGroup(1833515950);
                            __ReusableComposeView<LazyListItem>(modifier: Modifier.OnLocallyPositioned((!__composer.BuildChanged().Changed<global::UnityCompose.LazyListState>(resolvedState!).Changed<int>(currentI!).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => resolvedState.SyncOffset(currentI, it.LocalLeft)))), content: (!__composer.BuildChanged().Changed<int>(currentI!).Changed<global::UnityCompose.LazyListRecord>(item!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => item.Content(currentI))), __composer: __composer, __changed: 0b_01_00);
                            __composer.EndReplaceGroup(1833515950);
                        })));
                    }

                    __composer.EndReplaceGroup(1194866133);
                })), __composer: __composer, __changed: 0b_00_00_00);
            })), __composer: __composer, __changed: 0b_00_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01;
        __composer.EndRestartGroup(646078396, __isRestarted)?.UpdateScope(() => __LazyRow(__content, __state, __scrollStrength, __horizontalArrangement, __verticalAlignment, __modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}