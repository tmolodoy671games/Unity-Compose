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
            var isSwitched = Remember(() => MutableStateOf(false));
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
                            .Scale(AnimateFloatAsState(isSwitched.Value ? 1.5f : 1f).Value)
                    );
                    // Spacer(
                    //     Modifier
                    //         .Size(100)
                    //         .Background(Color.green)
                    //         .Border(16)
                    //         .OnClick(() => isSwitched.Value = !isSwitched.Value)
                    //         .Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())
                    // );
                    // Spacer(
                    //     Modifier
                    //         .Size(100)
                    //         .Background(Color.blue)
                    //         .Border(16)
                    //         .OnClick(() => isSwitched.Value = !isSwitched.Value)
                    //         .Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())
                    // );
                }
            );
        }
    }
}