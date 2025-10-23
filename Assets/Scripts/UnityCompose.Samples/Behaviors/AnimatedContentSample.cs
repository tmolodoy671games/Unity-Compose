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
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: Modifier
                    .Width(100.Percent())
                    .Height(100.Percent())
                    .FlexGrow(1),
                content: () =>
                {
                    Column(
                        alignHorizontally: Align.Center,
                        style: Modifier
                            .Name("animated-content-sample"),
                        content: () =>
                        {
                            var isSwitched = Remember(() => MutableStateOf(false));
                            AnimatedContent(
                                value: isSwitched.Value ? "Looooooooooooooooooong" : "Short",
                                transition: (_, _) =>
                                    isSwitched.Value
                                        ? ContentTransform(
                                            enter: SlideIn(SlideDirection.Up) + FadeIn(),
                                            exit: SlideOut(SlideDirection.Up) + FadeOut()
                                        )
                                        : ContentTransform(
                                            enter: SlideIn(SlideDirection.Down) + FadeIn(),
                                            exit: SlideOut(SlideDirection.Down) + FadeOut()
                                        ),
                                animateSize: true,
                                transitionDuration: Duration,
                                style: Modifier
                                    .Name("animated-content")
                                    .Background(
                                        isSwitched.Value ? Color.green : Color.red,
                                        Transition(Duration)
                                    ),
                                content: state =>
                                {
                                    Label(
                                        text: state.ToString(),
                                        textColor: Color.white,
                                        fontSize: 64
                                    );
                                }
                            );

                            Label(
                                text: "Switch",
                                textColor: Color.white,
                                fontSize: 64,
                                style: Modifier
                                    .NewPadding(horizontal: 100, vertical: 32)
                                    .Background(Color.blue)
                                    .Margin(top: 16)
                                    .BorderRadius(16)
                                    .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            );
                        }
                    );
                }
            );
        }
    }
}