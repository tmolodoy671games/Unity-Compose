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
            if (CurrentComposer.BeginComposeGroup(-2064172763, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-2064072763, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }

        [Composable]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(-1747397018, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-1747297018, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Preview()));
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(219010038, true))
                return;
            try
            {
                const float Duration = 0.5f;
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<float, UnityCompose.ComposableContent>(-670752465, Duration) ? CurrentComposer.RememberedValue<float, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<float, UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-content-sample"), content: CurrentComposer.HasRememberedValue<float, UnityCompose.ComposableContent>(-304427265, Duration) ? CurrentComposer.RememberedValue<float, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<float, UnityCompose.ComposableContent>(() =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(452696447, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
                        AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.AnimationSpec, UnityCompose.IMutableState<bool>?>, System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>(-282556784, (animationSpec, isSwitched)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.AnimationSpec, UnityCompose.IMutableState<bool>?>, System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.AnimationSpec, UnityCompose.IMutableState<bool>?>, System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>(_ => isSwitched.Value ? SlideInVertically(it => -it).TogetherWith(SlideOutVertically(it => it)).With(animationSpec: animationSpec) : SlideInVertically(it => it).TogetherWith(SlideOutVertically(it => -it)).With(animationSpec: animationSpec)), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(AnimateColorAsState(targetValue: isSwitched.Value ? Color.green : Color.red, animationSpec: animationSpec).Value), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent<string>>(1849232595, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent<string>>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent<string>>(state =>
                        {
                            Text(text: state.ToString(), color: Color.white, fontSize: 64);
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100, vertical: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-1952388886, isSwitched) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isSwitched.Value = !isSwitched.Value)));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(219110038, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }
    }
}