// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedSizeSample : ComposeUI
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
            const int AnimationDuration = 2;
            var animationSpec = Tween(AnimationDuration);
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Column(
                        horizontalAlignment: Alignment.CenterHorizontally,
                        modifier: Modifier
                            .Name("animated-size-sample"),
                        content: () =>
                        {
                            var isSwitched = Remember(() => MutableStateOf(false));
                            var text = isSwitched.Value
                                ? "Short"
                                : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                            AnimatedSize(
                                modifier: Modifier
                                    .Name("animated-size")
                                    .Background(
                                        isSwitched.Value ? Color.green : Color.red,
                                        Transition(AnimationDuration)
                                    )
                                    .Padding(all: 16.Px())
                                    .Border(
                                        AnimateFloatAsState(isSwitched.Value ? 8 : 32, animationSpec).Value.Px()
                                    ),
                                animationSpec: animationSpec,
                                content: modifier =>
                                {
                                    Text(
                                        text: text,
                                        color: Color.white,
                                        fontSize: 64,
                                        textAlign: TextAlign.MiddleCenter,
                                        modifier: modifier
                                            .Name("animated-label-child")
                                    );
                                }
                            );

                            Text(
                                text: "Switch",
                                color: Color.white,
                                fontSize: 64,
                                modifier: Modifier
                                    .Name("switch-button")
                                    .Padding(all: 32.Px())
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