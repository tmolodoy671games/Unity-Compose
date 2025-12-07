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
            __composer.StartRestartGroup(-1468188606);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log("MockLayout()");
                var _ = UpdateState.Value;
                MockSpacer();
                __composer.StartReplaceGroup(-975490194);
                if (SwitchState.Value)
                {
                    MockSpacer();
                }

                __composer.EndReplaceGroup(-975490194);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1468188606)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-380519672);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log("MockSpacer()");
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-380519672)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(678906330);
            if (__composer.ShouldExecute(true))
            {
                __composer.StartReplaceGroup(847579505);
                for (var i = 0; i < 1_000_000; i++)
                {
                    EmptyComposable();
                }

                __composer.EndReplaceGroup(847579505);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(678906330)?.UpdateScope(() => __MockPerformanceLayout());
        }

        [Composable]
        private static void __EmptyComposable()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-2069379763);
            if (__composer.ShouldExecute(true))
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2069379763)?.UpdateScope(() => __EmptyComposable());
        }
    }
}