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
            __composer.StartRestartGroup(651668197);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log("MockLayout()");
                var _ = UpdateState.Value;
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(651668197)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(609241029);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log("MockSpacer()");
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(609241029)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-155656874);
            if (__composer.ShouldExecute(true))
            {
                var composer = CurrentComposer;
                var list = !__composer.RememberedKeyChanged<bool>(1980987032, true) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create<CompositionLocalProvides>());
                var state = new object ();
                var time = TimeUtils.Measure(!__composer.RememberedKeyChanged<object?>(724559022, state) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                {
                    for (var i = 0; i < 1_000_000; i++)
                    {
                        var _ = !__composer.RememberedKeyChanged<bool>(-1758524372, true) ? __composer.RememberedValue<object>() : __composer.UpdateRememberedValue<object>(state);
                    }
                }));
                Debug.Log((int)time.TotalMilliseconds);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-155656874)?.UpdateScope(() => __MockPerformanceLayout());
        }

        [Composable]
        private static void __EmptyComposable()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-2119305355);
            if (__composer.ShouldExecute(true))
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2119305355)?.UpdateScope(() => __EmptyComposable());
        }
    }
}