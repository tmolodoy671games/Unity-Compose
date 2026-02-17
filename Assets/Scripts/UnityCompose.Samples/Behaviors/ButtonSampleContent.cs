// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            Layout();
        }

        [Composable]
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
                    .FillMaxSize()
                    .Background(Color.white),
                content: () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    Box(
                        modifier: Modifier
                            .Padding(
                                horizontal: AnimateFloatAsState(isHovered.Value ? 80 : 40).Value.Px(),
                                vertical: 16.Px()
                            )
                            .Background(Color.blue)
                            .Border(radius: 16.Px())
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false),
                        content: () =>
                        {
                            CompositionLocalProvider(
                                LocalContentColor.Provides(Color.white),
                                content: () =>
                                {
                                    Text(
                                        text: "Click me",
                                        fontSize: 24
                                    );
                                }
                            );
                        }
                    );
                }
            );
        }
    }
}