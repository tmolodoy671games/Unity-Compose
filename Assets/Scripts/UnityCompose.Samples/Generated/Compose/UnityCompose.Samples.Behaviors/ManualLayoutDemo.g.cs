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
            __composer.StartRestartGroup(651668197);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log(LocalTest.Current);
                CompositionLocalProvider(LocalTest.Provides("Custom1"), !__composer.RememberedKeyChanged<bool>(-1932153990, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Debug.Log(LocalTest.Current);
                    CompositionLocalProvider(LocalTest.Provides("Custom2"), !__composer.RememberedKeyChanged<bool>(-2069379763, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() => Debug.Log(LocalTest.Current)));
                }));
                Debug.Log(LocalTest.Current);
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

            __composer.EndRestartGroup(-155656874)?.UpdateScope(() => __MockPerformanceLayout());
        }

        [Composable]
        private static void __EmptyComposable()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-663790511);
            if (__composer.ShouldExecute(true))
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-663790511)?.UpdateScope(() => __EmptyComposable());
        }
    }
}