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
            __composer.StartRestartGroup(-520453920);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                _ = UpdateState.Value;
                __composer.StartReplaceGroup(-1858719709);
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

                __composer.EndReplaceGroup(-1858719709);
                MockSpacer();
                __composer.StartReplaceGroup(2002871028);
                for (var i = 0; i < AddState.Value; i++)
                    MockSpacer();
                __composer.EndReplaceGroup(2002871028);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-520453920, __isRestarted)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockColumn(ComposableContent content)
        {
            var __content = (content);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1639483258);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute(__content))
            {
                content();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1639483258, __isRestarted)?.UpdateScope(() => __MockColumn(__content));
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-349813030);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                MockNestedSpacer();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-349813030, __isRestarted)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockNestedSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1814819186);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1814819186, __isRestarted)?.UpdateScope(() => __MockNestedSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1520812552);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var state = new object ();
                var time = TimeUtils.Measure(!__composer.Changed() ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                {
                    __composer.StartReplaceGroup(785465272);
                    for (var i = 0; i < 1_000_000; i++)
                    {
                        _ = !__composer.Changed() ? __composer.RememberedValue<object>() : __composer.UpdateRememberedValue<object>(new object ());
                    }

                    __composer.EndReplaceGroup(785465272);
                }));
                Debug.Log((int)time.TotalMilliseconds);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1520812552, __isRestarted)?.UpdateScope(() => __MockPerformanceLayout());
        }
    }
}