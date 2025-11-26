using System;
using System.Collections;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

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
                var hovered = Remember(() => MutableStateOf(false));
                Spacer(Modifier.Align(Alignment.CenterVertically).Size(100).Scale(hovered.Value ? 2 : 1, transition: Transition()).Background(Color.blue).OnMouseEnter(CurrentComposer.WithState(hovered).Remember<System.Action>(__ => () => hovered.Value = true)).OnMouseLeave(CurrentComposer.WithState(hovered).Remember<System.Action>(__ => () => hovered.Value = false)));
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
                var hovered = Remember(() => MutableStateOf(false));
                Spacer(Modifier.Align(Alignment.CenterVertically).Size(100).Scale(hovered.Value ? 2 : 1, transition: Transition()).Background(Color.blue).OnMouseEnter(CurrentComposer.WithState(hovered).Remember<System.Action>(__ => () => hovered.Value = true)).OnMouseLeave(CurrentComposer.WithState(hovered).Remember<System.Action>(__ => () => hovered.Value = false)));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Preview());
            }
        }

        [Composable]
        private void __EmptyWrapper([Composable] Action content)
        {
            var __content = (content);
            if (CurrentComposer.BeginComposeGroup(__content))
                return;
            try
            {
                content();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.WithState(__content).Remember<Action>(__ => () => __EmptyWrapper(__)));
            }
        }

        [Composable]
        private void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Box(modifier: Modifier.FillMaxSize(), horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, content: CurrentComposer.WithState(this).Remember<System.Action>(__ => () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    var isPressed = Remember(() => MutableStateOf(false));
                    Spacer(modifier: Modifier.Size(100).Background(isPressed.Value ? Color.cyan : Color.blue, Transition()).Border(radius: 32)// .Scale(isHovered.Value ? 2 : 1, transition: Transition())
                    .Scale(AnimateFloatAsState(isHovered.Value ? 2 : 1).Value).OnMouseEnter(CurrentComposer.WithState((this, isHovered)).Remember<System.Action>(__ => () =>
                    {
                        isHovered.Value = true;
                        PrintTreeStructureDelayed();
                    })).OnMouseLeave(CurrentComposer.WithState((this, isHovered, isPressed)).Remember<System.Action>(__ => () =>
                    {
                        isPressed.Value = false;
                        isHovered.Value = false;
                        PrintTreeStructureDelayed();
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