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
            const float Duration = 0.5f;
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Column(
                        horizontalAlignment: Alignment.CenterHorizontally,
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
                                        ? SlideInVertically(it => -it)
                                            .TogetherWith(SlideOutVertically(it => it))
                                            .With(animationSpec: animationSpec)
                                        : SlideInVertically(it => it)
                                            .TogetherWith(SlideOutVertically(it => -it))
                                            .With(animationSpec: animationSpec),
                                sizeAnimationSpec: animationSpec,
                                modifier: Modifier
                                    .Name("animated-content")
                                    .Background(
                                        AnimateColorAsState(
                                            targetValue: isSwitched.Value ? Color.green : Color.red,
                                            animationSpec: animationSpec
                                        ).Value
                                    ),
                                content: (state, modifier) =>
                                {
                                    Text(
                                        text: state.ToString(),
                                        color: Color.white,
                                        fontSize: 64,
                                        modifier: modifier
                                    );
                                }
                            );

                            Text(
                                text: "Switch",
                                color: Color.white,
                                fontSize: 64,
                                modifier: Modifier
                                    .Padding(horizontal: 100.Px(), vertical: 32.Px())
                                    .Background(Color.blue)
                                    .Margin(top: 16.Px())
                                    .Border(radius: 16.Px())
                                    .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            );
                        }
                    );
                }
            );
        }
    }
}