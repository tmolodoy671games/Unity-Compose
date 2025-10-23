using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample : ComposeUI
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
            var isHovered = Remember(() => MutableStateOf(false));
            var isPressed = Remember(() => MutableStateOf(false));
            Box(
                style: Modifier
                    .FillMaxSize(),
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                content: () =>
                {
                    Spacer(
                        style: Modifier
                            .Size(100)
                            .Background(isPressed.Value ? Color.cyan : Color.blue, Transition())
                            .Border(radius: 32)
                            .Scale(isHovered.Value ? 2 : 1, Transition())
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() =>
                            {
                                isPressed.Value = false;
                                isHovered.Value = false;
                            })
                            .OnMouseDown(() => isPressed.Value = true)
                            .OnMouseUp(() => isPressed.Value = false)
                    );
                }
            );
        }
    }
}