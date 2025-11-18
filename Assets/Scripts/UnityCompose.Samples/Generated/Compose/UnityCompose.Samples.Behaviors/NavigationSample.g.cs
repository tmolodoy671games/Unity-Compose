using System.Collections;
using StableCollections;
using static UnityCompose.Samples.Behaviors.InputFunctions;
using Action = System.Action;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class NavigationSample : ComposeUI
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
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
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
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
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                var animationSpec = Tween(duration: 1f);
                Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(animationSpec).Remember<System.Action>(__ => () =>
                {
                    Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(animationSpec).Remember<System.Action>(__ => () =>
                    {
                        Navigation(coordinator: Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>>(__ => () => new SampleCoordinatorImpl())), transition: CurrentComposer.WithState(animationSpec).Remember<System.Func<UnityCompose.ContentTransform>?>(__ => () => SlideInHorizontally(static it => -it).TogetherWith(SlideOutHorizontally(static it => it)).With(animationSpec)), initialScreens: Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(__ => () => IImmutableStableList.Create<ComposeScreen>(new FirstScreen()))), modifier: Modifier.FillMaxSize());
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }

    internal partial class FirstScreen : ComposeScreen
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(CurrentComposer.WithState(coordinator).Remember<System.Action>(__ => () => coordinator.ShowSecondScreen()));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.green), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
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

    internal partial class SecondScreen : ComposeScreen
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(CurrentComposer.WithState(coordinator).Remember<System.Action>(__ => () => coordinator.ShowFirstScreen()));
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
        [Composable, DontGenerateComposeGroups]
        private static void __CollectSpace(Action onClick)
        {
            if (!IsActive)
                return;
            LaunchedEffect(1, CurrentComposer.WithState(onClick).Remember<System.Func<System.Collections.IEnumerator>>(__ => () => CollectSpaceEnumerator(onClick)));
        }
    }
}