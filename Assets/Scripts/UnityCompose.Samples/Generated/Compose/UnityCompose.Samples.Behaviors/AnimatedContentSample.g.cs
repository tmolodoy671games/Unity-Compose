using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedContentSample
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-2064172763);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2064172763)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1747397018);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1747397018)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(219010038);
            if (__composer.ShouldExecute(true))
            {
                const float Duration = 0.5f;
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.Changed(Duration) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-content-sample"), content: !__composer.Changed(Duration) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                        AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: !__composer.Changed((animationSpec, isSwitched)) ? __composer.RememberedValue<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>(_ => isSwitched.Value ? SlideInVertically(it => -it).TogetherWith(SlideOutVertically(it => it)).With(animationSpec: animationSpec) : SlideInVertically(it => it).TogetherWith(SlideOutVertically(it => -it)).With(animationSpec: animationSpec)), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(AnimateColorAsState(targetValue: isSwitched.Value ? Color.green : Color.red, animationSpec: animationSpec).Value), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent<string>>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent<string>>(state =>
                        {
                            Text(text: state.ToString(), color: Color.white, fontSize: 64);
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100, vertical: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value)));
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(219010038)?.UpdateScope(() => __Layout());
        }
    }
}