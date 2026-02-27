#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompilerContent
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1237389833);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Foo(text: "bla".ToString(), modifier: @"Modifier
                    .OnMouseLeave(() => { })".ToString(), __composer: __composer, __changed: 0b_01_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1237389833, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        private static void __Foo(string text, int misc1 = -1, string? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__text, __misc1, __modifier) = (text, misc1, modifier);
            var __isCreated = __composer.StartRestartGroup(1926882667);
            var __dirty = __changed;
            if ((__changed & 0b_00_00_11) == 0)
                __dirty |= __composer.Changed(text) ? 0b_00_00_10 : 0b_00_00_01;
            if ((__changed & 0b_00_11_00) == 0)
                __dirty |= __composer.Changed(misc1) ? 0b_00_10_00 : 0b_00_01_00;
            if ((__changed & 0b_11_00_00) == 0)
                __dirty |= __composer.Changed(modifier) ? 0b_10_00_00 : 0b_01_00_00;
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __dirty = 0b_01_01_01;
            __composer.EndRestartGroup(1926882667, __isRestarted)?.UpdateScope(() => __Foo(__text, __misc1, __modifier, __composer, __composer.UpdateChangedFlags(__changed)));
        }
    }
}