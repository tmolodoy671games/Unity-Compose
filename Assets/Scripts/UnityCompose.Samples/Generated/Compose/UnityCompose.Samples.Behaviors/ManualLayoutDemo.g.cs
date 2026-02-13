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
        private static void __MockLayout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(737530880);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                _ = UpdateState.Value;
                __composer.StartReplaceGroup(30031941);
                if (SwitchState.Value)
                {
                    // MockColumn(() =>
                    // {
                    __MockSpacer(__composer: __composer, __changed: 0);
                    __MockSpacer(__composer: __composer, __changed: 0);
                    __MockSpacer(__composer: __composer, __changed: 0);
                    __MockSpacer(__composer: __composer, __changed: 0);
                    __MockSpacer(__composer: __composer, __changed: 0);
                // });
                }

                __composer.EndReplaceGroup(30031941);
                __MockSpacer(__composer: __composer, __changed: 0);
                __composer.StartReplaceGroup(2117597776);
                for (var i = 0; i < AddState.Value; i++)
                    __MockSpacer(__composer: __composer, __changed: 0);
                __composer.EndReplaceGroup(2117597776);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(737530880, __isRestarted)?.UpdateScope(() => __MockLayout(__composer, 0));
        }

        private static void __MockColumn(ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __content = (content);
            var __isCreated = __composer.StartRestartGroup(1407509810);
            var __dirty = __changed;
            var __dirtyRestart = 0;
            if ((__changed & 0b_11) == 0)
            {
                __dirty |= __composer.Changed(content) ? 0b_10 : 0b_01;
            }
            else
            {
                __dirtyRestart |= 0b_01;
            }

            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01)
            {
                content();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1407509810, __isRestarted)?.UpdateScope(() => __MockColumn(__content, __composer, __dirtyRestart));
        }

        private static void __MockSpacer(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1566028247);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __MockNestedSpacer(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1566028247, __isRestarted)?.UpdateScope(() => __MockSpacer(__composer, 0));
        }

        private static void __MockNestedSpacer(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(129221247);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(129221247, __isRestarted)?.UpdateScope(() => __MockNestedSpacer(__composer, 0));
        }

        private static void __MockPerformanceLayout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1780842197);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var state = new object ();
                var time = TimeUtils.Measure((!__composer.Changed() ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                {
                    __composer.StartReplaceGroup(2080460313);
                    for (var i = 0; i < 1_000_000; i++)
                    {
                        _ = (!__composer.Changed() ? __composer.RememberedValue<object>() : __composer.UpdateRememberedValue<object>(new object ()));
                    }

                    __composer.EndReplaceGroup(2080460313);
                })));
                Debug.Log((int)time.TotalMilliseconds);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1780842197, __isRestarted)?.UpdateScope(() => __MockPerformanceLayout(__composer, 0));
        }
    }
}