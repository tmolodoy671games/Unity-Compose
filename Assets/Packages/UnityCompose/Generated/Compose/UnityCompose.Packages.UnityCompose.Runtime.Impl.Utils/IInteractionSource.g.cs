#nullable enable
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
public static partial class InteractionSourceExtensions
{
    public static IState<bool> __CollectIsHoveredAsState(this IInteractionSource interactionSource, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(interactionSource) ? 0b_10 : 0b_01;
        var isHovered = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
        __DisposableEffect((interactionSource, isHovered), (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_11) == 0b_10).Changed<global::UnityCompose.IMutableState<bool>>(isHovered!).Get() ? __composer.RememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>() : __composer.UpdateRememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>(scope =>
        {
            var disposable = interactionSource.Interactions.Collect(it => isHovered.Value = it switch
            {
                IHoverInteraction.Enter => true,
                IHoverInteraction.Exit => false,
                _ => isHovered.Value
            });
            return scope.OnDispose(disposable.Dispose);
        })), __composer: __composer, __changed: 0b_00_00);
        return isHovered;
    }

    public static IState<bool> __CollectIsFocusedAsState(this IInteractionSource interactionSource, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(interactionSource) ? 0b_10 : 0b_01;
        var isFocused = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
        __DisposableEffect((interactionSource, isFocused), (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_11) == 0b_10).Changed<global::UnityCompose.IMutableState<bool>>(isFocused!).Get() ? __composer.RememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>() : __composer.UpdateRememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>(scope =>
        {
            var disposable = interactionSource.Interactions.Collect(it => isFocused.Value = it switch
            {
                IFocusInteraction.Focus => true,
                IFocusInteraction.Unfocus => false,
                _ => isFocused.Value
            });
            return scope.OnDispose(disposable.Dispose);
        })), __composer: __composer, __changed: 0b_00_00);
        return isFocused;
    }

    public static IState<bool> __CollectIsPressedAsState(this IInteractionSource interactionSource, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(interactionSource) ? 0b_10 : 0b_01;
        var isPressed = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
        __DisposableEffect((interactionSource, isPressed), (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_11) == 0b_10).Changed<global::UnityCompose.IMutableState<bool>>(isPressed!).Get() ? __composer.RememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>() : __composer.UpdateRememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>(scope =>
        {
            var disposable = interactionSource.Interactions.Collect(it => isPressed.Value = it switch
            {
                IPressInteraction.Press => true,
                IPressInteraction.Release => false,
                IPressInteraction.Cancel => false,
                _ => isPressed.Value
            });
            return scope.OnDispose(disposable.Dispose);
        })), __composer: __composer, __changed: 0b_00_00);
        return isPressed;
    }
}