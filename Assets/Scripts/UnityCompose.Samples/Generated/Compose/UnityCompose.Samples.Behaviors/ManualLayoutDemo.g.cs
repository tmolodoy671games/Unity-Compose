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
            __composer.StartRestartGroup(-1926847197);
            if (__composer.ShouldExecute())
            {
                _ = UpdateState.Value;
                __composer.StartReplaceGroup(-574372599);
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

                __composer.EndReplaceGroup(-574372599);
                MockSpacer();
                __composer.StartReplaceGroup(325963036);
                for (var i = 0; i < AddState.Value; i++)
                    MockSpacer();
                __composer.EndReplaceGroup(325963036);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1926847197)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockColumn(ComposableContent content)
        {
            var __content = (content);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(737051444);
            if (__composer.ShouldExecute(__content))
            {
                content();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(737051444)?.UpdateScope(() => __MockColumn(__content));
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(178813868);
            if (__composer.ShouldExecute())
            {
                MockNestedSpacer();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(178813868)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockNestedSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(920743208);
            if (__composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(920743208)?.UpdateScope(() => __MockNestedSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-593993807);
            if (__composer.ShouldExecute())
            {
                var state = new object ();
                var time = TimeUtils.Measure(!__composer.Changed() ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                {
                    for (var i = 0; i < 1_000_000; i++)
                    {
                        _ = !__composer.Changed() ? __composer.RememberedValue<object>() : __composer.UpdateRememberedValue<object>(new object ());
                    }
                }));
                Debug.Log((int)time.TotalMilliseconds);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-593993807)?.UpdateScope(() => __MockPerformanceLayout());
        }
    }
}