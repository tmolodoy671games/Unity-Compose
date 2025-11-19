using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
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
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
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
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                var isHovered = Remember(() => MutableStateOf(false));
                var isPressed = Remember(() => MutableStateOf(false));
                Box(modifier: Modifier.FillMaxSize(), horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, content: CurrentComposer.WithState((isHovered, isPressed)).Remember<System.Action>(__ => () =>
                {
                    Spacer(modifier: Modifier.Size(100).Background(isPressed.Value ? Color.cyan : Color.blue, Transition()).Border(radius: 32).Scale(isHovered.Value ? 2 : 1, transition: Transition()).OnMouseEnter(CurrentComposer.WithState(isHovered).Remember<System.Action>(__ => () => isHovered.Value = true)).OnMouseLeave(CurrentComposer.WithState((isHovered, isPressed)).Remember<System.Action>(__ => () =>
                    {
                        isPressed.Value = false;
                        isHovered.Value = false;
                    })).OnMouseDown(CurrentComposer.WithState(isPressed).Remember<System.Action>(__ => () => isPressed.Value = true)).OnMouseUp(CurrentComposer.WithState(isPressed).Remember<System.Action>(__ => () => isPressed.Value = false)));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}