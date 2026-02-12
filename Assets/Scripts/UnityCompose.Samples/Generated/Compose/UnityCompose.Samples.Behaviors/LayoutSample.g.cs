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
        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(997071719);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    __Box(modifier: Modifier.Background(Color.red).Size(400.Px()), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        __Spacer(modifier: Modifier.Size(100.Px()).Float().Background(Color.yellow).Position(top: 5.Px()), __composer: __composer, __changed: 0);
                        __Spacer(modifier: Modifier.Size(100.Px()).Float().Background(Color.yellow).Position(bottom: 5.Px()), __composer: __composer, __changed: 0);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(997071719, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer, 0b_10);
        }
    }
}