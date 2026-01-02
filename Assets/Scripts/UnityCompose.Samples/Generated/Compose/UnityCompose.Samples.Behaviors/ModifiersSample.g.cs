using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ModifiersSample
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1922950060);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1922950060, __isRestarted)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-91035010);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-91035010, __isRestarted)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(440175921);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("composition-local-sample").FillMaxSize(), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    WithModifiers(after: Modifier.Background(AnimateColorAsState(isSwitched.Value ? Color.green : Color.red).Value), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => SampleReader()));
                    Text(text: "Switch", color: Color.white, fontSize: 32, modifier: Modifier.Background(Color.blue).Padding(all: 32).Border(radius: 16).OnClick(!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value)).Margin(top: 80));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(440175921, __isRestarted)?.UpdateScope(() => __Layout());
        }

        [Composable]
        private static void __SampleReader()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-211793104);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Spacer(modifier: Modifier.Padding(all: 100));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-211793104, __isRestarted)?.UpdateScope(() => __SampleReader());
        }
    }
}