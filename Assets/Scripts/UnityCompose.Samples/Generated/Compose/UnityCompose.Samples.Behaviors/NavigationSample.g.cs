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
            __composer.StartRestartGroup(553615033);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(553615033)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1988832801);
            if (__composer.ShouldExecute(true))
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1988832801)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-182273952);
            if (__composer.ShouldExecute(true))
            {
                var animationSpec = Tween(duration: 1f);
                Box(modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<UnityCompose.AnimationSpec>(-2003213279, animationSpec) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Box(modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<UnityCompose.AnimationSpec>(-1783280012, animationSpec) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        Navigation(coordinator: !__composer.RememberedKeyChanged<bool>(-1613637874, true) ? __composer.RememberedValue<UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.Samples.Behaviors.SampleCoordinatorImpl>(new SampleCoordinatorImpl()), transition: !__composer.RememberedKeyChanged<UnityCompose.AnimationSpec>(-2128946346, animationSpec) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.ContentTransform>?>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.ContentTransform>?>(() => SlideInHorizontally(static it => -it).TogetherWith(SlideOutHorizontally(static it => it)).With(animationSpec)), initialScreens: !__composer.RememberedKeyChanged<bool>(1965714662, true) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(IImmutableStableList.Create<ComposeScreen>(new FirstScreen())), modifier: Modifier.FillMaxSize());
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-182273952)?.UpdateScope(() => __Layout());
        }
    }

    internal partial class FirstScreen
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1719960415);
            if (__composer.ShouldExecute(true))
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(!__composer.RememberedKeyChanged<UnityCompose.Samples.Behaviors.ISampleCoordinator?>(-1827947330, coordinator) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => coordinator.ShowSecondScreen()));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.green), content: !__composer.RememberedKeyChanged<bool>(1552614955, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Spacer(modifier: Modifier.Size(100).Background(Color.blue).Scale(1 + 2 * LocalTransitionProgress.Current));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1719960415)?.UpdateScope(() => __Content());
        }
    }

    internal partial class SecondScreen
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1037156711);
            if (__composer.ShouldExecute(true))
            {
                var coordinator = FindCoordinator<ISampleCoordinator>();
                CollectSpace(!__composer.RememberedKeyChanged<UnityCompose.Samples.Behaviors.ISampleCoordinator?>(1025707352, coordinator) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => coordinator.ShowFirstScreen()));
                Spacer(modifier: Modifier.FillMaxSize().Background(Color.red));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1037156711)?.UpdateScope(() => __Content());
        }
    }

    internal static partial class InputFunctions
    {
        [Composable]
        private static void __CollectSpace(Action onClick)
        {
            var __onClick = (onClick);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1251363310);
            if (__composer.ShouldExecute(__onClick))
            {
                if (!IsActive)
                {
                    __composer.EndRestartGroup(-1251363310)?.UpdateScope(() => __CollectSpace(__onClick));
                    return;
                }

                LaunchedEffect(1, !__composer.RememberedKeyChanged<System.Action>(-1731916202, onClick) ? CurrentComposer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : CurrentComposer.UpdateLambda<System.Func<System.Collections.IEnumerator>>(() => CollectSpaceEnumerator(onClick)));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1251363310)?.UpdateScope(() => __CollectSpace(__onClick));
        }
    }
}