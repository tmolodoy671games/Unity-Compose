using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedContentSample
    {
        [Composable]
        [Compiled]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(null))
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
        [Compiled]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(null))
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
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                const int Duration = 1;
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>(Duration, () =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-content-sample"), content: RememberComposable<global::System.Action>(Duration, () =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = Remember(() => MutableStateOf(false));
                        AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: Remember<global::System.Func<global::UnityCompose.IAnimatedContentTransitionScope<string>, global::UnityCompose.ContentTransform>>((animationSpec, isSwitched), _ => isSwitched.Value ? SlideInVertically(it => -it, animationSpec: animationSpec).TogetherWith(SlideOutVertically(it => it, animationSpec: animationSpec)) : SlideInVertically(it => it, animationSpec: animationSpec).TogetherWith(SlideOutVertically(it => -it, animationSpec: animationSpec))), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(isSwitched.Value ? Color.green : Color.red, Transition(Duration)), content: RememberComposable<global::System.Action<string>>(null, state =>
                        {
                            Text(text: state.ToString(), color: Color.white, fontSize: 64);
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100, vertical: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value)));
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