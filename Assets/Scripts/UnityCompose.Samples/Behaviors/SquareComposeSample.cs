// ReSharper disable ArrangeNamespaceBody

using System;
using System.Collections;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview()
        {
            
        }

        [Composable]
        private  void Layout()
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