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
            __composer.StartRestartGroup(-1468109432);
            if (__composer.ShouldExecute())
            {
                Debug.Log("MockLayout()");
                _ = UpdateState.Value;
                __composer.StartReplaceGroup(-318598308);
                if (SwitchState.Value)
                {
                    MockSpacer();
                }

                __composer.EndReplaceGroup(-318598308);
                __composer.StartReplaceGroup(-1440257290);
                for (var i = 0; i < AddState.Value; i++)
                    MockSpacer();
                __composer.EndReplaceGroup(-1440257290);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1468109432)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(86275340);
            if (__composer.ShouldExecute())
            {
                Debug.Log("MockSpacer()");
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(86275340)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(532808067);
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

            __composer.EndRestartGroup(532808067)?.UpdateScope(() => __MockPerformanceLayout());
        }

        [Composable]
        private static void __EmptyComposable()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1998145738);
            if (__composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1998145738)?.UpdateScope(() => __EmptyComposable());
        }
    }
}