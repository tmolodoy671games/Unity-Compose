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
            __composer.StartRestartGroup(1544580258);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1544580258, __isRestarted)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(104297156);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(104297156, __isRestarted)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1890842087);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var isRedSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    Spacer(Modifier.Size(100).Background(Color.red).Border(16).OnClick(!__composer.Changed(isRedSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isRedSwitched.Value = !isRedSwitched.Value)).Scale(AnimateFloatAsState(isRedSwitched.Value ? 1.5f : 1f).Value).Margin(top: 40));
                    var isGreenSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    Spacer(Modifier.Size(100).Background(Color.green).Border(16).OnClick(!__composer.Changed(isGreenSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isGreenSwitched.Value = !isGreenSwitched.Value)).Scale(AnimateFloatAsState(isGreenSwitched.Value ? 1.5f : 1f).Value).Margin(top: 40));
                    var isBlueSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    Spacer(Modifier.Size(100).Background(Color.blue).Border(16).OnClick(!__composer.Changed(isBlueSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isBlueSwitched.Value = !isBlueSwitched.Value)).Scale(AnimateFloatAsState(isBlueSwitched.Value ? 1.5f : 1f).Value).Margin(top: 40));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1890842087, __isRestarted)?.UpdateScope(() => __Layout());
        }
    }
}