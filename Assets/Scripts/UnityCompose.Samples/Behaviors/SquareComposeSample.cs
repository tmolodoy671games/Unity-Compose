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
                horizontalAlignment: Alignment.CenterHorizontally,
                verticalArrangement: Arrangement.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    var isRedSwitched = Remember(() => MutableStateOf(false));
                    Spacer(
                        Modifier
                            .Size(100.Dp())
                            .Background(Color.red)
                            .Border(16.Dp())
                            .OnClick(() => isRedSwitched.Value = !isRedSwitched.Value)
                            .Scale(AnimateFloatAsState(isRedSwitched.Value ? 1.5f : 1f).Value)
                            .Margin(top: 40.Dp())
                    );

                    var isGreenSwitched = Remember(() => MutableStateOf(false));
                    Spacer(
                        Modifier
                            .Size(100.Dp())
                            .Background(Color.green)
                            .Border(16.Dp())
                            .OnClick(() => isGreenSwitched.Value = !isGreenSwitched.Value)
                            .Scale(AnimateFloatAsState(isGreenSwitched.Value ? 1.5f : 1f).Value)
                            .Margin(top: 40.Dp())
                    );

                    var isBlueSwitched = Remember(() => MutableStateOf(false));
                    Spacer(
                        Modifier
                            .Size(100.Dp())
                            .Background(Color.blue)
                            .Border(16.Dp())
                            .OnClick(() => isBlueSwitched.Value = !isBlueSwitched.Value)
                            .Scale(AnimateFloatAsState(isBlueSwitched.Value ? 1.5f : 1f).Value)
                            .Margin(top: 40.Dp())
                    );
                }
            );
        }
    }
}