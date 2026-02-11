#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System.Drawing;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample2
    {
        private void __Content(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-282645618);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-282645618, __isRestarted)?.UpdateScope(() => __Content());
        }

        private void __Content()
        {
            __Content(CurrentComposer);
        }

        private void __Preview(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-1107021593);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1107021593, __isRestarted)?.UpdateScope(() => __Preview());
        }

        private void __Preview()
        {
            __Preview(CurrentComposer);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-1170808269);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                CompositionLocalProvider(LocalTextStyle.Provides(new TextStyle(FontSize: 80, Color: Color.white)), (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        Text(LocalDebugString.Current);
                        CompositionLocalProvider(LocalDebugString.Provides("Nested"), (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            Text(LocalDebugString.Current);
                            CompositionLocalProvider(LocalDebugString.Provides("Super Nested"), (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                            {
                                Text(LocalDebugString.Current);
                                Text(LocalDebugString.Current);
                            })));
                            Text(LocalDebugString.Current);
                        })));
                        Text(LocalDebugString.Current);
                    })));
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1170808269, __isRestarted)?.UpdateScope(() => __Layout());
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer);
        }
    }
}