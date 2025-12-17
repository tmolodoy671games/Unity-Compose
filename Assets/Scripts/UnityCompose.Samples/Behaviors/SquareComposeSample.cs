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
            Column(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    var isRedSwitched = Remember(() => MutableStateOf(false));
                    Spacer(
                        Modifier
                            .Size(100)
                            .Background(Color.red)
                            .Border(16)
                            .OnClick(() => isRedSwitched.Value = !isRedSwitched.Value)
                            .Scale(AnimateFloatAsState(isRedSwitched.Value ? 1.5f : 1f).Value)
                            .Margin(top: 40)
                    );

                    var isGreenSwitched = Remember(() => MutableStateOf(false));
                    Spacer(
                        Modifier
                            .Size(100)
                            .Background(Color.green)
                            .Border(16)
                            .OnClick(() => isGreenSwitched.Value = !isGreenSwitched.Value)
                            .Scale(AnimateFloatAsState(isGreenSwitched.Value ? 1.5f : 1f).Value)
                            .Margin(top: 40)
                    );

                    var isBlueSwitched = Remember(() => MutableStateOf(false));
                    Spacer(
                        Modifier
                            .Size(100)
                            .Background(Color.blue)
                            .Border(16)
                            .OnClick(() => isBlueSwitched.Value = !isBlueSwitched.Value)
                            .Scale(AnimateFloatAsState(isBlueSwitched.Value ? 1.5f : 1f).Value)
                            .Margin(top: 40)
                    );
                }
            );
        }
    }
}