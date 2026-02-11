#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedContentSample
    {
        private void __Content(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(1205897630);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1205897630, __isRestarted)?.UpdateScope(() => __Content());
        }

        private void __Content()
        {
            __Content(CurrentComposer);
        }

        private void __Preview(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-265478403);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-265478403, __isRestarted)?.UpdateScope(() => __Preview());
        }

        private void __Preview()
        {
            __Preview(CurrentComposer);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(443709627);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                const float Duration = 0.5f;
                Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.ChangedAsStruct(Duration) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("animated-content-sample"), content: (!__composer.ChangedAsStruct(Duration) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                        AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: (!__composer.ChangedAsStruct((animationSpec, isSwitched)) ? __composer.RememberedValue<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>(_ => isSwitched.Value ? SlideInVertically(it => -it).TogetherWith(SlideOutVertically(it => it)).With(animationSpec: animationSpec) : SlideInVertically(it => it).TogetherWith(SlideOutVertically(it => -it)).With(animationSpec: animationSpec))), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(AnimateColorAsState(targetValue: isSwitched.Value ? Color.green : Color.red, animationSpec: animationSpec).Value), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent<string, UnityCompose.IModifier>>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent<string, UnityCompose.IModifier>>((state, modifier) =>
                        {
                            Text(text: state.ToString(), color: Color.white, fontSize: 64, modifier: modifier);
                        })));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100.Px(), vertical: 32.Px()).Background(Color.blue).Margin(top: 16.Px()).Border(radius: 16.Px()).OnClick((!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value))));
                    })));
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(443709627, __isRestarted)?.UpdateScope(() => __Layout());
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer);
        }
    }
}