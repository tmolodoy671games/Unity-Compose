using Compose.Net;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    public partial class AnimatedSizeSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    var isSwitched = Remember(() => MutableStateOf(true));
                    Box(
                        alignment: Alignment.Center,
                        modifier: Modifier
                            .Clip(RoundedCornerShape(32.Dp()))
                            .Padding(16.Dp())
                            .Background(Color.lightCoral.ToSystemColor())
                            .AnimateContentSize(Tween(1))
                            .OnClick(() => isSwitched.Value = !isSwitched.Value),
                        content: () => Text(
                            text: isSwitched.Value ? "Short" : "Looooooooooooooooooooong\nLooooooooooooooooooooong\nLooooooooooooooooooooong",
                            fontSize: 64.Sp(),
                            color: Color.white.ToSystemColor()
                        )
                    );
                }
            );
        }
    }
}