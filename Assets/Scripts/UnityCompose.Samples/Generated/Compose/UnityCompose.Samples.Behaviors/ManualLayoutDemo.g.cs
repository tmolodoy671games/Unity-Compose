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
            __composer.StartRestartGroup(-1118934111);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log(LocalTest.Current);
                CompositionLocalProvider(LocalTest.Provides("Custom1"), !__composer.RememberedKeyChanged<bool>(-100341517, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Debug.Log(LocalTest.Current);
                    CompositionLocalProvider(LocalTest.Provides("Custom2"), !__composer.RememberedKeyChanged<bool>(888977492, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() => Debug.Log(LocalTest.Current)));
                }));
                Debug.Log(LocalTest.Current);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1118934111)?.UpdateScope(() => __MockLayout());
        }

        [Composable]
        private static void __MockSpacer()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-849844547);
            if (__composer.ShouldExecute(true))
            {
                Debug.Log("MockSpacer()");
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-849844547)?.UpdateScope(() => __MockSpacer());
        }

        [Composable]
        private static void __MockPerformanceLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-12567558);
            if (__composer.ShouldExecute(true))
            {
                var composer = CurrentComposer;
                var list = !__composer.RememberedKeyChanged<bool>(-616727790, true) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create<CompositionLocalProvides>());
                var time = TimeUtils.Measure(!__composer.RememberedKeyChanged<bool>(724559022, true) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                {
                    for (var i = 0; i < 1_000_000; i++)
                    {
                        var _ = !__composer.RememberedKeyChanged<bool>(-1758524372, true) ? __composer.RememberedValue<int>() : __composer.UpdateRememberedValue<int>(1);
                    }
                }));
                Debug.Log((int)time.TotalMilliseconds);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-12567558)?.UpdateScope(() => __MockPerformanceLayout());
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