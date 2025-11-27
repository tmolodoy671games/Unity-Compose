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
            if (CurrentComposer.BeginComposeGroup(202235192, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(202335192, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }

        [Composable]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(694094757, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(694194757, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Preview()));
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(1747818766, true))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.FillMaxSize().Padding(100), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(1303957417, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(-904743194, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(static () => MutableStateOf(false));
                    var layout = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>(716384622, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>(static () => MutableStateOf(Optional.Empty<Vector2>()));
                    Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>, UnityCompose.ComposableContent>(-843538543, (isSwitched, layout)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>, UnityCompose.ComposableContent>(() =>
                    {
                        var transitionSpec = Tween();
                        Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Size(40).Background(Color.blue).Offset(x: AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec).Value), content: CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(693661224, layout) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(() =>
                        {
                            Box(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(2081388306, layout) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(() =>
                            {
                                Box(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(897762967, layout) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, UnityCompose.ComposableContent>(() =>
                                {
                                    Spacer(Modifier.Background(Color.green).Size(20).OnGloballyPositioned(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, System.Action<UnityCompose.LayoutCoordinates>>(-344146716, layout) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?, System.Action<UnityCompose.LayoutCoordinates>>(it => layout.Value = it.GlobalCenter)));
                                }));
                            }));
                        }));
                    }));
                    Text(modifier: Modifier.Background(Color.blue).Padding(32).Border(32).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-1723671151, isSwitched) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isSwitched.Value = !isSwitched.Value)), color: Color.white, text: "Switch");
                    if (layout.Value.HasValue)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Size(10).Background(Color.red).Float().Position(left: measurer.GlobalToLocal(layout.Value.Value).x, top: measurer.GlobalToLocal(layout.Value.Value).y));
                    }
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1747918766, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }
    }
}