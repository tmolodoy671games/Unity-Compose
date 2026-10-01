using Compose.Net;
using SharpExtensions;

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class ShadowSample : ComposeUI
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
                    Spacer(
                        Modifier
                            .Size(100.Dp())
                            .Background(Color.gray.ToSystemColor())
                            .DropShadow(
                                shape: RoundedCornerShape(0.Dp()),
                                shadow: new Shadow(
                                    Radius: AnimateFloatAsState(32f * hasShadow.Value.ToInt()).Value.Dp(),
                                    Color: AnimateColorAsState(
                                        hasShadow.Value
                                            ? Color.black.ToSystemColor()
                                            : new Color(0, 0, 0, 0).ToSystemColor()
                                    ).Value,
                                    Offset: new Offset()
                                )
                            )
                    );
                    Text(
                        text: "Switch",
                        fontSize: 32.Sp(),
                        color: Color.white.ToSystemColor(),
                        modifier: Modifier
                            .Background(Color.royalBlue.ToSystemColor())
                            .Padding(16.Dp())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .Margin(top: 16.Dp())
                            .OnClick(() => hasShadow.Value = !hasShadow.Value)
                    );
                }
            );
        }
    }
}