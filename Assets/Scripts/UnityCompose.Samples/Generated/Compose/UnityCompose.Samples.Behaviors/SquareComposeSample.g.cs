using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1183406456);
            if (__composer.ShouldExecute(true))
            {
                Preview();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1183406456)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-2102891545);
            if (__composer.ShouldExecute(true))
            {
                var isSwitched = !__composer.RememberedKeyChanged<bool>(-894395642, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(LoggableMutableStateOf(false));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(62634758, isSwitched) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Spacer(Modifier.Size(100).Background(Color.red).Border(16).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(1202012326, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                    {
                        Debug.Log("OnClick()");
                        isSwitched.Value = !isSwitched.Value;
                    })).Scale(isSwitched.Value ? 1.5f : 1, transition: Transition()));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2102891545)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1712682151);
            if (__composer.ShouldExecute(true))
            {
                var isSwitched = !__composer.RememberedKeyChanged<bool>(751214259, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(LoggableMutableStateOf(false));
                var isHovered = !__composer.RememberedKeyChanged<bool>(237932776, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(LoggableMutableStateOf(false));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<bool>?>>(1854010567, (isSwitched, isHovered)) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    __composer.StartReplaceGroup(497652786);
                    if (isSwitched.Value)
                    {
                        Spacer(modifier: Modifier.Size(50).Background(Color.green).Border(16).Margin(top: 100));
                    }

                    __composer.EndReplaceGroup(497652786);
                    Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Background(Color.blue).Border(16).Size(100).Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())// .Scale(AnimateFloatAsState(isHovered.Value ? 1.5f : 1).Value)
                    .OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(1360769975, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isSwitched.Value = !isSwitched.Value)).OnMouseEnter(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(822677266, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(1701961213, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = false)), content: !__composer.RememberedKeyChanged<bool>(1755578601, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        Box(modifier: Modifier.Size(50).Background(Color.red).Border(16), content: !__composer.RememberedKeyChanged<bool>(-1245837134, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                        {
                            Text(text: "Text", color: Color.white);
                        }));
                    }));
                    __composer.StartReplaceGroup(1253046614);
                    if (isSwitched.Value)
                    {
                        Spacer(modifier: Modifier.Size(50).Background(Color.green).Border(16).Margin(top: 100));
                    }

                    __composer.EndReplaceGroup(1253046614);
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1712682151)?.UpdateScope(() => __Layout());
        }
    }
}