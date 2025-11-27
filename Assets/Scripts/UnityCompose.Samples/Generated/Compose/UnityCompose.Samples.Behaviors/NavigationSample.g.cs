using System.Collections;
using StableCollections;
using static UnityCompose.Samples.Behaviors.InputFunctions;
using Action = System.Action;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class NavigationSample
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(107286556, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(107386556, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }

        [Composable]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(1172386894, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1172486894, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Preview()));
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(1517382754, true))
                return;
            try
            {
                var animationSpec = Tween(duration: 1f);
                Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<UnityCompose.AnimationSpec, UnityCompose.ComposableContent>(500783218, animationSpec) ? CurrentComposer.RememberedValue<UnityCompose.AnimationSpec, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.AnimationSpec, UnityCompose.ComposableContent>(() =>
                {
                    Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<UnityCompose.AnimationSpec, UnityCompose.ComposableContent>(1327091946, animationSpec) ? CurrentComposer.RememberedValue<UnityCompose.AnimationSpec, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.AnimationSpec, UnityCompose.ComposableContent>(() =>
                    {
                        Navigation(coordinator: CurrentComposer.HasRememberedValue<bool, UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>(-991672865, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>() : CurrentComposer.WriteValue<bool, UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>(() => new SampleCoordinatorImpl()), transition: CurrentComposer.HasRememberedValue<UnityCompose.AnimationSpec, System.Func<UnityCompose.ContentTransform>?>(-1187911465, animationSpec) ? CurrentComposer.RememberedValue<UnityCompose.AnimationSpec, System.Func<UnityCompose.ContentTransform>?>() : CurrentComposer.WriteLambda<UnityCompose.AnimationSpec, System.Func<UnityCompose.ContentTransform>?>(() => SlideInHorizontally(static it => -it).TogetherWith(SlideOutHorizontally(static it => it)).With(animationSpec)), initialScreens: CurrentComposer.HasRememberedValue<bool, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(-1831452740, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : CurrentComposer.WriteValue<bool, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(() => IImmutableStableList.Create<ComposeScreen>(new FirstScreen())), modifier: Modifier.FillMaxSize());
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1517482754, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }
    }

    internal partial class FirstScreen
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(-611690533, true))
                return;
            try
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(CurrentComposer.HasRememberedValue<UnityCompose.Samples.Behaviors.ISampleCoordinator?, System.Action>(-1455542132, coordinator) ? CurrentComposer.RememberedValue<UnityCompose.Samples.Behaviors.ISampleCoordinator?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.Samples.Behaviors.ISampleCoordinator?, System.Action>(() => coordinator.ShowSecondScreen()));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.green), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-1889230133, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    Spacer(modifier: Modifier.Size(100).Background(Color.blue).Scale(1 + 2 * LocalTransitionProgress.Current));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-611590533, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }
    }

    internal partial class SecondScreen
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(1026872014, true))
                return;
            try
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(CurrentComposer.HasRememberedValue<UnityCompose.Samples.Behaviors.ISampleCoordinator?, System.Action>(2051935752, coordinator) ? CurrentComposer.RememberedValue<UnityCompose.Samples.Behaviors.ISampleCoordinator?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.Samples.Behaviors.ISampleCoordinator?, System.Action>(() => coordinator.ShowFirstScreen()));
                Spacer(modifier: Modifier.FillMaxSize().Background(Color.red));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1026972014, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
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
            LaunchedEffect(1, CurrentComposer.HasRememberedValue<System.Action, System.Func<System.Collections.IEnumerator>>(-945086192, onClick) ? CurrentComposer.RememberedValue<System.Action, System.Func<System.Collections.IEnumerator>>() : CurrentComposer.WriteLambda<System.Action, System.Func<System.Collections.IEnumerator>>(() => CollectSpaceEnumerator(onClick)));
        }
    }
}