#nullable enable
using StableCollections;
using UnityEngine.SocialPlatforms;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(624803745);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(624803745, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1469040989);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1469040989, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1260028608);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.Name("composition-local-sample").FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __CompositionLocalProvider(LocalIsSwitched.Provides(isSwitched.Value), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __SampleReader(__composer: __composer, __changed: 0b_00))), __composer: __composer, __changed: 0b_00_00);
                    __Text(text: "Switch", color: Color.white, fontSize: 32, modifier: Modifier.Background(Color.blue).Padding(all: 32.Px()).Border(radius: 16.Px()).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isSwitched.Value = !isSwitched.Value))).Margin(top: 80.Px()), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1260028608, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __OtherSampleReader(bool firstValue, bool secondValue, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__firstValue, __secondValue) = (firstValue, secondValue);
            var __isCreated = __composer.StartRestartGroup(2017996857);
            var __dirty = __changed;
            var __dirtyRestart = 0;
            if ((__changed & 0b_00_11) == 0)
                __dirty |= __composer.Changed(firstValue) ? 0b_00_10 : 0b_00_01;
            else
                __dirtyRestart |= 0b_00_01;
            if ((__changed & 0b_11_00) == 0)
                __dirty |= __composer.Changed(secondValue) ? 0b_10_00 : 0b_01_00;
            else
                __dirtyRestart |= 0b_01_00;
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01_01)
            {
                Debug.Log($"{Time.frameCount}: {firstValue} vs {secondValue}");
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __dirty = 0b_01_01;
            __composer.EndRestartGroup(2017996857, __isRestarted)?.UpdateScope(() => __OtherSampleReader(__firstValue, __secondValue, __composer, __dirtyRestart));
        }

        private static void __SampleReader(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(621485519);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Box((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Spacer(modifier: Modifier.Background(LocalIsSwitched.Current ? Color.green : Color.red, transition: Transition()).Padding(all: 100.Px()), __composer: __composer, __changed: 0b_00);
                    })), __composer: __composer, __changed: 0b_01_01_00);
                })), __composer: __composer, __changed: 0b_01_01_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(621485519, __isRestarted)?.UpdateScope(() => __SampleReader(__composer, 0));
        }
    }
}