#nullable enable
using System;
using SharpExtensions;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ManualLayoutDemo
    {
        [Composable]
        private static void __MockLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-737530880);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                _ = UpdateState.Value;
                __composer.StartReplaceGroup(-30031941);
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

                __composer.EndReplaceGroup(-30031941);
                MockSpacer();
                __composer.StartReplaceGroup(2117597776);
                for (var i = 0; i < AddState.Value; i++)
                    MockSpacer();
                __composer.EndReplaceGroup(2117597776);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-737530880, __isRestarted)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockColumn(ComposableContent content)
        {
            var __content = (content);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1407509810);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute(__content))
            {
                content();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1407509810, __isRestarted)?.UpdateScope(() => __MockColumn(__content));
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1566028247);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                MockNestedSpacer();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1566028247, __isRestarted)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockNestedSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-129221247);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-129221247, __isRestarted)?.UpdateScope(() => __MockNestedSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1780842197);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var state = new object ();
                var time = TimeUtils.Measure(!__composer.Changed() ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                {
                    __composer.StartReplaceGroup(-2080460313);
                    for (var i = 0; i < 1_000_000; i++)
                    {
                        _ = !__composer.Changed() ? __composer.RememberedValue<object>() : __composer.UpdateRememberedValue<object>(new object ());
                    }

                    __composer.EndReplaceGroup(-2080460313);
                }));
                Debug.Log((int)time.TotalMilliseconds);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1780842197, __isRestarted)?.UpdateScope(() => __MockPerformanceLayout());
        }
    }
}