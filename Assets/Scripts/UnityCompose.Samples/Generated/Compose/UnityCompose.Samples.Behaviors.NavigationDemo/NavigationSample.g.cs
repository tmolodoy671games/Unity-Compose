#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors.NavigationDemo
{
    internal partial class NavigationSample
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-232127417);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-232127417, __isRestarted)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(197824488);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(197824488, __isRestarted)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-782892601);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var animationSpec = Tween(duration: 1f);
                Box(modifier: Modifier.FillMaxSize(), content: !__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Box(modifier: Modifier.FillMaxSize(), content: !__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        Navigation(coordinator: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Samples.Behaviors.NavigationDemo.SampleCoordinatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.Samples.Behaviors.NavigationDemo.SampleCoordinatorImpl>(new SampleCoordinatorImpl()), transition: !__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValueAsStruct<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValueAsStruct<UnityCompose.ContentTransform>(FadeIn().TogetherWith(FadeOut()).With(animationSpec)), modifier: Modifier.FillMaxSize());
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-782892601, __isRestarted)?.UpdateScope(() => __Layout());
        }
    }
}