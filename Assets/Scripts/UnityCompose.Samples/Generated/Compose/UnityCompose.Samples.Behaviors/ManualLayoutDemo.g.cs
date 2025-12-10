using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using StableCollections;
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
            __composer.StartRestartGroup(-2004217741);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log("MockLayout()");
                var _ = UpdateState.Value;
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2004217741)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(57639709);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log("MockSpacer()");
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(57639709)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(221741087);
            if (__composer.ShouldExecute(true))
            {
                var composer = CurrentComposer;
                var list = !__composer.RememberedKeyChanged<bool>(69566520, true) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create<CompositionLocalProvides>());
                var time = TimeUtils.Measure(!__composer.RememberedKeyChanged<bool>(920743208, true) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                {
                    for (var i = 0; i < 1_000_000; i++)
                    {
                        var _ = !__composer.RememberedKeyChanged<bool>(744441104, true) ? __composer.RememberedValue<int>() : __composer.UpdateRememberedValue<int>(1);
                    }
                }));
                Debug.Log((int)time.TotalMilliseconds);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(221741087)?.UpdateScope(() => __MockPerformanceLayout());
        }

        [Composable]
        private static void __EmptyComposable()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-593993807);
            if (__composer.ShouldExecute(true))
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-593993807)?.UpdateScope(() => __EmptyComposable());
        }
    }
}