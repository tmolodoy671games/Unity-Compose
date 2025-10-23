using UnityEngine.UIElements;

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
                            var isSwitched = Remember(() => MutableStateOf(false));
                            AnimatedContent(
                                value: isSwitched.Value ? "Looooooooooooooooooong" : "Short",
                                transition: (_, _) =>
                                    isSwitched.Value
                                        ? ContentTransform(
                                            enter: SlideInVertically(it => -it) + FadeIn(),
                                            exit: SlideOutVertically(it => it) + FadeOut()
                                        )
                                        : ContentTransform(
                                            enter: SlideInVertically(it => it) + FadeIn(),
                                            exit: SlideOutVertically(it => -it) + FadeOut()
                                        ),
                                animateSize: true,
                                transitionDuration: Duration,
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
                                        textColor: Color.white,
                                        fontSize: 64
                                    );
                                }
                            );

                            Text(
                                text: "Switch",
                                textColor: Color.white,
                                fontSize: 64,
                                modifier: Modifier
                                    .NewPadding(horizontal: 100, vertical: 32)
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