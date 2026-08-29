// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ScrollableLayoutSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        { 
            Box(
                alignment: Alignment.Center,
                modifier: Modifier.FillMaxSize(),
                content: () =>
                {
                    Column(
                        horizontalAlignment: Alignment.CenterHorizontally,
                        content: () =>
                        {
                            ColumnSample();
                            Spacer(Modifier.Height(100.Dp()));
                            RowSample();
                        }
                    );
                    Spacer(
                        Modifier
                            .Float()
                            .Size(100.Percent())
                            .IgnoreInput()
                    );
                }
            );
        }

        [Composable]
        private static void ColumnSample()
        {
            var state = RememberScrollState();
            Row(() =>
            {
                Box(
                    modifier: Modifier
                        .Height(400.Dp()),
                    content: () =>
                    {
                        ScrollableColumn(
                            state: state,
                            content: () =>
                            {
                                Column(
                                    modifier: Modifier.Background(Color.white),
                                    content: () =>
                                    {
                                        for (var i = 0; i < 20; i++)
                                        {
                                            Text(
                                                text: i.ToString(),
                                                color: Color.white,
                                                fontSize: 32.Sp(),
                                                textAlign: TextAlign.MiddleCenter,
                                                modifier: Modifier
                                                    .Background(Color.red)
                                                    .Size(100.Dp())
                                                    .Margin(vertical: 4.Dp())
                                                    .OnClick(() => Debug.Log("Glick"))
                                            );
                                        }
                                    }
                                );
                            }
                        );
                        Box(
                            modifier: Modifier
                                .Height(100.Percent())
                                .Float()
                                .Position(right: 0.Dp()),
                            content: () =>
                            {
                                var scrollerSize = state.ViewportSize / state.ContentSize;
                                Spacer(
                                    Modifier
                                        .Background(Color.cadetBlue)
                                        .Width(32.Dp())
                                        .Border(16.Dp())
                                        .Height(scrollerSize * 100.Percent())
                                        .Position(top: (state.Value / state.ContentSize) * 100.Percent())
                                );
                            }
                        );
                    }
                );
            });
        }

        [Composable]
        private static void RowSample()
        {
            var state = RememberScrollState();
            Column(() =>
            {
                ScrollableRow(
                    state: state,
                    modifier: Modifier
                        .Width(400.Dp()),
                    content: () =>
                    {
                        Row(
                            modifier: Modifier.Background(Color.white),
                            content: () =>
                            {
                                for (var i = 0; i < 20; i++)
                                {
                                    Text(
                                        text: i.ToString(),
                                        color: Color.white,
                                        fontSize: 32.Sp(),
                                        textAlign: TextAlign.MiddleCenter,
                                        modifier: Modifier
                                            .Background(Color.red)
                                            .Size(100.Dp())
                                            .Margin(horizontal: 4.Dp())
                                            .OnClick(() => Debug.Log("Glick"))
                                    );
                                }
                            }
                        );
                    }
                );
                const float width = 32;
                Row(
                    modifier: Modifier
                        .FillMaxWidth()
                        .Height(width.Dp()),
                    content: () =>
                    {
                        Spacer(
                            Modifier
                                .Size(width.Dp())
                                .Background(Color.forestGreen)
                                .OnClick(() => state.AnimateScrollBy(-200))
                        );
                        Spacer(
                            Modifier
                                .Weight(1)
                                .Background(Color.indianRed)
                        );
                        Spacer(
                            Modifier
                                .Size(width.Dp())
                                .Background(Color.forestGreen)
                                .OnClick(() => state.AnimateScrollBy(200))
                        );
                    }
                );
            });
        }
    }
}