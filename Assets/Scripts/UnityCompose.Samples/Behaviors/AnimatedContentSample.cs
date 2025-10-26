// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedContentSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            Layout();
        }

        [Composable]
        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            const int Duration = 1;
            Box(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Column(
                        horizontalAlignment: Alignment.Horizontal.Center,
                        modifier: Modifier
                            .Name("animated-content-sample"),
                        content: () =>
                        {
                            var animationSpec = Tween(
                                easing: EaseInOutEasing,
                                duration: Duration
                            );
                            var isSwitched = Remember(() => MutableStateOf(false));
                            AnimatedContent(
                                targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short",
                                transitionSpec: _ =>
                                    isSwitched.Value
                                        ? SlideInVertically(it => -it, animationSpec: animationSpec)
                                            .TogetherWith(
                                                SlideOutVertically(it => it, animationSpec: animationSpec)
                                            )
                                        : SlideInVertically(it => it, animationSpec: animationSpec)
                                            .TogetherWith(
                                                SlideOutVertically(it => -it, animationSpec: animationSpec)
                                            ),
                                sizeAnimationSpec: animationSpec,
                                modifier: Modifier
                                    .Name("animated-content")
                                    .Background(
                                        isSwitched.Value ? Color.green : Color.red,
                                        Transition(Duration)
                                    ),
                                content: state =>
                                {
                                    Text(
                                        text: state.ToString(),
                                        color: Color.white,
                                        fontSize: 64
                                    );
                                }
                            );

                            Text(
                                text: "Switch",
                                color: Color.white,
                                fontSize: 64,
                                modifier: Modifier
                                    .Padding(horizontal: 100, vertical: 32)
                                    .Background(Color.blue)
                                    .Margin(top: 16)
                                    .Border(radius: 16)
                                    .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            );
                        }
                    );
                }
            );
        }
    }
}