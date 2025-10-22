// ReSharper disable ArrangeNamespaceBody

using System.Collections;
using StableCollections;
using UnityEngine.UIElements;
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
            Box(
                style: ComposeStyle.Empty
                    .Size(100.Percent()),
                content: () =>
                {
                    Box(
                        style: ComposeStyle.Empty
                            .Size(100.Percent()),
                        content: () =>
                        {
                            Navigation(
                                coordinator: Remember(() => new SampleCoordinatorImpl()),
                                transition: () => ContentTransform(
                                    enter: FadeIn() + SlideIn(SlideDirection.Left),
                                    exit: FadeOut() + SlideOut(SlideDirection.Left)
                                ),
                                transitionDuration: 5,
                                initialScreens: Remember(() =>
                                    IImmutableStableList.Create<ComposeScreen>(new FirstScreen())),
                                style: ComposeStyle.Empty
                                    .Size(100.Percent())
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
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: ComposeStyle.Empty
                    .Size(100.Percent())
                    .BackgroundColor(Color.green),
                content: () =>
                {
                    Spacer(
                        style: ComposeStyle.Empty
                            .Size(100)
                            .BackgroundColor(Color.blue)
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
                style: ComposeStyle.Empty
                    .Size(100.Percent())
                    .BackgroundColor(Color.red)
            );
        }
    }

    internal static partial class InputFunctions
    {
        [Composable, Compiled]
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