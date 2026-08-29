// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class LazyLayoutSample : ComposeUI
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
                            .FillMaxSize()
                            .IgnoreInput()
                    );
                }
            );
        }

        [Composable]
        private static void ColumnSample()
        {
            var state = RememberLazyListState();
            Row(() =>
            {
                Box(
                    modifier: Modifier
                        .Height(400.Dp()),
                    content: () =>
                    {
                        LazyColumn(
                            state: state,
                            content: scope =>
                            {
                                scope.Items(
                                    count: 20,
                                    key: it => it,
                                    content: it =>
                                    {
                                        Text(
                                            text: it.ToString(),
                                            color: Color.white,
                                            fontSize: 32.Sp(),
                                            textAlign: TextAlign.MiddleCenter,
                                            modifier: Modifier
                                                .Background(Color.red)
                                                .Size(100.Dp())
                                                .Margin(vertical: 4.Dp())
                                                .OnClick(() => state.AnimateScrollToItem(it))
                                        );
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
            var state = RememberLazyListState();
            Column(() =>
            {
                LazyRow(
                    state: state,
                    modifier: Modifier
                        .Width(400.Dp()),
                    content: scope =>
                    {
                        Row(
                            modifier: Modifier.Background(Color.white),
                            content: () =>
                            {
                                scope.Items(
                                    count: 20,
                                    key: it => it,
                                    content: it =>
                                    {
                                        Text(
                                            text: it.ToString(),
                                            color: Color.white,
                                            fontSize: 32.Sp(),
                                            textAlign: TextAlign.MiddleCenter,
                                            modifier: Modifier
                                                .Background(Color.red)
                                                .Size(100.Dp())
                                                .Margin(horizontal: 4.Dp())
                                                .OnClick(() => Debug.Log("Glick"))
                                                .OnClick(() => state.AnimateScrollToItem(it))
                                        );
                                    }
                                );
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
                                .OnClick(() => state.AnimateScrollToItem(0))
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
                                .OnClick(() => state.AnimateScrollToItem(19))
                        );
                    }
                );
            });
        }
    }
}