using System;
using Compose.Net;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class MinimalSample : ComposeContent
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            var a = Remember(1, () => 2);
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: [Composable]() =>
                {
                    var b = Remember(MutableInteractionSource);
                    var interactionSource = Remember(MutableInteractionSource);
                    var isHovered = interactionSource.CollectIsHoveredAsState().Value;
                    Box(
                        modifier: Modifier
                            .Background(Color.lightGreen.ToSystemColor())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .Hoverable(interactionSource)
                            .Padding(vertical: 16.Dp()),
                        content: [Composable]() =>
                        {
                            Text(
                                fontSize: 40.Sp(),
                                color: Color.white.ToSystemColor(),
                                text: "Click Me!"
                            );
                        }
                    );
                }
            );
        }
    }
}