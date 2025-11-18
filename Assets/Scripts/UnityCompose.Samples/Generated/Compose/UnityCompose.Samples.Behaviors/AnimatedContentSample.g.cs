using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedContentSample : ComposeUI
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }

        [Composable]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Preview());
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                const float Duration = 0.5f;
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(Duration).Remember<System.Action>(__ => () =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-content-sample"), content: CurrentComposer.WithState(Duration).Remember<System.Action>(__ => () =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IMutableState<bool>>>(__ => () => MutableStateOf(false)));
                        AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: CurrentComposer.WithState((animationSpec, isSwitched)).Remember<System.Func<UnityCompose.IAnimatedContentTransitionScope<string>, UnityCompose.ContentTransform>>(__ => _ => isSwitched.Value ? SlideInVertically(CurrentComposer.WithState(string.Empty).Remember<System.Func<float, float>>(__ => it => -it)).TogetherWith(SlideOutVertically(CurrentComposer.WithState(string.Empty).Remember<System.Func<float, float>>(__ => it => it))).With(animationSpec: animationSpec) : SlideInVertically(CurrentComposer.WithState(string.Empty).Remember<System.Func<float, float>>(__ => it => it)).TogetherWith(SlideOutVertically(CurrentComposer.WithState(string.Empty).Remember<System.Func<float, float>>(__ => it => -it))).With(animationSpec: animationSpec)), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(AnimateColorAsState(targetValue: isSwitched.Value ? Color.green : Color.red, animationSpec: animationSpec).Value), content: CurrentComposer.WithState(string.Empty).Remember<System.Action<string>>(__ => state =>
                        {
                            Text(text: state.ToString(), color: Color.white, fontSize: 64);
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100, vertical: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(CurrentComposer.WithState(isSwitched).Remember<System.Action>(__ => () => isSwitched.Value = !isSwitched.Value)));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}