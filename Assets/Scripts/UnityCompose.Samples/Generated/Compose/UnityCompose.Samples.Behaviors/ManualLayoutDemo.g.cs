using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ManualLayoutDemo
    {
        [Composable]
        private static void __MockLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-940293073);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                _ = UpdateState.Value;
                __composer.StartReplaceGroup(-1989253727);
                if (SwitchState.Value)
                {
                    // MockColumn(() =>
                    // {
                    MockSpacer();
                    MockSpacer();
                    MockSpacer();
                    MockSpacer();
                    MockSpacer();
                // });
                }

                __composer.EndReplaceGroup(-1989253727);
                MockSpacer();
                __composer.StartReplaceGroup(1936930316);
                for (var i = 0; i < AddState.Value; i++)
                    MockSpacer();
                __composer.EndReplaceGroup(1936930316);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-940293073, __isRestarted)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockColumn(ComposableContent content)
        {
            var __content = (content);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1399840978);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute(__content))
            {
                content();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1399840978, __isRestarted)?.UpdateScope(() => __MockColumn(__content));
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1155614382);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                MockNestedSpacer();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1155614382, __isRestarted)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockNestedSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1371155226);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1371155226, __isRestarted)?.UpdateScope(() => __MockNestedSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(678665084);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var state = new object ();
                var time = TimeUtils.Measure(!__composer.Changed() ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                {
                    __composer.StartReplaceGroup(-1320220082);
                    for (var i = 0; i < 1_000_000; i++)
                    {
                        _ = !__composer.Changed() ? __composer.RememberedValue<object>() : __composer.UpdateRememberedValue<object>(new object ());
                    }

                    __composer.EndReplaceGroup(-1320220082);
                }));
                Debug.Log((int)time.TotalMilliseconds);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(678665084, __isRestarted)?.UpdateScope(() => __MockPerformanceLayout());
        }
    }
}