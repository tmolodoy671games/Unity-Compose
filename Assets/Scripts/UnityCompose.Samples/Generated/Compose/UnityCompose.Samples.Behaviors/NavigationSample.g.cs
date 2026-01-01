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
            __composer.StartRestartGroup(-508664531);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-508664531, __isRestarted)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-2118074874);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2118074874, __isRestarted)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(597437411);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var animationSpec = Tween(duration: 1f);
                Box(modifier: Modifier.FillMaxSize(), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Box(modifier: Modifier.FillMaxSize(), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        Navigation(coordinator: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>(new SampleCoordinatorImpl()), // transition: () => SlideInHorizontally(static it => -it)
                        //     .TogetherWith(SlideOutHorizontally(static it => it))
                        //     .With(animationSpec),
                        initialScreens: !__composer.Changed() ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(IImmutableStableList.Create<ComposeScreen>(new FirstScreen())), modifier: Modifier.FillMaxSize());
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(597437411, __isRestarted)?.UpdateScope(() => __Layout());
        }
    }

    internal partial class FirstScreen
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-2083380366);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
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

            __composer.EndRestartGroup(-2083380366, __isRestarted)?.UpdateScope(() => __Content());
        }
    }

    internal partial class SecondScreen
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-2034319591);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(!__composer.Changed(coordinator) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => coordinator.ShowFirstScreen()));
                Spacer(modifier: Modifier.FillMaxSize().Background(Color.red));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2034319591, __isRestarted)?.UpdateScope(() => __Content());
        }
    }

    internal static partial class InputFunctions
    {
        [Composable]
        private static void __CollectSpace(Action onClick)
        {
            var __onClick = (onClick);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-356806667);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute(__onClick))
            {
                if (!IsActive)
                {
                    __composer.EndRestartGroup(-356806667, __isRestarted)?.UpdateScope(() => __CollectSpace(__onClick));
                    return;
                }

                LaunchedEffect(1, !__composer.Changed(onClick) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => CollectSpaceEnumerator(onClick)));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-356806667, __isRestarted)?.UpdateScope(() => __CollectSpace(__onClick));
        }
    }
}