#nullable enable
// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using SharpExtensions;
using UnityEngine;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class ComposeFunctions
{
    public static void __ModalMenu(ComposableContent content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __content = (content);
        var __isCreated = __composer.StartRestartGroup(1669541423);
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10 : 0b_01;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            var manager = LocalOnScreenMenuManager.Current;
            __DisposableEffect(content, (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_11) == 0b_10).Changed<global::UnityCompose.ModalMenuManager>(manager!).Get() ? __composer.RememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>() : __composer.UpdateRememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>(it =>
            {
                manager.AddContent(content);
                return it.OnDispose(() => manager.RemoveContent(content));
            })), __composer: __composer, __changed: (__dirty & 0b_00_11));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(1669541423, __isRestarted)?.UpdateScope(() => __ModalMenu(__content, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}