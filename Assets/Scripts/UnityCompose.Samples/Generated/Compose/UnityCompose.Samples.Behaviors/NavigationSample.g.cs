// ReSharper disable ArrangeNamespaceBody
using System.Collections;
using StableCollections;
using static UnityCompose.Samples.Behaviors.InputFunctions;
using Action = System.Action;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class NavigationSample
    {
        [Composable]
        [Compiled]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }

        [Composable]
        [Compiled]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Preview());
            }
        }

        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                var animationSpec = Tween(duration: 1f);
                Box(modifier: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>(animationSpec, () =>
                {
                    Box(modifier: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>(animationSpec, () =>
                    {
                        Navigation(coordinator: Remember(() => new SampleCoordinatorImpl()), transition: Remember<global::System.Func<global::UnityCompose.ContentTransform>>(animationSpec, () => (FadeIn(animationSpec: animationSpec) + SlideInHorizontally(it => -it, animationSpec: animationSpec)).TogetherWith(FadeOut(animationSpec: animationSpec) + SlideOutHorizontally(it => it, animationSpec: animationSpec))), initialScreens: Remember(() => IImmutableStableList.Create<ComposeScreen>(new FirstScreen())), modifier: Modifier.FillMaxSize());
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }

    internal partial class FirstScreen
    {
        [Composable]
        [Compiled]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(Remember<global::System.Action>(coordinator, () => coordinator.ShowSecondScreen()));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.green), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Spacer(modifier: Modifier.Size(100).Background(Color.blue).Scale(1 + 2 * LocalTransitionProgress.Current));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }
    }

    internal partial class SecondScreen
    {
        [Composable]
        [Compiled]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(Remember<global::System.Action>(coordinator, () => coordinator.ShowFirstScreen()));
                Spacer(modifier: Modifier.FillMaxSize().Background(Color.red));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }
    }

    internal static partial class InputFunctions
    {
    }
}