using Compose.Net;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class MinimalSample : ComposePreview
    {
        [Composable]
        protected override void Preview()
        {
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: [Composable]() =>
                {
                    var interactionSource = Remember(MutableInteractionSource);
                    var isHovered = interactionSource.CollectIsHoveredAsState().Value;
                    Box(
                        modifier: Modifier
                            .Background(Color.lightGreen.ToSystemColor())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .Hoverable(interactionSource)
                            .Padding(vertical:16.Dp())
                            .Padding(horizontal: isHovered ? 32.Dp() : 16.Dp()),
                        content: [Composable] () =>
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