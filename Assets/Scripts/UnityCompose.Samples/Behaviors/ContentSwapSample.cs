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
                        fontSize: 62,
                        modifier: Modifier
                            .Padding(horizontal: 20.Px(), vertical: 12.Px())
                            .Border(16.Px())
                            .Background(Color.blue)
                            .Margin(top: 16.Px())
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
                    .Size(100.Px())
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
                        .Size(100.Px())
                        .Background(Color.red)
                );
                Spacer(
                    Modifier
                        .Size(100.Px())
                        .Background(Color.red)
                );
            });
        }
    }
}