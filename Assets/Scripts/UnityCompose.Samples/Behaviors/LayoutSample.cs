// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class LayoutSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            Layout();
        }

        [Composable]
        protected override void Preview()
        {
            Column(() =>
            {
                Spacer(
                    modifier: Modifier
                        .Size(100.Dp())
                        .Background(Color.yellow)
                );
                Row(() =>
                {
                    for (var i = 0; i < 10; i++)
                    {
                        Spacer(
                            modifier: Modifier
                                .Size(100.Dp())
                                .Background(Color.yellow)
                        );
                    }
                });
            });
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
                            .Size(400.Dp()),
                        content: () =>
                        {
                            Column(() =>
                            {
                                Spacer(
                                    modifier: Modifier
                                        .Size(100.Dp())
                                        .Float()
                                        .Background(Color.yellow)
                                        .Position(top: 5.Dp())
                                );
                                Row(() =>
                                {
                                    for (var i = 0; i < 10; i++)
                                    {
                                        Spacer(
                                            modifier: Modifier
                                                .Size(100.Dp())
                                                .Float()
                                                .Background(Color.yellow)
                                                .Position(top: 5.Dp())
                                        );
                                    }
                                });
                            });
                            Spacer(
                                modifier: Modifier
                                    .Size(100.Dp())
                                    .Float()
                                    .Background(Color.yellow)
                                    .Position(bottom: 5.Dp())
                            );
                        }
                    );
                }
            );
        }
    }
}