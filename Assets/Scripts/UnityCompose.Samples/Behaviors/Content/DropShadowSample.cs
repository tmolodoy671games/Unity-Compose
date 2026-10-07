using Compose.Net;
using SharpExtensions;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class DropShadowSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            Column(
                verticalArrangement: Arrangement.Center,
                horizontalAlignment: Alignment.CenterHorizontally,
                modifier: Modifier
                    .FillMaxSize()
                    .Background(Color.white.ToSystemColor()),
                content: () =>
                {
                    var hasShadow = Remember(() => MutableStateOf(true));
                    var tween = Tween();
                    Spacer(
                        Modifier
                            .Size(100.Dp())
                            .Background(Color.gray.ToSystemColor())
                            .DropShadow(
                                shape: RoundedCornerShape(0.Dp()),
                                shadow: Shadow(
                                    radius: AnimateFloatAsState(32 * hasShadow.Value.ToInt(), tween).Value.Dp(),
                                    spread: 10.Dp(),
                                    color: AnimateColorAsState(hasShadow.Value ? Color.black : new Color(), tween).Value
                                        .ToSystemColor(),
                                    offset: new Offset()
                                )
                            )
                    );
                    Text(
                        text: "Switch",
                        fontSize: 32.Sp(),
                        color: Color.white.ToSystemColor(),
                        modifier: Modifier
                            .Padding(top: 16.Dp())
                            .Padding(top: 16.Dp())
                            .Padding(top: 16.Dp())
                            .Padding(top: 16.Dp())
                            .Background(Color.royalBlue.ToSystemColor())
                            .Padding(16.Dp())
                            .Padding(top: 16.Dp())
                            .Padding(top: 16.Dp())
                            .Padding(top: 16.Dp())
                            .Padding(top: 16.Dp())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .OnClick(() => hasShadow.Value = !hasShadow.Value)
                    );
                }
            );
        }
    }
}