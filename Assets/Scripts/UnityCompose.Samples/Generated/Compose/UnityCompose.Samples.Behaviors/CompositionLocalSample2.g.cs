#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System.Drawing;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample2
    {
        protected void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(282645618);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(282645618, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        private void __Content()
        {
            __Content(CurrentComposer, 0b_10);
        }

        protected void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1107021593);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1107021593, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private void __Preview()
        {
            __Preview(CurrentComposer, 0b_10);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1170808269);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __CompositionLocalProvider(LocalTextStyle.Provides(new TextStyle(FontSize: 80, Color: Color.white)), (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    __Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        __Text(LocalDebugString.Current, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_00);
                        __CompositionLocalProvider(LocalDebugString.Provides("Nested"), (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            __Text(LocalDebugString.Current, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_00);
                            __CompositionLocalProvider(LocalDebugString.Provides("Super Nested"), (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                            {
                                __Text(LocalDebugString.Current, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_00);
                                __Text(LocalDebugString.Current, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_00);
                            })), __composer: __composer, __changed: 0);
                            __Text(LocalDebugString.Current, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_00);
                        })), __composer: __composer, __changed: 0);
                        __Text(LocalDebugString.Current, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_00);
                    })), __composer: __composer, __changed: 0);
                })), __composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1170808269, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer, 0b_10);
        }
    }
}