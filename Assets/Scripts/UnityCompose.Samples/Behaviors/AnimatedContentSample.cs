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
                style: IModifier.Empty
                    .Width(100.Percent())
                    .Height(100.Percent())
                    .FlexGrow(1),
                content: () =>
                {
                    Column(
                        alignHorizontally: Align.Center,
                        style: IModifier.Empty
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
                                style: IModifier.Empty
                                    .Name("animated-content")
                                    .BackgroundColor(
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
                                style: IModifier.Empty
                                    .Padding(100, 32)
                                    .BackgroundColor(Color.blue)
                                    .MarginTop(16)
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