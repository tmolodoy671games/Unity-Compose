using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent : ComposeUI
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
            Column(
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: Modifier
                    .Width(100.Percent())
                    .Height(100.Percent())
                    .BackgroundColor(Color.white),
                content: () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    Box(
                        style: Modifier
                            .Padding(isHovered.Value ? 80 : 40, 16, Transition())
                            .BackgroundColor(Color.blue)
                            .BorderRadius(16)
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false),
                        content: () =>
                        {
                            Label(
                                text: "Click me",
                                fontSize: 24
                            );
                        }
                    );
                }
            );
        }
    }
}