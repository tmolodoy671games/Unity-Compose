using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample : ComposeUI
    {
        [Composable]
        protected override void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Content());
            }
        }

        [Composable]
        protected override void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Preview());
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                var isHovered = Remember(static () => MutableStateOf(false));
                var isPressed = Remember(static () => MutableStateOf(false));
                Box(modifier: Modifier.FillMaxSize(), horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, content: CurrentComposer.WithState((isHovered, isPressed)).Remember<Action>(__ => () =>
                {
                    Spacer(modifier: Modifier.Size(100).Background(isPressed.Value ? Color.cyan : Color.blue, Transition()).Border(radius: 32).Scale(isHovered.Value ? 2 : 1, transition: Transition()).OnMouseEnter(CurrentComposer.WithState(__.isHovered).Remember<Action>(__ => () => isHovered.Value = true)).OnMouseLeave(CurrentComposer.WithState((__.isHovered, __.isPressed)).Remember<Action>(__ => () =>
                    {
                        isPressed.Value = false;
                        isHovered.Value = false;
                    })).OnMouseDown(CurrentComposer.WithState(__.isPressed).Remember<Action>(__ => () => isPressed.Value = true)).OnMouseUp(CurrentComposer.WithState(__.isPressed).Remember<Action>(__ => () => isPressed.Value = false)));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }
    }
}