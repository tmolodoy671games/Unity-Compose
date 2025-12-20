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
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(107286556);
            var __isRestarted = __composer.IsRestarted();
            if (__composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(107286556, __isRestarted)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1172386894);
            var __isRestarted = __composer.IsRestarted();
            if (__composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1172386894, __isRestarted)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1517382754);
            var __isRestarted = __composer.IsRestarted();
            if (__composer.ShouldExecute())
            {
                var animationSpec = Tween(duration: 1f);
                Box(modifier: Modifier.FillMaxSize(), content: !__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Box(modifier: Modifier.FillMaxSize(), content: !__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        Navigation(coordinator: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>(new SampleCoordinatorImpl()), transition: !__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValue<System.Func<UnityCompose.ContentTransform>?>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.ContentTransform>?>(() => SlideInHorizontally(static it => -it).TogetherWith(SlideOutHorizontally(static it => it)).With(animationSpec)), initialScreens: !__composer.Changed() ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(IImmutableStableList.Create<ComposeScreen>(new FirstScreen())), modifier: Modifier.FillMaxSize());
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1517382754, __isRestarted)?.UpdateScope(() => __Layout());
        }
    }

    internal partial class FirstScreen
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-611690533);
            var __isRestarted = __composer.IsRestarted();
            if (__composer.ShouldExecute())
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(!__composer.Changed(coordinator) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => coordinator.ShowSecondScreen()));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.green), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Spacer(modifier: Modifier.Size(100).Background(Color.blue).Scale(1 + 2 * LocalTransitionProgress.Current));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-611690533, __isRestarted)?.UpdateScope(() => __Content());
        }
    }

    internal partial class SecondScreen
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1026872014);
            var __isRestarted = __composer.IsRestarted();
            if (__composer.ShouldExecute())
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(!__composer.Changed(coordinator) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => coordinator.ShowFirstScreen()));
                Spacer(modifier: Modifier.FillMaxSize().Background(Color.red));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1026872014, __isRestarted)?.UpdateScope(() => __Content());
        }
    }

    internal static partial class InputFunctions
    {
        [Composable]
        private static void __CollectSpace(Action onClick)
        {
            var __onClick = (onClick);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1188124018);
            var __isRestarted = __composer.IsRestarted();
            if (__composer.ShouldExecute(__onClick))
            {
                if (!IsActive)
                {
                    __composer.EndRestartGroup(1188124018, __isRestarted)?.UpdateScope(() => __CollectSpace(__onClick));
                    return;
                }

                LaunchedEffect(1, !__composer.Changed(onClick) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => CollectSpaceEnumerator(onClick)));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1188124018, __isRestarted)?.UpdateScope(() => __CollectSpace(__onClick));
        }
    }
}