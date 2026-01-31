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
    [Composable]
    private static void __DsClickIndication(ComposableContent content, bool hovered, Action onHover, Action onLeave, Optional<Color> rippleColor = default, Optional<Color> hoverColor = default, Optional<AnimationSpec> animationSpec = default, IModifier? modifier = null)
    {
        var(__content, __hovered, __onHover, __onLeave, __rippleColor, __hoverColor, __animationSpec, __modifier) = (content, hovered, onHover, onLeave, rippleColor, hoverColor, animationSpec, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1316622409);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __hovered, __onHover, __onLeave, __rippleColor, __hoverColor, __animationSpec, __modifier)))
        {
            var resolvedHoverColor = hoverColor.GetOrDefault(Color.white.With(a: 0.1f));
            var resolvedPressColor = rippleColor.GetOrDefault(Color.black.With(a: 0.75f));
            var layout = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(MutableStateOf(Optional.Empty<LayoutCoordinates>()));
            var isPressed = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<bool>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<bool>>>(MutableStateOf(Optional.Empty<bool>()));
            Box(modifier: modifier.OrEmpty().Clip().OnGloballyPositioned(!__composer.Changed(layout) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => layout.Value = it)).OnMouseEnter(!__composer.Changed(onHover) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => onHover())).OnMouseLeave(!__composer.ChangedAsStruct((onLeave, isPressed)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
            {
                onLeave();
                isPressed.Value = false;
            })).OnMouseDown(!__composer.Changed(isPressed) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isPressed.Value = true)).OnMouseUp(!__composer.Changed(isPressed) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isPressed.Value = false)), content: !__composer.ChangedAsStruct((content, hovered, animationSpec, resolvedHoverColor, resolvedPressColor, layout, isPressed)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                content();
                // Hover Indication:
                Spacer(modifier: Modifier.Float().FillMaxSize().Position(top: 0.Px(), left: 0.Px()).Background(AnimateColorAsState(hovered ? resolvedHoverColor : resolvedHoverColor.With(a: 0)).Value));
                var pressAnimation = RememberSingleAnimation(animationSpec);
                // var releaseAnimation = RememberSingleAnimation(animationSpec);
                if (!layout.Value.HasValue || !isPressed.Value.HasValue)
                    return;
                var layoutValue = layout.Value.Value;
                var pressedValue = isPressed.Value.Value;
                var pressPosition = layoutValue.Size / 2;
                LaunchedEffect(pressedValue, !__composer.ChangedAsStruct((pressAnimation, pressedValue)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                {
                    if (pressedValue)
                    {
                        // releaseAnimation.Stop();
                        pressAnimation.Start();
                    }
                // else
                //     releaseAnimation.Start();
                }));
                var pressProgress = pressAnimation.Progress;
                var maxSize = !__composer.ChangedAsStruct(layoutValue.Size) ? __composer.RememberedValueAsStruct<float>() : __composer.UpdateRememberedValueAsStruct<float>(layoutValue.Size.magnitude);
                var size = maxSize * pressProgress;
                Spacer(Modifier.Size(size.Px()).Border(size.Px() / 2).Background(resolvedPressColor)// .Alpha(1 - releaseAnimation.Progress)
                .Float().Offset(x: -size.Px() / 2, y: -size.Px() / 2).Position(top: pressPosition.y.Px(), left: pressPosition.x.Px()));
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1316622409, __isRestarted)?.UpdateScope(() => __DsClickIndication(__content, __hovered, __onHover, __onLeave, __rippleColor, __hoverColor, __animationSpec, __modifier));
    }

    [Composable]
    private static void __DsClickIndication(ComposableContent content, Optional<Color> rippleColor = default, Optional<Color> hoverColor = default, Optional<AnimationSpec> animationSpec = default, IModifier? modifier = null)
    {
        var(__content, __rippleColor, __hoverColor, __animationSpec, __modifier) = (content, rippleColor, hoverColor, animationSpec, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(459997311);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __rippleColor, __hoverColor, __animationSpec, __modifier)))
        {
            var isHovered = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
            DsClickIndication(content: content, hovered: isHovered.Value, onHover: !__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = true), onLeave: !__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = false), rippleColor: rippleColor, hoverColor: hoverColor, animationSpec: animationSpec, modifier: modifier);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(459997311, __isRestarted)?.UpdateScope(() => __DsClickIndication(__content, __rippleColor, __hoverColor, __animationSpec, __modifier));
    }
}