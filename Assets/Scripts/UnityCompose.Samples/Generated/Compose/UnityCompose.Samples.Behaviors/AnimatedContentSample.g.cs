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
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<float>(-670752465, Duration) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-content-sample"), content: !__composer.RememberedKeyChanged<float>(-304427265, Duration) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = !__composer.RememberedKeyChanged<bool>(452696447, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                        AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.AnimationSpec, UnityCompose.IMutableState<bool>?>>(-282556784, (animationSpec, isSwitched)) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>(_ => isSwitched.Value ? SlideInVertically(!__composer.RememberedKeyChanged<bool>(-1479645396, true) ? CurrentComposer.RememberedValue<System.Func<float, float>>() : CurrentComposer.UpdateLambda<System.Func<float, float>>(it => -it)).TogetherWith(SlideOutVertically(!__composer.RememberedKeyChanged<bool>(1680510924, true) ? CurrentComposer.RememberedValue<System.Func<float, float>>() : CurrentComposer.UpdateLambda<System.Func<float, float>>(it => it))).With(animationSpec: animationSpec) : SlideInVertically(!__composer.RememberedKeyChanged<bool>(-89775308, true) ? CurrentComposer.RememberedValue<System.Func<float, float>>() : CurrentComposer.UpdateLambda<System.Func<float, float>>(it => it)).TogetherWith(SlideOutVertically(!__composer.RememberedKeyChanged<bool>(945136666, true) ? CurrentComposer.RememberedValue<System.Func<float, float>>() : CurrentComposer.UpdateLambda<System.Func<float, float>>(it => -it))).With(animationSpec: animationSpec)), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(AnimateColorAsState(targetValue: isSwitched.Value ? Color.green : Color.red, animationSpec: animationSpec).Value), content: !__composer.RememberedKeyChanged<bool>(1849232595, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent<string>>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent<string>>(state =>
                        {
                            Text(text: state.ToString(), color: Color.white, fontSize: 64);
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100, vertical: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-1952388886, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isSwitched.Value = !isSwitched.Value)));
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