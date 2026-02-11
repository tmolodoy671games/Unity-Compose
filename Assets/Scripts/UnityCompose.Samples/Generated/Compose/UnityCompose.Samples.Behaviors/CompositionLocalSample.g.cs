#nullable enable
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
        private void __Content(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(624803745);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(624803745, __isRestarted)?.UpdateScope(() => __Content());
        }

        private void __Content()
        {
            __Content(CurrentComposer);
        }

        private void __Preview(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-1469040989);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1469040989, __isRestarted)?.UpdateScope(() => __Preview());
        }

        private void __Preview()
        {
            __Preview(CurrentComposer);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-1260028608);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Box(alignment: Alignment.Center, modifier: Modifier.Name("composition-local-sample").FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    CompositionLocalProvider(LocalIsSwitched.Provides(isSwitched.Value), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => SampleReader())));
                    Text(text: "Switch", color: Color.white, fontSize: 32, modifier: Modifier.Background(Color.blue).Padding(all: 32.Px()).Border(radius: 16.Px()).OnClick((!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value))).Margin(top: 80.Px()));
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1260028608, __isRestarted)?.UpdateScope(() => __Layout());
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer);
        }

        private static void __OtherSampleReader(bool firstValue, bool secondValue, global::UnityCompose.Composer __composer = null !)
        {
            var(__firstValue, __secondValue) = (firstValue, secondValue);
            __composer.StartRestartGroup(-2017996857);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecuteAsStruct((__firstValue, __secondValue)))
            {
                Debug.Log($"{Time.frameCount}: {firstValue} vs {secondValue}");
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2017996857, __isRestarted)?.UpdateScope(() => __OtherSampleReader(__firstValue, __secondValue));
        }

        private static void __OtherSampleReader(bool firstValue, bool secondValue)
        {
            __OtherSampleReader(firstValue, secondValue, CurrentComposer);
        }

        private static void __SampleReader(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(621485519);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Box((!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Box((!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        Spacer(modifier: Modifier.Background(LocalIsSwitched.Current ? Color.green : Color.red, transition: Transition()).Padding(all: 100.Px()));
                    })));
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(621485519, __isRestarted)?.UpdateScope(() => __SampleReader());
        }

        private static void __SampleReader()
        {
            __SampleReader(CurrentComposer);
        }
    }
}