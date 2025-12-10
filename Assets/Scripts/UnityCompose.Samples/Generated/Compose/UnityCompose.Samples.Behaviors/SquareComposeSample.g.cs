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
                var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(LoggableMutableStateOf(false));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.Changed(isSwitched) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    isSwitched.Value.ToString();
                    Spacer(Modifier.Size(100).Background(Color.red).Border(16).OnClick(!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value)).Scale(1, transition: Transition()));
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
                var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(LoggableMutableStateOf(false));
                var isHovered = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(LoggableMutableStateOf(false));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.Changed((isSwitched, isHovered)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    __composer.StartReplaceGroup(1471159646);
                    if (isSwitched.Value)
                    {
                        Spacer(modifier: Modifier.Size(50).Background(Color.green).Border(16).Margin(top: 100));
                    }

                    __composer.EndReplaceGroup(1471159646);
                    Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Background(Color.blue).Border(16).Size(100).Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())// .Scale(AnimateFloatAsState(isHovered.Value ? 1.5f : 1).Value)
                    .OnClick(!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value)).OnMouseEnter(!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = false)), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        Box(modifier: Modifier.Size(50).Background(Color.red).Border(16), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                        {
                            Text(text: "Text", color: Color.white);
                        }));
                    }));
                    __composer.StartReplaceGroup(159441168);
                    if (isSwitched.Value)
                    {
                        Spacer(modifier: Modifier.Size(50).Background(Color.green).Border(16).Margin(top: 100));
                    }

                    __composer.EndReplaceGroup(159441168);
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