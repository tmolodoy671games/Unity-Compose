// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class LayoutSample : ComposeUI
    {
        protected override void Content()
        {
            Layout();
        }

        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Box(
                        modifier: Modifier
                            .Background(Color.red)
                            .Size(400.Px()),
                        content: () =>
                        {
                            Spacer(
                                modifier: Modifier
                                    .Size(100.Px())
                                    .Float()
                                    .Background(Color.yellow)
                                    .Position(top: 5.Px())
                            );
                            Spacer(
                                modifier: Modifier
                                    .Size(100.Px())
                                    .Float()
                                    .Background(Color.yellow)
                                    .Position(bottom: 5.Px())
                            );
                        }
                    );
                }
            );
        }
    }
}