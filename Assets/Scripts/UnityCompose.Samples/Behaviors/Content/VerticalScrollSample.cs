// ReSharper disable ArrangeNamespaceBody

using Compose.Net;

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class VerticalScrollSample : ComposeUI
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
                    var scrollState = RememberScrollState();
                    Column(
                        modifier: Modifier
                            .Height(700.Dp())
                            .Background(Color.white.ToSystemColor())
                            .VerticalScroll(scrollState),
                        content: () =>
                        {
                            for (var i = 0; i < 100; i++)
                            {
                                Text($"Bla {i}", fontSize: 64.Sp());
                            }
                        }
                    );
                }
            );
        }
    }
}