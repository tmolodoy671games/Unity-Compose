#nullable enable
using System;
using SharpExtensions;
using UnityCompose;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UI.DesignSystem.Compose;
public static partial class DesignSystemComposeFunctions
{
    public static void __DsClickIndication(ComposableContent<DsClickIndicationScope> content, bool hovered, Action onHover, Action onLeave, Optional<Color> rippleColor = default, Optional<Color> hoverColor = default, Optional<AnimationSpec> animationSpec = default, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __hovered, __onHover, __onLeave, __rippleColor, __hoverColor, __animationSpec, __modifier) = (content, hovered, onHover, onLeave, rippleColor, hoverColor, animationSpec, modifier);
        var __isCreated = __composer.StartRestartGroup(1316622409);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_00_00_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_00_00_00_00_10 : 0b_00_00_00_00_00_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_00_00_00_01;
        }

        if ((__changed & 0b_00_00_00_00_00_00_11_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(hovered) ? 0b_00_00_00_00_00_00_10_00 : 0b_00_00_00_00_00_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_00_00_01_00;
        }

        if ((__changed & 0b_00_00_00_00_00_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(onHover) ? 0b_00_00_00_00_00_10_00_00 : 0b_00_00_00_00_00_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_00_01_00_00;
        }

        if ((__changed & 0b_00_00_00_00_11_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(onLeave) ? 0b_00_00_00_00_10_00_00_00 : 0b_00_00_00_00_01_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_01_00_00_00;
        }

        if ((__changed & 0b_00_00_00_11_00_00_00_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(rippleColor) ? 0b_00_00_00_10_00_00_00_00 : 0b_00_00_00_01_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_01_00_00_00_00;
        }

        if ((__changed & 0b_00_00_11_00_00_00_00_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(hoverColor) ? 0b_00_00_10_00_00_00_00_00 : 0b_00_00_01_00_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01_00_00_00_00_00;
        }

        if ((__changed & 0b_00_11_00_00_00_00_00_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(animationSpec) ? 0b_00_10_00_00_00_00_00_00 : 0b_00_01_00_00_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00_00_00_00_00_00;
        }

        if ((__changed & 0b_11_00_00_00_00_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00_00_00_00 : 0b_01_00_00_00_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00_00_00_00_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01_01_01)
        {
            var resolvedHoverColor = hoverColor.GetOrDefault(Color.white.With(a: 0.1f));
            var resolvedPressColor = rippleColor.GetOrDefault(Color.black.With(a: 0.75f));
            var layout = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityCompose.LayoutCoordinates>>>(MutableStateOf(Optional.Empty<LayoutCoordinates>())));
            var isPressed = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<bool>>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<bool>>>(MutableStateOf(Optional.Empty<bool>())));
            __Box(modifier: modifier.OrEmpty().Clip().OnGloballyPositioned((!__composer.Changed(layout) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => layout.Value = it))).OnMouseEnter((!__composer.Changed(onHover) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => onHover()))).OnMouseLeave((!__composer.ChangedAsStruct((onLeave, isPressed)) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
            {
                onLeave();
                isPressed.Value = false;
            }))).OnMouseDown((!__composer.Changed(isPressed) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isPressed.Value = true))).OnMouseUp((!__composer.Changed(isPressed) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isPressed.Value = false))), content: (!__composer.ChangedAsStruct((content, hovered, animationSpec, resolvedHoverColor, resolvedPressColor, layout, isPressed)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __composer.StartReplaceGroup(1296287483);
                content(new DsClickIndicationScope(hovered, isPressed.Value is { HasValue: true, Value: true }));
                __composer.EndReplaceGroup(1296287483);
                // Hover Indication:
                __Spacer(modifier: Modifier.Float().FillMaxSize().Position(top: 0.Px(), left: 0.Px()).Background(__AnimateColorAsState(hovered ? resolvedHoverColor : resolvedHoverColor.With(a: 0), __composer: __composer, __changed: 0b_01_00).Value), __composer: __composer, __changed: 0b_00);
                var pressAnimation = __RememberSingleAnimation(animationSpec, __composer: __composer, __changed: 0b_01_00 | ((__dirty & 0b_11_00_00_00_00_00_00) >> 12));
                var releaseAnimation = __RememberSingleAnimation(animationSpec, __composer: __composer, __changed: 0b_01_00 | ((__dirty & 0b_11_00_00_00_00_00_00) >> 12));
                if (!layout.Value.HasValue || !isPressed.Value.HasValue)
                    return;
                var layoutValue = layout.Value.Value;
                var pressedValue = isPressed.Value.Value;
                var pressPosition = layoutValue.Size / 2;
                __LaunchedEffect(pressedValue, (!__composer.ChangedAsStruct((pressAnimation, releaseAnimation, pressedValue)) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                {
                    if (pressedValue)
                    {
                        releaseAnimation.Stop();
                        pressAnimation.Start();
                    }
                    else
                        releaseAnimation.Start();
                })), __composer: __composer, __changed: 0b_00_00);
                var pressProgress = pressAnimation.Progress;
                var maxSize = (!__composer.ChangedAsStruct<global::UnityEngine.Vector2>(layoutValue.Size) ? __composer.RememberedValueAsStruct<float>() : __composer.UpdateRememberedValueAsStruct<float>(layoutValue.Size.magnitude));
                var size = maxSize * pressProgress;
                __Spacer(Modifier.Size(size.Px()).Border(size.Px() / 2).Background(resolvedPressColor).Alpha(1 - releaseAnimation.Progress).Float().Offset(x: -size.Px() / 2, y: -size.Px() / 2).Position(top: pressPosition.y.Px(), left: pressPosition.x.Px()), __composer: __composer, __changed: 0b_00);
            })), __composer: __composer, __changed: 0b_01_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1316622409, __isRestarted)?.UpdateScope(() => __DsClickIndication(__content, __hovered, __onHover, __onLeave, __rippleColor, __hoverColor, __animationSpec, __modifier, __composer, __dirtyRestart));
    }

    public static void __DsClickIndication(ComposableContent<DsClickIndicationScope> content, Optional<Color> rippleColor = default, Optional<Color> hoverColor = default, Optional<AnimationSpec> animationSpec = default, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __rippleColor, __hoverColor, __animationSpec, __modifier) = (content, rippleColor, hoverColor, animationSpec, modifier);
        var __isCreated = __composer.StartRestartGroup(459997311);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_00_10 : 0b_00_00_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_01;
        }

        if ((__changed & 0b_00_00_00_11_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(rippleColor) ? 0b_00_00_00_10_00 : 0b_00_00_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_01_00;
        }

        if ((__changed & 0b_00_00_11_00_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(hoverColor) ? 0b_00_00_10_00_00 : 0b_00_00_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01_00_00;
        }

        if ((__changed & 0b_00_11_00_00_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(animationSpec) ? 0b_00_10_00_00_00 : 0b_00_01_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00_00_00;
        }

        if ((__changed & 0b_11_00_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00 : 0b_01_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01)
        {
            var isHovered = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
            __DsClickIndication(content: content, hovered: isHovered.Value, onHover: (!__composer.Changed(isHovered) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = true)), onLeave: (!__composer.Changed(isHovered) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = false)), rippleColor: rippleColor, hoverColor: hoverColor, animationSpec: animationSpec, modifier: modifier, __composer: __composer, __changed: (__dirty & 0b_00_00_00_00_00_00_00_11) | ((__dirty & 0b_00_00_00_00_00_00_11_00) << 6) | ((__dirty & 0b_00_00_00_00_00_11_00_00) << 6) | ((__dirty & 0b_00_00_00_00_11_00_00_00) << 6) | ((__dirty & 0b_00_00_00_11_00_00_00_00) << 6));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(459997311, __isRestarted)?.UpdateScope(() => __DsClickIndication(__content, __rippleColor, __hoverColor, __animationSpec, __modifier, __composer, __dirtyRestart));
    }
}