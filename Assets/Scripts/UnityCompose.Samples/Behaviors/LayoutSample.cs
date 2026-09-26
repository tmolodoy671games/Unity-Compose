// ReSharper disable ArrangeNamespaceBody

using Compose.Net;

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
                        .Background(Color.yellow.ToSystemColor())
                );
                Row(() =>
                {
                    for (var i = 0; i < 10; i++)
                    {
                        Spacer(
                            modifier: Modifier
                                .Size(100.Dp())
                                .Background(Color.yellow.ToSystemColor())
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
                            .Background(Color.red.ToSystemColor())
                            .Size(400.Dp()),
                        content: () =>
                        {
                            Column(() =>
                            {
                                Spacer(
                                    modifier: Modifier
                                        .Size(100.Dp())
                                        .Float()
                                        .Background(Color.yellow.ToSystemColor())
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
                                                .Background(Color.yellow.ToSystemColor())
                                                .Position(top: 5.Dp())
                                        );
                                    }
                                });
                            });
                            Spacer(
                                modifier: Modifier
                                    .Size(100.Dp())
                                    .Float()
                                    .Background(Color.yellow.ToSystemColor())
                                    .Position(bottom: 5.Dp())
                            );
                        }
                    );
                }
            );
        }
    }
}