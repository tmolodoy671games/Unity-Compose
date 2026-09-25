using System;
using System.Collections.Generic;
using Compose.Net;
using SharpExtensions;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class MinimalSample : ComposeUI
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
                content: () =>
                {
                    var interactionSource = Remember(MutableInteractionSource);
                    var hovered = interactionSource.CollectIsHoveredAsState().Value;
                    var pressed = interactionSource.CollectIsPressedAsState().Value;
                    var background = AnimateColorAsState(
                        pressed
                            ? Color.darkGreen.ToSystemColor()
                            : Color.seaGreen.ToSystemColor()
                    ).Value;
                    Box(
                        modifier: Modifier
                            .Background(background)
                            .Clip(RoundedCornerShape(16.Dp()))
                            .Hoverable(interactionSource)
                            .Clickable(interactionSource)
                            .Padding(vertical: 16.Dp())
                            .Padding(
                                horizontal: AnimateFloatAsState(hovered ? 128 : 16).Value.Dp()
                            ),
                        content: () =>
                        {
                            Text(
                                fontSize: 40.Sp(),
                                color: Color.white.ToSystemColor(),
                                text: "Click Me!",
                                modifier: Modifier
                                    .Scale(AnimateFloatAsState(pressed ? 0.6f : 1f).Value)
                            );
                        }
                    );
                }
            );
        }
    }
}