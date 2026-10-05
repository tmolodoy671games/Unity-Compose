using System.Threading;
using Compose.Net;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class LazyRowSample : ComposeUI
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
                    var state = RememberLazyListState();
                    LazyRow(
                        state: state,
                        modifier: Modifier
                            .WidthIn(max: 700.Dp())
                            .Background(Color.white.ToSystemColor()),
                        content: scope =>
                        {
                            // Scroll
                            scope.Items(100, it =>
                            {
                                Text(
                                    $"Bla {it}",
                                    fontSize: 64.Sp(),
                                    modifier: Modifier
                                        .OnClick(() => state.AnimateScrollToItem(it, CancellationToken.None))
                                );
                            });
                        }
                    );
                }
            );
        }
    }
}