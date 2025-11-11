// ReSharper disable ArrangeNamespaceBody

using System.Collections;
using StableCollections;
using static UnityCompose.Samples.Behaviors.InputFunctions;
using Action = System.Action;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class NavigationSample : ComposeUI
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
                                transition: () =>
                                (
                                    (FadeIn() + SlideInHorizontally(it => -it))
                                    .TogetherWith(FadeOut() + SlideOutHorizontally(it => it))
                                    .With(animationSpec)
                                ),
                                initialScreens: Remember(() =>
                                    IImmutableStableList.Create<ComposeScreen>(new FirstScreen())),
                                modifier: Modifier
                                    .FillMaxSize()
                            );
                        }
                    );
                }
            );
        }
    }

    internal interface ISampleCoordinator
    {
        void ShowSecondScreen();
        void ShowFirstScreen();
    }

    internal class SampleCoordinatorImpl : BaseComposeCoordinator, ISampleCoordinator
    {
        public void ShowSecondScreen()
        {
            Router.ReplaceScreen(new SecondScreen());
        }

        public void ShowFirstScreen()
        {
            Router.Exit();
        }
    }

    internal partial class FirstScreen : ComposeScreen
    {
        [Composable]
        public override void Content()
        {
            var coordinator = FindCoordinator<ISampleCoordinator>();
            CollectSpace(() => coordinator.ShowSecondScreen());
            Box(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .FillMaxSize()
                    .Background(Color.green),
                content: () =>
                {
                    Spacer(
                        modifier: Modifier
                            .Size(100)
                            .Background(Color.blue)
                            .Scale(1 + 2 * LocalTransitionProgress.Current)
                    );
                }
            );
        }
    }

    internal partial class SecondScreen : ComposeScreen
    {
        [Composable]
        public override void Content()
        {
            var coordinator = FindCoordinator<ISampleCoordinator>();
            CollectSpace(() => coordinator.ShowFirstScreen());
            Spacer(
                modifier: Modifier
                    .FillMaxSize()
                    .Background(Color.red)
            );
        }
    }

    internal static partial class InputFunctions
    {
        [Composable, DontGenerateComposeGroups]
        public static void CollectSpace(Action onClick)
        {
            if (!IsActive)
                return;
            LaunchedEffect(1, CollectSpaceEnumerator(onClick));
        }

        private static IEnumerator CollectSpaceEnumerator(Action onClick)
        {
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Space))
                    onClick();
            }
        }
    }
}