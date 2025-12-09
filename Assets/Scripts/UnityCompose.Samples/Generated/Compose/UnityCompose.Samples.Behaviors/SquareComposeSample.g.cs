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
                    isSwitched.Value.ToString();
                    Spacer(Modifier.Size(100).Background(Color.red).Border(16).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-187125448, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isSwitched.Value = !isSwitched.Value)).Scale(1, transition: Transition()));
                // Spacer(
                //     Modifier
                //         .Size(100)
                //         .Background(Color.green)
                //         .Border(16)
                //         .OnClick(() => isSwitched.Value = !isSwitched.Value)
                //         .Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())
                // );
                // Spacer(
                //     Modifier
                //         .Size(100)
                //         .Background(Color.blue)
                //         .Border(16)
                //         .OnClick(() => isSwitched.Value = !isSwitched.Value)
                //         .Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())
                // );
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
            __composer.StartRestartGroup(1601086115);
            if (__composer.ShouldExecute(true))
            {
                var isSwitched = !__composer.RememberedKeyChanged<bool>(-479376136, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(LoggableMutableStateOf(false));
                var isHovered = !__composer.RememberedKeyChanged<bool>(1766611630, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(LoggableMutableStateOf(false));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<bool>?>>(-1763156985, (isSwitched, isHovered)) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    __composer.StartReplaceGroup(497652786);
                    if (isSwitched.Value)
                    {
                        Spacer(modifier: Modifier.Size(50).Background(Color.green).Border(16).Margin(top: 100));
                    }

                    __composer.EndReplaceGroup(497652786);
                    Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Background(Color.blue).Border(16).Size(100).Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())// .Scale(AnimateFloatAsState(isHovered.Value ? 1.5f : 1).Value)
                    .OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-1567267116, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isSwitched.Value = !isSwitched.Value)).OnMouseEnter(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(996334958, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-1404477844, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = false)), content: !__composer.RememberedKeyChanged<bool>(1996493258, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        Box(modifier: Modifier.Size(50).Background(Color.red).Border(16), content: !__composer.RememberedKeyChanged<bool>(417618376, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
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

            __composer.EndRestartGroup(1601086115)?.UpdateScope(() => __Layout());
        }
    }
}