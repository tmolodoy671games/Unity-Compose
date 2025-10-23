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
                style: IModifier.Empty
                    .Width(100.Percent())
                    .Height(100.Percent())
                    .FlexGrow(1),
                content: () =>
                {
                    Column(
                        alignHorizontally: Align.Center,
                        style: IModifier.Empty
                            .Name("animated-size-sample"),
                        content: () =>
                        {
                            var isSwitched = Remember(() => MutableStateOf(false));
                            var text = isSwitched.Value
                                ? "Short"
                                : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";

                            AnimatedSize(
                                style: IModifier.Empty
                                    .Name("animated-size")
                                    .BackgroundColor(isSwitched.Value ? Color.green : Color.red, Transition(5))
                                    .Padding(16),
                                duration: 5,
                                content: () =>
                                {
                                    Label(
                                        text: text,
                                        textColor: Color.white,
                                        fontSize: 64,
                                        align: TextAnchor.MiddleCenter,
                                        style: IModifier.Empty
                                            .Name("animated-label-child")
                                    );
                                }
                            );

                            Label(
                                text: "Switch",
                                textColor: Color.white,
                                fontSize: 64,
                                style: IModifier.Empty
                                    .Name("switch-button")
                                    .Padding(32)
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