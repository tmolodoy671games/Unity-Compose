#nullable enable
// ReSharper disable ArrangeNamespaceBody

using UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo
{
    internal partial class NavigationSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1866917304);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1866917304, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(972566156);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(972566156, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(513098348);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var animationSpec = Tween(duration: 1f);
                __Box(modifier: Modifier.FillMaxSize(), content: (!__composer.Changed<global::UnityCompose.AnimationSpec>(animationSpec!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Box(modifier: Modifier.FillMaxSize(), content: (!__composer.Changed<global::UnityCompose.AnimationSpec>(animationSpec!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        var resumedScreen = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.Screens.ResumedScreen>() : __composer.UpdateRememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.Screens.ResumedScreen>(new ResumedScreen()));
                        var pausedScreen = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.Screens.PausedScreen>() : __composer.UpdateRememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.Screens.PausedScreen>(new PausedScreen()));
                        var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                        // AnimatedContent(
                        //     transitionSpec: _ => IEnterTransition.Empty().TogetherWith(Hide()),
                        //     // transitionSpec: it => FadeIn().TogetherWith(FadeOut()).With(animationSpec),
                        //     targetState: isSwitched.Value,
                        //     modifier: Modifier.FillMaxSize(),
                        //     content: (it, m) =>
                        //     {
                        //         ComposeScreen screen = it ? pausedScreen : resumedScreen;
                        //         screen.Content(m.OnClick(() => isSwitched.Value = !isSwitched.Value));
                        //     }
                        // );
                        __Navigation(coordinator: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.SampleCoordinatorImpl>() : __composer.UpdateRememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.SampleCoordinatorImpl>(new SampleCoordinatorImpl())), transition: (!__composer.Changed<global::UnityCompose.AnimationSpec>(animationSpec!) ? __composer.RememberedValue<global::UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<global::UnityCompose.ContentTransform>(FadeIn().TogetherWith(FadeOut()).With(animationSpec))), modifier: Modifier.FillMaxSize(), __composer: __composer, __changed: 0b_00_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_01_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(513098348, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}