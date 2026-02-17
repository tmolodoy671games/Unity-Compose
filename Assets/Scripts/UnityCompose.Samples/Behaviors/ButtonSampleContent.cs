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
                    Foo(isHovered.Value);
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

        [Composable]
        private static void Foo(bool param)
        {
            Bar(param, AnimateFloatAsState(param ? 1 : 0).Value);
        }

        [Composable]
        private static void Bar(bool param, float param2)
        {
            // Debug.Log(param);
            LaunchedEffect(param, () => Debug.Log(param));
        }
    }
}