// ReSharper disable ArrangeNamespaceBody
using System.Collections;
using StableCollections;
using UnityEngine.UIElements;
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
                Box(style: ComposeStyle.Empty.Size(100.Percent()), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Box(style: ComposeStyle.Empty.Size(100.Percent()), content: RememberComposable<global::System.Action>(null, () =>
                    {
                        Navigation(coordinator: Remember(() => new SampleCoordinatorImpl()), transition: Remember<global::System.Func<global::UnityCompose.ContentTransform>>(null, () => ContentTransform(enter: FadeIn() + SlideIn(SlideDirection.Left), exit: FadeOut() + SlideOut(SlideDirection.Left))), transitionDuration: 5, initialScreens: Remember(() => IImmutableStableList.Create<ComposeScreen>(new FirstScreen())), style: ComposeStyle.Empty.Size(100.Percent()));
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
                Box(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: ComposeStyle.Empty.Size(100.Percent()).BackgroundColor(Color.green), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Spacer(style: ComposeStyle.Empty.Size(100).BackgroundColor(Color.blue).Scale(1 + 2 * LocalTransitionProgress.Current));
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
                Spacer(style: ComposeStyle.Empty.Size(100.Percent()).BackgroundColor(Color.red));
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