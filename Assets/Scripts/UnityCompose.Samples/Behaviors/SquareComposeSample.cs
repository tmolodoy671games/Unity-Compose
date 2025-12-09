// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Preview();

        [Composable]
        protected override void Preview()
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
                            .Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())
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

        [Composable]
        private static void Layout()
        {
            var isSwitched = Remember(() => LoggableMutableStateOf(false));
            var isHovered = Remember(() => LoggableMutableStateOf(false));
            Box(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    if (isSwitched.Value)
                    {
                        Spacer(
                            modifier: Modifier
                                .Size(50)
                                .Background(Color.green)
                                .Border(16)
                                .Margin(top: 100)
                        );
                    }

                    Box(
                        horizontalAlignment: Alignment.Horizontal.Center,
                        verticalAlignment: Alignment.Vertical.Center,
                        modifier: Modifier
                            .Background(Color.blue)
                            .Border(16)
                            .Size(100)
                            .Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())
                            // .Scale(AnimateFloatAsState(isHovered.Value ? 1.5f : 1).Value)
                            .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false),
                        content: () =>
                        {
                            Box(
                                modifier: Modifier
                                    .Size(50)
                                    .Background(Color.red)
                                    .Border(16),
                                content: () => { Text(text: "Text", color: Color.white); }
                            );
                        }
                    );
                    if (isSwitched.Value)
                    {
                        Spacer(
                            modifier: Modifier
                                .Size(50)
                                .Background(Color.green)
                                .Border(16)
                                .Margin(top: 100)
                        );
                    }
                }
            );
        }
    }
}