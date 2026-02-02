// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.NavigationDemo
{
    internal partial class NavigationSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            var animationSpec = Tween(
                duration: 1f
            );
            Box(
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Box(
                        modifier: Modifier
                            .FillMaxSize(),
                        content: () =>
                        {
                            Navigation(
                                coordinator: Remember(() => new SampleCoordinatorImpl()),
                                transition: Remember(animationSpec, () => FadeIn()
                                    .TogetherWith(FadeOut())
                                    .With(animationSpec)
                                ),
                                modifier: Modifier
                                    .FillMaxSize()
                            );
                        }
                    );
                }
            );
        }
    }
}