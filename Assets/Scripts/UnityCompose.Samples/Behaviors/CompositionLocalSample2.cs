// ReSharper disable ArrangeNamespaceBody

using System.Drawing;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample2 : ComposeUI
    {
        private static readonly ICompositionLocal<string> LocalDebugString = CompositionLocalOf(() => "Default");

        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            CompositionLocalProvider(
                LocalTextStyle.Provides(
                    new TextStyle(
                        FontSize: 80,
                        Color: Color.white
                    )
                ),
                () =>
                {
                    Column(
                        horizontalAlignment: Alignment.Horizontal.Center,
                        verticalAlignment: Alignment.Vertical.Center,
                        modifier: Modifier.FillMaxSize(),
                        content: () =>
                        {
                            Text(LocalDebugString.Current);
                            CompositionLocalProvider(
                                LocalDebugString.Provides("Nested"),
                                () =>
                                {
                                    Text(LocalDebugString.Current);
                                    CompositionLocalProvider(
                                        LocalDebugString.Provides("Super Nested"),
                                        () =>
                                        {
                                            Text(LocalDebugString.Current);
                                            Text(LocalDebugString.Current);
                                        });
                                    Text(LocalDebugString.Current);
                                }
                            );
                            Text(LocalDebugString.Current);
                        }
                    );
                }
            );
        }
    }
}