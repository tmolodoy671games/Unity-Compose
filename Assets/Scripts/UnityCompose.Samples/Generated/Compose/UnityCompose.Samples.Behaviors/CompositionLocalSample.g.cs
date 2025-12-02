using StableCollections;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1449131958);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1449131958)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1367538738);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1367538738)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-597693002);
            if (__composer.ShouldExecute(true))
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("composition-local-sample").FillMaxSize(), content: !__composer.RememberedKeyChanged<bool>(-1301388334, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = !__composer.RememberedKeyChanged<bool>(812200372, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    CompositionLocalProvider(LocalIsSwitched.Provides(isSwitched.Value), content: SampleReader);
                    Text(text: "Switch", color: Color.white, fontSize: 32, modifier: Modifier.Background(Color.blue).Padding(all: 32).Border(radius: 16).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-900605980, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isSwitched.Value = !isSwitched.Value)).Margin(top: 80));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-597693002)?.UpdateScope(() => __Layout());
        }

        [Composable]
        private static void __SampleReader()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-943066722);
            if (__composer.ShouldExecute(true))
            {
                Box(!__composer.RememberedKeyChanged<bool>(1151300559, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Box(!__composer.RememberedKeyChanged<bool>(1018330227, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        Spacer(modifier: Modifier.Background(LocalIsSwitched.Current ? Color.green : Color.red, transition: Transition()).Padding(all: 100));
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-943066722)?.UpdateScope(() => __SampleReader());
        }
    }
}