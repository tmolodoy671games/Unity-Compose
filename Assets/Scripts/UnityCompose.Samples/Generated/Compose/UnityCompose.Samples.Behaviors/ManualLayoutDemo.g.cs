using System;
using SharpExtensions;
using Sirenix.OdinInspector;
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
            __composer.StartRestartGroup(203612774);
            if (__composer.ShouldExecute())
            {
                _ = UpdateState.Value;
                __composer.StartReplaceGroup(977782000);
                if (SwitchState.Value)
                {
                    MockSpacer();
                }

                __composer.EndReplaceGroup(977782000);
                MockSpacer();
                __composer.StartReplaceGroup(1922131363);
                for (var i = 0; i < AddState.Value; i++)
                    MockSpacer();
                __composer.EndReplaceGroup(1922131363);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(203612774)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(578180728);
            if (__composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(578180728)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(642155314);
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

            __composer.EndRestartGroup(642155314)?.UpdateScope(() => __MockPerformanceLayout());
        }

        [Composable]
        private static void __EmptyComposable()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(957535468);
            if (__composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(957535468)?.UpdateScope(() => __EmptyComposable());
        }
    }
}