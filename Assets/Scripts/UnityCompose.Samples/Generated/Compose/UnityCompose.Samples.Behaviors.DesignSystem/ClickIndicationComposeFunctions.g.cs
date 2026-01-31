#nullable enable
using System;
using SharpExtensions;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.DesignSystem;
internal static partial class ClickIndicationComposeFunctions
{
    [Composable]
    private static void __DsClickIndication(ComposableContent<DsClickIndicationScope> content, IModifier? modifier = null, Optional<bool> hovered = default, Optional<bool> pressed = default, Optional<AnimationSpec> animationSpec = default, Optional<Color> hoverColor = default, Optional<Color> pressedColor = default)
    {
        var(__content, __modifier, __hovered, __pressed, __animationSpec, __hoverColor, __pressedColor) = (content, modifier, hovered, pressed, animationSpec, hoverColor, pressedColor);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-359445226);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __modifier, __hovered, __pressed, __animationSpec, __hoverColor, __pressedColor)))
        {
            var isHovered = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
            var isPressed = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
            var resolvedHoverColor = hoverColor.GetOrDefault(new Color(1, 1, 1, 0.25f));
            var resolvedPressedColor = pressedColor.GetOrDefault(new Color(0, 0, 0, 0.75f));
            var resolvedHovered = hovered.GetOrDefault(isHovered.Value);
            var resolvedPressed = pressed.GetOrDefault(isPressed.Value);
            var size = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(MutableStateOf(Optional.Empty<LayoutCoordinates>()));
            Box(modifier: modifier.OrEmpty().OnMouseEnter(!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.ChangedAsStruct((isHovered, isPressed)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
            {
                isHovered.Value = false;
                isPressed.Value = false;
            })).OnMouseDown(!__composer.Changed(isPressed) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isPressed.Value = true)).OnMouseUp(!__composer.Changed(isPressed) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isPressed.Value = false)).OnGloballyPositioned(!__composer.Changed(size) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => size.Value = it)).Clip(), content: !__composer.ChangedAsStruct((content, animationSpec, isHovered, isPressed, resolvedHoverColor, resolvedPressedColor, resolvedHovered, resolvedPressed, size)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                content(new DsClickIndicationScope(IsHovered: isHovered.Value, IsPressed: isPressed.Value));
                Spacer(Modifier.FillMaxSize().Background(resolvedHoverColor).Alpha(AnimateFloatAsState(resolvedHovered.ToInt(), animationSpec: animationSpec).Value).Float().Position(top: 0.Px(), left: 0.Px()));
                if (!size.Value.HasValue)
                    return;
                var resolvedSize = Math.Max(size.Value.Value.Width, size.Value.Value.Height);
                var clickProgress = AnimateFloatAsState(resolvedPressed.ToInt(), animationSpec).Value;
                var clickIndicationSize = clickProgress.Px() * resolvedSize * 1.1f;
                Spacer(Modifier.Size(clickIndicationSize).Border(clickIndicationSize / 2).Background(resolvedPressedColor).Offset(x: -clickIndicationSize / 2, y: -clickIndicationSize / 2).Alpha(clickProgress).Float().Position(top: 50.Percent(), left: 50.Percent()));
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-359445226, __isRestarted)?.UpdateScope(() => __DsClickIndication(__content, __modifier, __hovered, __pressed, __animationSpec, __hoverColor, __pressedColor));
    }
}