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
        private void __Content(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-2015973743);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2015973743, __isRestarted)?.UpdateScope(() => __Content());
        }

        private void __Content()
        {
            __Content(CurrentComposer);
        }

        private void __Preview(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-816713456);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-816713456, __isRestarted)?.UpdateScope(() => __Preview());
        }

        private void __Preview()
        {
            __Preview(CurrentComposer);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(1317664372);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var layoutCoordinates = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(MutableStateOf(Optional.Empty<LayoutCoordinates>())));
                Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize().OnGloballyPositioned((!__composer.Changed(layoutCoordinates) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => layoutCoordinates.Value = it))), content: (!__composer.Changed(layoutCoordinates) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var positions = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>(MutableStateDictionaryOf<int, Vector2>()));
                    Row((!__composer.Changed(positions) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var selectionIndex = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0)));
                        Tab(selected: selectionIndex.Value == 0, modifier: Modifier.OnClick((!__composer.Changed(selectionIndex) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => selectionIndex.Value = 0))).OnGloballyPositioned((!__composer.Changed(positions) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[0] = it.GlobalPosition))), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Text(text: "First"))));
                        Tab(selected: selectionIndex.Value == 1, modifier: Modifier.OnClick((!__composer.Changed(selectionIndex) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => selectionIndex.Value = 1))).OnGloballyPositioned((!__composer.Changed(positions) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[1] = it.GlobalPosition))), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Text(text: "Second"))));
                        Tab(selected: selectionIndex.Value == 2, modifier: Modifier.OnClick((!__composer.Changed(selectionIndex) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => selectionIndex.Value = 2))).OnGloballyPositioned((!__composer.Changed(positions) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[2] = it.GlobalPosition))), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Text(text: "Third"))));
                        Tab(selected: selectionIndex.Value == 3, modifier: Modifier.OnClick((!__composer.Changed(selectionIndex) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => selectionIndex.Value = 3))).OnGloballyPositioned((!__composer.Changed(positions) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[3] = it.GlobalPosition))), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Text(text: "Fourth"))));
                    })));
                    if (!layoutCoordinates.Value.HasValue)
                        return;
                    __composer.StartReplaceGroup(1630269357);
                    foreach (var position in positions.Values)
                    {
                        var coordinates = layoutCoordinates.Value.Value;
                        Spacer(modifier: Modifier.Background(Color.red).Size(16.Px()).Border(4.Px(), topLeftRadius: 0.Px()).Float().Position(left: coordinates.GlobalToLocal(position).x.Px(), top: coordinates.GlobalToLocal(position).y.Px()));
                    }

                    __composer.EndReplaceGroup(1630269357);
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1317664372, __isRestarted)?.UpdateScope(() => __Layout());
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer);
        }

        private static void __Tab(bool selected, ComposableContent content, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !)
        {
            var(__selected, __content, __modifier) = (selected, content, modifier);
            __composer.StartRestartGroup(-1999327903);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecuteAsStruct((__selected, __content, __modifier)))
            {
                var animationSpec = Tween();
                Box(modifier: modifier.OrEmpty().Background(Color.grey).Padding(vertical: 8.Px(), horizontal: AnimateFloatAsState(selected ? 160 : 20, animationSpec: animationSpec).Value.Px()).Margin(horizontal: 2.Px()).Border(16.Px(), topLeftRadius: 0.Px()).Scale(AnimateFloatAsState(selected ? 0.8f : 1).Value), content: (!__composer.Changed(content) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    CompositionLocalProvider(LocalContentColor.Provides(Color.white), LocalTextStyle.Provides(new TextStyle(Color: Color.white, FontSize: 32)), content: content);
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1999327903, __isRestarted)?.UpdateScope(() => __Tab(__selected, __content, __modifier));
        }

        private static void __Tab(bool selected, ComposableContent content, IModifier? modifier = null)
        {
            __Tab(selected, content, modifier, CurrentComposer);
        }
    }
}