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
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1984938033);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1984938033)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1799797902);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1799797902)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1907345131);
            if (__composer.ShouldExecute(true))
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.FillMaxSize().Padding(100), content: !__composer.RememberedKeyChanged<bool>(-199839457, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = !__composer.RememberedKeyChanged<bool>(2099990617, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    var layout = !__composer.RememberedKeyChanged<bool>(1672145474, true) ? __composer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>(MutableStateOf(Optional.Empty<Vector2>()));
                    Box(modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>>(-532533588, (isSwitched, layout)) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        var transitionSpec = Tween();
                        Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Size(40).Background(Color.blue).Offset(x: AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec).Value), content: !__composer.RememberedKeyChanged<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>(-1512483868, layout) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                        {
                            Box(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>(-431635704, layout) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                            {
                                Box(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>(472236449, layout) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                                {
                                    Spacer(Modifier.Background(Color.green).Size(20).OnGloballyPositioned(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>?>(-1802126943, layout) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.LayoutCoordinates>>(it => layout.Value = it.GlobalCenter)));
                                }));
                            }));
                        }));
                    }));
                    Text(modifier: Modifier.Background(Color.blue).Padding(32).Border(32).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(1652614945, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isSwitched.Value = !isSwitched.Value)), color: Color.white, text: "Switch");
                    __composer.StartReplaceGroup(1445387667);
                    if (layout.Value.HasValue)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Size(10).Background(Color.red).Float().Position(left: measurer.GlobalToLocal(layout.Value.Value).x, top: measurer.GlobalToLocal(layout.Value.Value).y));
                    }

                    __composer.EndReplaceGroup(1445387667);
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1907345131)?.UpdateScope(() => __Layout());
        }
    }
}