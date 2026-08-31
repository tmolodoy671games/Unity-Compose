using System;
using SharpExtensions;
using UnityCompose;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.DesignSystem.Compose;

public static partial class DesignSystemComposeFunctions
{
    [Composable]
    public static void DsClickIndication(
        ComposableContent<DsClickIndicationScope> content,
        bool hovered,
        Action onHover,
        Action onLeave,
        Optional<Color> rippleColor = default,
        Optional<Color> hoverColor = default,
        Optional<AnimationSpec> animationSpec = default,
        IModifier? modifier = null
    )
    {
        var resolvedHoverColor = hoverColor.GetOrDefault(Color.white.With(a: 0.1f));
        var resolvedPressColor = rippleColor.GetOrDefault(Color.black.With(a: 0.75f));
        var layout = Remember(() => MutableStateOf(Optional.Empty<LayoutCoordinates>()));
        var isPressed = Remember(() => MutableStateOf(Optional.Empty<bool>()));
        Box(
            modifier: modifier.OrEmpty()
                .Clip(RoundedCornerShape())
                .OnGloballyPositioned(it => layout.Value = it)
                .OnMouseEnter(() => onHover())
                .OnMouseLeave(() =>
                {
                    onLeave();
                    isPressed.Value = false;
                })
                .OnMouseDown(() => isPressed.Value = true)
                .OnMouseUp(() => isPressed.Value = false),
            content: () =>
            {
                content(new DsClickIndicationScope(hovered, isPressed.Value is { HasValue: true, Value: true }));

                // Hover Indication:
                Spacer(
                    modifier: Modifier
                        .Float()
                        .FillMaxSize()
                        .Position(
                            top: 0.Dp(),
                            left: 0.Dp()
                        )
                        .Background(
                            AnimateColorAsState(hovered ? resolvedHoverColor : resolvedHoverColor.With(a: 0))
                                .Value
                        )
                );

                var pressAnimation = RememberSingleAnimation(animationSpec);
                var releaseAnimation = RememberSingleAnimation(animationSpec);
                if (!layout.Value.HasValue || !isPressed.Value.HasValue)
                    return;
                var layoutValue = layout.Value.Value;
                var pressedValue = isPressed.Value.Value;
                var pressPosition = layoutValue.Size / 2;
                SideEffect(pressedValue, () =>
                {
                    if (pressedValue)
                    {
                        releaseAnimation.Stop();
                        pressAnimation.Start();
                    }
                    else
                        releaseAnimation.Start();
                });
                var pressProgress = pressAnimation.Progress;

                var maxSize = Remember(layoutValue.Size, () => layoutValue.Size.magnitude);
                var size = maxSize * pressProgress;
                Spacer(
                    Modifier
                        .Size(size.Dp())
                        .Clip(RoundedCornerShape(size.Dp() / 2))
                        .Background(resolvedPressColor)
                        .Alpha(1 - releaseAnimation.Progress)
                        .Float()
                        .Offset(x: -size.Dp() / 2, y: -size.Dp() / 2)
                        .Position(
                            top: pressPosition.y.Dp(),
                            left: pressPosition.x.Dp()
                        )
                );
            }
        );
    }

    [Composable]
    public static void DsClickIndication(
        ComposableContent<DsClickIndicationScope> content,
        Optional<Color> rippleColor = default,
        Optional<Color> hoverColor = default,
        Optional<AnimationSpec> animationSpec = default,
        IModifier? modifier = null
    )
    {
        var isHovered = Remember(() => MutableStateOf(false));
        DsClickIndication(
            content: content,
            hovered: isHovered.Value,
            onHover: () => isHovered.Value = true,
            onLeave: () => isHovered.Value = false,
            rippleColor: rippleColor,
            hoverColor: hoverColor,
            animationSpec: animationSpec,
            modifier: modifier
        );
    }

    public static Color With(
        this Color color,
        float r = -1,
        float g = -1,
        float b = -1,
        float a = -1,
        float h = -1,
        float s = -1,
        float v = -1
    )
    {
        return new Color(
            r: r < 0 ? color.r : r,
            g: g < 0 ? color.g : g,
            b: b < 0 ? color.b : b,
            a: a < 0 ? color.a : a
        );
    }
}

public readonly record struct DsClickIndicationScope(
    bool IsHovered,
    bool IsPressed
);