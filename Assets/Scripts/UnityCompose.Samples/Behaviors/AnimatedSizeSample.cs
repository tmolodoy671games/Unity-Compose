using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedSizeSample : ComposeUI
    {
        protected override void Content()
        {
            Layout();
        }

        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            Box(
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Column(
                        alignHorizontally: Align.Center,
                        style: Modifier
                            .Name("animated-size-sample"),
                        content: () =>
                        {
                            var isSwitched = Remember(() => MutableStateOf(false));
                            var text = isSwitched.Value
                                ? "Short"
                                : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";

                            AnimatedSize(
                                style: Modifier
                                    .Name("animated-size")
                                    .Background(isSwitched.Value ? Color.green : Color.red, Transition(5))
                                    .NewPadding(all: 16),
                                duration: 5,
                                content: () =>
                                {
                                    Text(
                                        text: text,
                                        textColor: Color.white,
                                        fontSize: 64,
                                        align: TextAnchor.MiddleCenter,
                                        style: Modifier
                                            .Name("animated-label-child")
                                    );
                                }
                            );

                            Text(
                                text: "Switch",
                                textColor: Color.white,
                                fontSize: 64,
                                style: Modifier
                                    .Name("switch-button")
                                    .NewPadding(all: 32)
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