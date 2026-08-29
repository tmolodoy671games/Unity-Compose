// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ContentSwapSample : ComposeUI
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
                modifier: Modifier.FillMaxSize(),
                content: () =>
                {
                    var isSwitched = Remember(() => MutableStateOf(false));
                    if (!isSwitched.Value)
                        Content2();
                    else
                        Content1();

                    Text(
                        text: "Switch",
                        color: Color.white,
                        fontSize: 62.Sp(),
                        modifier: Modifier
                            .Padding(horizontal: 20.Dp(), vertical: 12.Dp())
                            .Border(16.Dp())
                            .Background(Color.blue)
                            .Margin(top: 16.Dp())
                            .OnClick(() => isSwitched.Value = !isSwitched.Value)
                    );
                }
            );
        }

        [Composable]
        private static void Content1()
        {
            Spacer(
                Modifier
                    .Size(100.Dp())
                    .Background(Color.green)
            );
        }

        [Composable]
        private static void Content2()
        {
            Row(() =>
            {
                Spacer(
                    Modifier
                        .Size(100.Dp())
                        .Background(Color.red)
                );
                Spacer(
                    Modifier
                        .Size(100.Dp())
                        .Background(Color.red)
                );
            });
        }
    }
}