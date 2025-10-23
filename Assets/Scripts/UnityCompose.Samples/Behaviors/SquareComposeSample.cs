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
            Spacer(
                style: Modifier
                    .Size(100)
                    .Background(isPressed.Value ? Color.cyan : Color.blue, Transition())
                    .AlignSelf(Align.Center)
                    .BorderRadius(32)
                    .Top(50.Percent())
                    .Bottom(50.Percent())
                    .Pivot(0, 0.5f)
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
    }
}