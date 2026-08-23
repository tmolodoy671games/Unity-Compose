// ReSharper disable ArrangeNamespaceBody

using UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

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
                            var resumedScreen = Remember(() => new ResumedScreen());
                            var pausedScreen = Remember(() => new PausedScreen());
                            var isSwitched = Remember(() => MutableStateOf(false));
                            // AnimatedContent(
                            //     transitionSpec: _ => IEnterTransition.Empty().TogetherWith(Hide()),
                            //     // transitionSpec: it => FadeIn().TogetherWith(FadeOut()).With(animationSpec),
                            //     targetState: isSwitched.Value,
                            //     modifier: Modifier.FillMaxSize(),
                            //     content: (it, m) =>
                            //     {
                            //         ComposeScreen screen = it ? pausedScreen : resumedScreen;
                            //         screen.Content(m.OnClick(() => isSwitched.Value = !isSwitched.Value));
                            //     }
                            // );
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