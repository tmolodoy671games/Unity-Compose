using System;
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
            __composer.StartRestartGroup(1825056684);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1825056684)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-294336196);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-294336196)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(946367469);
            if (__composer.ShouldExecute(true))
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<bool>(-81028971, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    var positions = !__composer.RememberedKeyChanged<bool>(833145096, true) ? __composer.RememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>(MutableStateDictionaryOf<int, Vector2>());
                    Row(!__composer.RememberedKeyChanged<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?>(1019703683, positions) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        var selectionIndex = !__composer.RememberedKeyChanged<bool>(2094280871, true) ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                        Tab(selected: selectionIndex.Value == 0, modifier: Modifier.OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<int>?>(-878499615, selectionIndex) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => selectionIndex.Value = 0)).OnGloballyPositioned(!__composer.RememberedKeyChanged<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?>(1317878297, positions) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[0] = it.GlobalPosition)), content: !__composer.RememberedKeyChanged<bool>(-227576277, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() => Text(text: "First")));
                        Tab(selected: selectionIndex.Value == 1, modifier: Modifier.OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<int>?>(333944938, selectionIndex) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => selectionIndex.Value = 1)).OnGloballyPositioned(!__composer.RememberedKeyChanged<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?>(2103622051, positions) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[1] = it.GlobalPosition)), content: !__composer.RememberedKeyChanged<bool>(493853157, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() => Text(text: "Second")));
                        Tab(selected: selectionIndex.Value == 2, modifier: Modifier.OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<int>?>(751459661, selectionIndex) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => selectionIndex.Value = 2)).OnGloballyPositioned(!__composer.RememberedKeyChanged<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?>(764132810, positions) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[2] = it.GlobalPosition)), content: !__composer.RememberedKeyChanged<bool>(-1856590256, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() => Text(text: "Third")));
                        Tab(selected: selectionIndex.Value == 3, modifier: Modifier.OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<int>?>(1302562891, selectionIndex) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => selectionIndex.Value = 3)).OnGloballyPositioned(!__composer.RememberedKeyChanged<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?>(-1617177316, positions) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.LayoutCoordinates>>(it => positions[3] = it.GlobalPosition)), content: !__composer.RememberedKeyChanged<bool>(876275273, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() => Text(text: "Fourth")));
                    }));
                    __composer.StartReplaceGroup(-1681198346);
                    foreach (var position in positions.Values)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Background(Color.red).Size(16).Border(4, topLeftRadius: 0).Float().Position(left: measurer.GlobalToLocal(position).x, top: measurer.GlobalToLocal(position).y));
                    }

                    __composer.EndReplaceGroup(-1681198346);
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(946367469)?.UpdateScope(() => __Layout());
        }

        [Composable]
        private static void __Tab(bool selected, ComposableContent content, IModifier? modifier = null)
        {
            var(__selected, __content, __modifier) = (selected, content, modifier);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-977926424);
            if (__composer.ShouldExecute((__selected, __content, __modifier)))
            {
                Box(modifier: modifier.OrEmpty().Background(Color.grey).Padding(vertical: 8, horizontal: AnimateFloatAsState(selected ? 160 : 20).Value).Margin(horizontal: 2).Border(16, topLeftRadius: 0).Scale(AnimateFloatAsState(selected ? 0.8f : 1).Value), content: !__composer.RememberedKeyChanged<UnityCompose.ComposableContent>(863819400, content) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    CompositionLocalProvider(LocalContentColor.Provides(Color.white), LocalTextStyle.Provides(new TextStyle(Color: Color.white, FontSize: 32)), content: content);
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-977926424)?.UpdateScope(() => __Tab(__selected, __content, __modifier));
        }
    }
}