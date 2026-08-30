#nullable enable
// ReSharper disable CheckNamespace

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
internal partial class DrawBeforeModifierImpl
{
    public override void __DrawBefore(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(642055633);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
            _content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(642055633, __isRestarted)?.UpdateScope(() => __DrawBefore(__composer, 0));
    }
}

internal partial class DrawAfterModifierImpl
{
    public override void __DrawAfter(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(85320007);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
            _content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(85320007, __isRestarted)?.UpdateScope(() => __DrawAfter(__composer, 0));
    }
}