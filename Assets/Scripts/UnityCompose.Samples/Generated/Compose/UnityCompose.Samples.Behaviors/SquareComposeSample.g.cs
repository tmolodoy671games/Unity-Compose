using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample
    {
        [Composable]
        [Compiled]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }

        [Composable]
        [Compiled]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Preview());
            }
        }

        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                var isHovered = Remember(() => MutableStateOf(false));
                var isPressed = Remember(() => MutableStateOf(false));
                Spacer(style: ComposeStyle.Empty.Size(100).BackgroundColor(isPressed.Value ? Color.cyan : Color.blue, Transition()).AlignSelf(Align.Center).BorderRadius(32).Top(50.Percent()).Bottom(50.Percent()).Pivot(0, 0.5f).Scale(isHovered.Value ? 2 : 1, Transition()).OnMouseEnter(Remember<global::System.Action>(isHovered, () => isHovered.Value = true)).OnMouseLeave(Remember<global::System.Action>((isHovered, isPressed), () =>
                {
                    isPressed.Value = false;
                    isHovered.Value = false;
                })).OnMouseDown(Remember<global::System.Action>(isPressed, () => isPressed.Value = true)).OnMouseUp(Remember<global::System.Action>(isPressed, () => isPressed.Value = false)));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}