#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedContentSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1205897630);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1205897630, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(265478403);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(265478403, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(443709627);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                const float Duration = 0.5f;
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("animated-content-sample"), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                        __AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: (!__composer.BuildChanged().Changed<global::UnityCompose.AnimationSpec>(animationSpec!).Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!).Get() ? __composer.RememberedValue<global::System.Func<global::UnityCompose.IAnimatedContentTransitionScope<string>, global::UnityCompose.ContentTransform>>() : __composer.UpdateRememberedValue<global::System.Func<global::UnityCompose.IAnimatedContentTransitionScope<string>, global::UnityCompose.ContentTransform>>(_ => isSwitched.Value ? SlideInVertically(it => -it).TogetherWith(SlideOutVertically(it => it)).With(animationSpec: animationSpec) : SlideInVertically(it => it).TogetherWith(SlideOutVertically(it => -it)).With(animationSpec: animationSpec))), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(__AnimateColorAsState(targetValue: isSwitched.Value ? Color.green : Color.red, animationSpec: animationSpec, __composer: __composer, __changed: 0b_00_00).Value), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent<string, global::UnityCompose.IModifier>>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent<string, global::UnityCompose.IModifier>>((state, modifier) =>
                        {
                            __Text(text: state.ToString(), color: Color.white, fontSize: 64, modifier: modifier, __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                        })), __composer: __composer, __changed: 0b_00_00_00_00_00);
                        __Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100.Px(), vertical: 32.Px()).Background(Color.blue).Margin(top: 16.Px()).Border(radius: 16.Px()).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isSwitched.Value = !isSwitched.Value))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                    })), __composer: __composer, __changed: 0b_01_00_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(443709627, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}