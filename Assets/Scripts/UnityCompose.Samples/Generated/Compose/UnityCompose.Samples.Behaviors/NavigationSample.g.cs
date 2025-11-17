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
        protected override void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Content());
            }
        }

        [Composable]
        protected override void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Preview());
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
                Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(animationSpec).Remember<Action>(__ => () =>
                {
                    Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(__.animationSpec).Remember<Action>(__ => () =>
                    {
                        Navigation(coordinator: Remember(static () => new SampleCoordinatorImpl()), transition: CurrentComposer.WithState(__.animationSpec).Remember<Func>(__ => () => ((FadeIn() + SlideInHorizontally(static it => -it)).TogetherWith(FadeOut() + SlideOutHorizontally(static it => it)).With(animationSpec))), initialScreens: Remember(static () => IImmutableStableList.Create<ComposeScreen>(new FirstScreen())), modifier: Modifier.FillMaxSize());
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }
    }

    internal partial class FirstScreen : ComposeScreen
    {
        [Composable]
        public override void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(CurrentComposer.WithState(coordinator).Remember<Action>(__ => () => coordinator.ShowSecondScreen()));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.green), content: static () =>
                {
                    Spacer(modifier: Modifier.Size(100).Background(Color.blue).Scale(1 + 2 * LocalTransitionProgress.Current));
                });
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Content());
            }
        }
    }

    internal partial class SecondScreen : ComposeScreen
    {
        [Composable]
        public override void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(CurrentComposer.WithState(coordinator).Remember<Action>(__ => () => coordinator.ShowFirstScreen()));
                Spacer(modifier: Modifier.FillMaxSize().Background(Color.red));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Content());
            }
        }
    }

    internal static partial class InputFunctions
    {
        [Composable, DontGenerateComposeGroups]
        public static void __CollectSpace(Action onClick)
        {
            if (!IsActive)
                return;
            LaunchedEffect(1, CurrentComposer.WithState(onClick).Remember<Func>(__ => () => CollectSpaceEnumerator(onClick)));
        }
    }
}