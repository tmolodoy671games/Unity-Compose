#nullable enable
// ReSharper disable CheckNamespace

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    public static void __WithIsActive(bool active, ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__active, __content) = (active, content);
        var __isCreated = __composer.StartRestartGroup(1457337593);
        var __dirty = __changed;
        if ((__changed & 0b_00_11) == 0)
            __dirty |= __composer.Changed(active) ? 0b_00_10 : 0b_00_01;
        if ((__changed & 0b_11_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00 : 0b_01_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            var isActiveInstance = LocalIsActive.Current;
            var newIsActiveInstance = (!__composer.Changed<(global::UnityCompose.IsActiveEntry isActiveInstance, bool active)>((isActiveInstance, active)!) ? __composer.RememberedValue<global::UnityCompose.IsActiveEntry>() : __composer.UpdateRememberedValue<global::UnityCompose.IsActiveEntry>(new IsActiveEntry(IsActiveSelf: active, Parent: isActiveInstance)));
            __CompositionLocalProvider(LocalIsActive.Provides(newIsActiveInstance), content, __composer: __composer, __changed: (__dirty & 0b_11_00));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01;
        __composer.EndRestartGroup(1457337593, __isRestarted)?.UpdateScope(() => __WithIsActive(__active, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}