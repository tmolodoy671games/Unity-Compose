using SharpExtensions;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositionedSample
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(1984938033, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1985038033, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }

        [Composable]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(-1799797902, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-1799697902, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Preview()));
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(1907345131, true))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.FillMaxSize().Padding(100), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-199839457, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(2099990617, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(static () => MutableStateOf(false));
                    var layout = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>(1672145474, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>(static () => MutableStateOf(Optional.Empty<Vector2>()));
                    Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>, UnityCompose.ComposableContent>(-532533588, (isSwitched, layout)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>, UnityCompose.ComposableContent>(() =>
                    {
                        var transitionSpec = Tween();
                        Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Size(40).Background(Color.blue).Offset(x: AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec).Value), content: CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(-1512483868, layout) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(() =>
                        {
                            Box(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(-431635704, layout) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(() =>
                            {
                                Box(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(472236449, layout) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(() =>
                                {
                                    Spacer(Modifier.Background(Color.green).Size(20).OnGloballyPositioned(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, System.Action<UnityCompose.LayoutCoordinates>>(-1802126943, layout) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, System.Action<UnityCompose.LayoutCoordinates>>(it => layout.Value = it.GlobalCenter)));
                                }));
                            }));
                        }));
                    }));
                    Text(modifier: Modifier.Background(Color.blue).Padding(32).Border(32).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(1652614945, isSwitched) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isSwitched.Value = !isSwitched.Value)), color: Color.white, text: "Switch");
                    if (layout.Value.HasValue)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Size(10).Background(Color.red).Float().Position(left: measurer.GlobalToLocal(layout.Value.Value).x, top: measurer.GlobalToLocal(layout.Value.Value).y));
                    }
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1907445131, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }
    }
}