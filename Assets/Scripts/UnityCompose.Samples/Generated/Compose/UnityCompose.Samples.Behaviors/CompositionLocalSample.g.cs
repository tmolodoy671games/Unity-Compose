using StableCollections;
using UnityEngine.SocialPlatforms;
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
            __composer.StartRestartGroup(-1377754495);
            if (__composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1377754495)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-768442416);
            if (__composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-768442416)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1469247124);
            if (__composer.ShouldExecute())
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("composition-local-sample").FillMaxSize(), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    CompositionLocalProvider(LocalIsSwitched.Provides(isSwitched.Value), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => SampleReader()));
                    Text(text: "Switch", color: Color.white, fontSize: 32, modifier: Modifier.Background(Color.blue).Padding(all: 32).Border(radius: 16).OnClick(!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value)).Margin(top: 80));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1469247124)?.UpdateScope(() => __Layout());
        }

        [Composable]
        private static void __OtherSampleReader(bool firstValue, bool secondValue)
        {
            var(__firstValue, __secondValue) = (firstValue, secondValue);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-341035652);
            if (__composer.ShouldExecuteAsStruct((__firstValue, __secondValue)))
            {
                Debug.Log($"{Time.frameCount}: {firstValue} vs {secondValue}");
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-341035652)?.UpdateScope(() => __OtherSampleReader(__firstValue, __secondValue));
        }

        [Composable]
        private static void __SampleReader()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(16558468);
            if (__composer.ShouldExecute())
            {
                Box(!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Box(!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        Spacer(modifier: Modifier.Background(LocalIsSwitched.Current ? Color.green : Color.red, transition: Transition()).Padding(all: 100));
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(16558468)?.UpdateScope(() => __SampleReader());
        }
    }
}