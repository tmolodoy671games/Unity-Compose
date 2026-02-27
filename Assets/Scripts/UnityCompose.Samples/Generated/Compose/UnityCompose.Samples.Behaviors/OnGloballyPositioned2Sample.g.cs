#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using SharpExtensions;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositioned2Sample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(2015973743);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(2015973743, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(816713456);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(816713456, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1317664372);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var layoutCoordinates = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityCompose.LayoutCoordinates>>>(MutableStateOf(Optional.Empty<LayoutCoordinates>())));
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize().OnGloballyPositioned((!__composer.Changed<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityCompose.LayoutCoordinates>>>(layoutCoordinates!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => layoutCoordinates.Value = it))), content: (!__composer.Changed<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityCompose.LayoutCoordinates>>>(layoutCoordinates!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var positions = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableStateDictionary<int, global::UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableStateDictionary<int, global::UnityEngine.Vector2>>(MutableStateDictionaryOf<int, Vector2>()));
                    __Row((!__composer.Changed<global::UnityCompose.IMutableStateDictionary<int, global::UnityEngine.Vector2>>(positions!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        var selectionIndex = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<int>>(MutableStateOf(0)));
                        __Tab(selected: selectionIndex.Value == 0, modifier: Modifier.OnClick((!__composer.Changed<global::UnityCompose.IMutableState<int>>(selectionIndex!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => selectionIndex.Value = 0))).OnGloballyPositioned((!__composer.Changed<global::UnityCompose.IMutableStateDictionary<int, global::UnityEngine.Vector2>>(positions!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => positions[0] = it.GlobalPosition))), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __Text(text: "First", __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_01))), __composer: __composer, __changed: 0b_00_00_00);
                        __Tab(selected: selectionIndex.Value == 1, modifier: Modifier.OnClick((!__composer.Changed<global::UnityCompose.IMutableState<int>>(selectionIndex!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => selectionIndex.Value = 1))).OnGloballyPositioned((!__composer.Changed<global::UnityCompose.IMutableStateDictionary<int, global::UnityEngine.Vector2>>(positions!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => positions[1] = it.GlobalPosition))), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __Text(text: "Second", __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_01))), __composer: __composer, __changed: 0b_00_00_00);
                        __Tab(selected: selectionIndex.Value == 2, modifier: Modifier.OnClick((!__composer.Changed<global::UnityCompose.IMutableState<int>>(selectionIndex!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => selectionIndex.Value = 2))).OnGloballyPositioned((!__composer.Changed<global::UnityCompose.IMutableStateDictionary<int, global::UnityEngine.Vector2>>(positions!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => positions[2] = it.GlobalPosition))), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __Text(text: "Third", __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_01))), __composer: __composer, __changed: 0b_00_00_00);
                        __Tab(selected: selectionIndex.Value == 3, modifier: Modifier.OnClick((!__composer.Changed<global::UnityCompose.IMutableState<int>>(selectionIndex!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => selectionIndex.Value = 3))).OnGloballyPositioned((!__composer.Changed<global::UnityCompose.IMutableStateDictionary<int, global::UnityEngine.Vector2>>(positions!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => positions[3] = it.GlobalPosition))), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __Text(text: "Fourth", __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_01))), __composer: __composer, __changed: 0b_00_00_00);
                    })), __composer: __composer, __changed: 0b_01_01_01_00);
                    if (!layoutCoordinates.Value.HasValue)
                        return;
                    __composer.StartReplaceGroup(1630269357);
                    foreach (var position in positions.Values)
                    {
                        var coordinates = layoutCoordinates.Value.Value;
                        __Spacer(modifier: Modifier.Background(Color.red).Size(16.Px()).Border(4.Px(), topLeftRadius: 0.Px()).Float().Position(left: coordinates.GlobalToLocal(position).x.Px(), top: coordinates.GlobalToLocal(position).y.Px()), __composer: __composer, __changed: 0b_00);
                    }

                    __composer.EndReplaceGroup(1630269357);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1317664372, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Tab(bool selected, ComposableContent content, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__selected, __content, __modifier) = (selected, content, modifier);
            var __isCreated = __composer.StartRestartGroup(1999327903);
            var __dirty = __changed;
            if ((__changed & 0b_00_00_11) == 0)
                __dirty |= __composer.Changed(selected) ? 0b_00_00_10 : 0b_00_00_01;
            if ((__changed & 0b_00_11_00) == 0)
                __dirty |= __composer.Changed(content) ? 0b_00_10_00 : 0b_00_01_00;
            if ((__changed & 0b_11_00_00) == 0)
                __dirty |= __composer.Changed(modifier) ? 0b_10_00_00 : 0b_01_00_00;
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
            {
                var animationSpec = Tween();
                __Box(modifier: modifier.OrEmpty().Background(Color.grey).Padding(vertical: 8.Px(), horizontal: __AnimateFloatAsState(selected ? 160 : 20, animationSpec: animationSpec, __composer: __composer, __changed: 0b_01_00_00).Value.Px()).Margin(horizontal: 2.Px()).Border(16.Px(), topLeftRadius: 0.Px()).Scale(__AnimateFloatAsState(selected ? 0.8f : 1, __composer: __composer, __changed: 0b_01_01_00).Value), content: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11_00) == 0b_00_10_00).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __CompositionLocalProvider(LocalContentColor.Provides(Color.white), LocalTextStyle.Provides(new TextStyle(Color: Color.white, FontSize: 32)), content: content, __composer: __composer, __changed: ((__dirty & 0b_00_11_00) << 2));
                })), __composer: __composer, __changed: 0b_01_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __dirty = 0b_01_01_01;
            __composer.EndRestartGroup(1999327903, __isRestarted)?.UpdateScope(() => __Tab(__selected, __content, __modifier, __composer, __composer.UpdateChangedFlags(__changed)));
        }
    }
}