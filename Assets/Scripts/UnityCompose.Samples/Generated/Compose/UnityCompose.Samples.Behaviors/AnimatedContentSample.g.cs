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
            __composer.StartRestartGroup(-1836938474);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1836938474)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(383655333);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(383655333)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(896715348);
            if (__composer.ShouldExecute(true))
            {
                const float Duration = 0.5f;
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<float>(166700196, Duration) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-content-sample"), content: !__composer.RememberedKeyChanged<float>(304511829, Duration) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = !__composer.RememberedKeyChanged<bool>(-1013642392, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                        AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.AnimationSpec, UnityCompose.IMutableState<bool>?>>(-859063702, (animationSpec, isSwitched)) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>(_ => isSwitched.Value ? SlideInVertically(!__composer.RememberedKeyChanged<bool>(1297470497, true) ? CurrentComposer.RememberedValue<System.Func<float, float>>() : CurrentComposer.UpdateLambda<System.Func<float, float>>(it => -it)).TogetherWith(SlideOutVertically(!__composer.RememberedKeyChanged<bool>(876056421, true) ? CurrentComposer.RememberedValue<System.Func<float, float>>() : CurrentComposer.UpdateLambda<System.Func<float, float>>(it => it))).With(animationSpec: animationSpec) : SlideInVertically(!__composer.RememberedKeyChanged<bool>(-1772332841, true) ? CurrentComposer.RememberedValue<System.Func<float, float>>() : CurrentComposer.UpdateLambda<System.Func<float, float>>(it => it)).TogetherWith(SlideOutVertically(!__composer.RememberedKeyChanged<bool>(1392559502, true) ? CurrentComposer.RememberedValue<System.Func<float, float>>() : CurrentComposer.UpdateLambda<System.Func<float, float>>(it => -it))).With(animationSpec: animationSpec)), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(AnimateColorAsState(targetValue: isSwitched.Value ? Color.green : Color.red, animationSpec: animationSpec).Value), content: !__composer.RememberedKeyChanged<bool>(2048422367, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent<string>>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent<string>>(state =>
                        {
                            Text(text: state.ToString(), color: Color.white, fontSize: 64);
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100, vertical: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-1754583917, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isSwitched.Value = !isSwitched.Value)));
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(896715348)?.UpdateScope(() => __Layout());
        }
    }
}