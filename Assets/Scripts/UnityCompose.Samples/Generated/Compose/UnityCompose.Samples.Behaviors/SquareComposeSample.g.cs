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
            __composer.StartRestartGroup(969856316);
            if (__composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(969856316)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-284742440);
            if (__composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-284742440)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(814623295);
            if (__composer.ShouldExecute())
            {
                var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.Changed(isSwitched) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Spacer(Modifier.Size(100).Background(Color.red).Border(16).OnClick(!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value)).Scale(AnimateFloatAsState(isSwitched.Value ? 1.5f : 1f).Value));
                // if (isSwitched.Value)
                //     EmptySpacer();
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(814623295)?.UpdateScope(() => __Layout());
        }

        [Composable]
        private static void __EmptySpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-469596372);
            if (__composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-469596372)?.UpdateScope(() => __EmptySpacer());
        }
    }
}