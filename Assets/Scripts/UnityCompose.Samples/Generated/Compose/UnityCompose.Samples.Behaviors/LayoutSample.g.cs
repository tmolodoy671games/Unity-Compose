#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class LayoutSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1104042731);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1104042731, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(625058520);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Column((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Spacer(modifier: Modifier.Size(100.Px()).Background(Color.yellow), __composer: __composer, __changed: 0b_00);
                    __Row((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __composer.StartReplaceGroup(1605829516);
                        for (var i = 0; i < 10; i++)
                        {
                            __Spacer(modifier: Modifier.Size(100.Px()).Background(Color.yellow), __composer: __composer, __changed: 0b_00);
                        }

                        __composer.EndReplaceGroup(1605829516);
                    })), __composer: __composer, __changed: 0b_01_01_01_00);
                })), __composer: __composer, __changed: 0b_01_01_01_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(625058520, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1492131254);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Box(modifier: Modifier.Background(Color.red).Size(400.Px()), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Column((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __Spacer(modifier: Modifier.Size(100.Px()).Float().Background(Color.yellow).Position(top: 5.Px()), __composer: __composer, __changed: 0b_00);
                            __Row((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                            {
                                __composer.StartReplaceGroup(1309801539);
                                for (var i = 0; i < 10; i++)
                                {
                                    __Spacer(modifier: Modifier.Size(100.Px()).Float().Background(Color.yellow).Position(top: 5.Px()), __composer: __composer, __changed: 0b_00);
                                }

                                __composer.EndReplaceGroup(1309801539);
                            })), __composer: __composer, __changed: 0b_01_01_01_00);
                        })), __composer: __composer, __changed: 0b_01_01_01_00);
                        __Spacer(modifier: Modifier.Size(100.Px()).Float().Background(Color.yellow).Position(bottom: 5.Px()), __composer: __composer, __changed: 0b_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1492131254, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}