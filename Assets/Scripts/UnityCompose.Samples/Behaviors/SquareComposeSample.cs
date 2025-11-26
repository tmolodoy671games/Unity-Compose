// ReSharper disable ArrangeNamespaceBody

using System;
using System.Collections;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            var hovered = Remember(() => MutableStateOf(false));
            Spacer(
                Modifier
                    .Align(Alignment.CenterVertically)
                    .Size(100)
                    .Scale(hovered.Value ? 2 : 1, transition: Transition())
                    .Background(Color.blue)
                    .OnMouseEnter(() => hovered.Value = true)
                    .OnMouseLeave(() => hovered.Value = false)
            );
        }

        [Composable]
        protected override void Preview()
        {
            var hovered = Remember(() => MutableStateOf(false));
            Spacer(
                Modifier
                    .Align(Alignment.CenterVertically)
                    .Size(100)
                    .Scale(hovered.Value ? 2 : 1, transition: Transition())
                    .Background(Color.blue)
                    .OnMouseEnter(() => hovered.Value = true)
                    .OnMouseLeave(() => hovered.Value = false)
            );
        }

        [Composable]
        private void EmptyWrapper([Composable] Action content)
        {
            content();
        }

        [Composable]
        private void Layout()
        {
            Box(
                modifier: Modifier
                    .FillMaxSize(),
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                content: () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    var isPressed = Remember(() => MutableStateOf(false));
                    Spacer(
                        modifier: Modifier
                            .Size(100)
                            .Background(isPressed.Value ? Color.cyan : Color.blue, Transition())
                            .Border(radius: 32)
                            // .Scale(isHovered.Value ? 2 : 1, transition: Transition())
                            .Scale(AnimateFloatAsState(isHovered.Value ? 2 : 1).Value)
                            .OnMouseEnter(() =>
                            {
                                isHovered.Value = true;
                                PrintTreeStructureDelayed();
                            })
                            .OnMouseLeave(() =>
                            {
                                isPressed.Value = false;
                                isHovered.Value = false;
                                PrintTreeStructureDelayed();
                            })
                            .OnMouseDown(() => isPressed.Value = true)
                            .OnMouseUp(() => isPressed.Value = false)
                    );
                }
            );
        }

        private void PrintTreeStructureDelayed()
        {
            StartCoroutine(PrintTreeStructureDelayedCoroutine());
        }

        private IEnumerator PrintTreeStructureDelayedCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            // PrintTreeStructure();
        }
    }
}