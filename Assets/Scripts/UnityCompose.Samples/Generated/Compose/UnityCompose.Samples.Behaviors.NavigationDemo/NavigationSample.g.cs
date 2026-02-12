#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors.NavigationDemo
{
    internal partial class NavigationSample
    {
        protected void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(232127417);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(232127417, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        private void __Content()
        {
            __Content(CurrentComposer, 0b_10);
        }

        protected void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1086179851);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1086179851, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private void __Preview()
        {
            __Preview(CurrentComposer, 0b_10);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(197824488);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var animationSpec = Tween(duration: 1f);
                __Box(modifier: Modifier.FillMaxSize(), content: (!__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    __Box(modifier: Modifier.FillMaxSize(), content: (!__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        __Navigation(coordinator: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.Samples.Behaviors.NavigationDemo.SampleCoordinatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.Samples.Behaviors.NavigationDemo.SampleCoordinatorImpl>(new SampleCoordinatorImpl())), transition: (!__composer.ChangedAsStruct(animationSpec) ? __composer.RememberedValueAsStruct<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValueAsStruct<UnityCompose.ContentTransform>(FadeIn().TogetherWith(FadeOut()).With(animationSpec))), modifier: Modifier.FillMaxSize(), __composer: __composer, __changed: 0);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_01_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(197824488, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer, 0b_10);
        }
    }
}