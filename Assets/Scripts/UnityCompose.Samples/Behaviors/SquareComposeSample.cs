// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            var isSwitched = Remember(() => LoggableMutableStateOf(false));
            Box(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Spacer(
                        Modifier
                            .Size(100)
                            .Background(Color.red)
                            .Border(16)
                            .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            .Scale(AnimateFloatAsState(isSwitched.Value ? 1.5f : 1f, animationSpec: Tween(duration: 2)).Value)
                    );
                }
            );
        }
    }
}