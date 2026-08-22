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
                        .Size(100.Px())
                        .Background(Color.yellow)
                );
                Row(() =>
                {
                    for (var i = 0; i < 10; i++)
                    {
                        Spacer(
                            modifier: Modifier
                                .Size(100.Px())
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
                            .Size(400.Px()),
                        content: () =>
                        {
                            Column(() =>
                            {
                                Spacer(
                                    modifier: Modifier
                                        .Size(100.Px())
                                        .Float()
                                        .Background(Color.yellow)
                                        .Position(top: 5.Px())
                                );
                                Row(() =>
                                {
                                    for (var i = 0; i < 10; i++)
                                    {
                                        Spacer(
                                            modifier: Modifier
                                                .Size(100.Px())
                                                .Float()
                                                .Background(Color.yellow)
                                                .Position(top: 5.Px())
                                        );
                                    }
                                });
                            });
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