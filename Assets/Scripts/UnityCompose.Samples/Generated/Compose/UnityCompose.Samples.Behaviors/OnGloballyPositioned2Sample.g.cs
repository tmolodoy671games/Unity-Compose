using System;
using SharpExtensions;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositioned2Sample
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(397285478);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(397285478, __isRestarted)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-383066578);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-383066578, __isRestarted)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1280684234);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var layoutCoordinates = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(MutableStateOf(Optional.Empty<LayoutCoordinates>()));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().OnGloballyPositioned(!__composer.Changed(layoutCoordinates) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => layoutCoordinates.Value = it)), content: !__composer.Changed(layoutCoordinates) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var positions = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>(MutableStateDictionaryOf<int, Vector2>());
                    Row(!__composer.Changed(positions) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var selectionIndex = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                        Tab(selected: selectionIndex.Value == 0, modifier: Modifier.OnClick(!__composer.Changed(selectionIndex) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => selectionIndex.Value = 0)).OnGloballyPositioned(!__composer.Changed(positions) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[0] = it.GlobalPosition)), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Text(text: "First")));
                        Tab(selected: selectionIndex.Value == 1, modifier: Modifier.OnClick(!__composer.Changed(selectionIndex) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => selectionIndex.Value = 1)).OnGloballyPositioned(!__composer.Changed(positions) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[1] = it.GlobalPosition)), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Text(text: "Second")));
                        Tab(selected: selectionIndex.Value == 2, modifier: Modifier.OnClick(!__composer.Changed(selectionIndex) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => selectionIndex.Value = 2)).OnGloballyPositioned(!__composer.Changed(positions) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[2] = it.GlobalPosition)), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Text(text: "Third")));
                        Tab(selected: selectionIndex.Value == 3, modifier: Modifier.OnClick(!__composer.Changed(selectionIndex) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => selectionIndex.Value = 3)).OnGloballyPositioned(!__composer.Changed(positions) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[3] = it.GlobalPosition)), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Text(text: "Fourth")));
                    }));
                    if (!layoutCoordinates.Value.HasValue)
                        return;
                    __composer.StartReplaceGroup(-1331057970);
                    foreach (var position in positions.Values)
                    {
                        var coordinates = layoutCoordinates.Value.Value;
                        Spacer(modifier: Modifier.Background(Color.red).Size(16).Border(4, topLeftRadius: 0).Float().Position(left: coordinates.GlobalToLocal(position).x, top: coordinates.GlobalToLocal(position).y));
                    }

                    __composer.EndReplaceGroup(-1331057970);
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1280684234, __isRestarted)?.UpdateScope(() => __Layout());
        }

        [Composable]
        private static void __Tab(bool selected, ComposableContent content, IModifier? modifier = null)
        {
            var(__selected, __content, __modifier) = (selected, content, modifier);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1662233891);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecuteAsStruct((__selected, __content, __modifier)))
            {
                Box(modifier: modifier.OrEmpty().Background(Color.grey).Padding(vertical: 8, horizontal: AnimateFloatAsState(selected ? 160 : 20).Value).Margin(horizontal: 2).Border(16, topLeftRadius: 0).Scale(AnimateFloatAsState(selected ? 0.8f : 1).Value), content: !__composer.Changed(content) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    CompositionLocalProvider(LocalContentColor.Provides(Color.white), LocalTextStyle.Provides(new TextStyle(Color: Color.white, FontSize: 32)), content: content);
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1662233891, __isRestarted)?.UpdateScope(() => __Tab(__selected, __content, __modifier));
        }
    }
}