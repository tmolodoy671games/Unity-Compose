using System;
using SharpExtensions;

namespace UnityCompose.Samples.Behaviors.DesignSystem;

internal static partial class ClickIndicationComposeFunctions
{
    [Composable]
    public static void DsClickIndication(
        ComposableContent<DsClickIndicationScope> content,
        IModifier? modifier = null,
        Optional<bool> hovered = default,
        Optional<bool> pressed = default,
        Optional<AnimationSpec> animationSpec = default,
        Optional<Color> hoverColor = default,
        Optional<Color> pressedColor = default
    )
    {
        var isHovered = Remember(() => MutableStateOf(false));
        var isPressed = Remember(() => MutableStateOf(false));
        var resolvedHoverColor = hoverColor.GetOrDefault(new Color(1, 1, 1, 0.25f));
        var resolvedPressedColor = pressedColor.GetOrDefault(new Color(0, 0, 0, 0.75f));
        var resolvedHovered = hovered.GetOrDefault(isHovered.Value);
        var resolvedPressed = pressed.GetOrDefault(isPressed.Value);
        var size = Remember(() => MutableStateOf(Optional.Empty<LayoutCoordinates>()));
        Box(
            modifier: modifier.OrEmpty()
                .OnMouseEnter(() => isHovered.Value = true)
                .OnMouseLeave(() =>
                {
                    isHovered.Value = false;
                    isPressed.Value = false;
                })
                .OnMouseDown(() => isPressed.Value = true)
                .OnMouseUp(() => isPressed.Value = false)
                .OnGloballyPositioned(it => size.Value = it)
                .Clip(),
            content: () =>
            {
                content(
                    new DsClickIndicationScope(
                        IsHovered: isHovered.Value,
                        IsPressed: isPressed.Value
                    )
                );
                Spacer(
                    Modifier
                        .FillMaxSize()
                        .Background(resolvedHoverColor)
                        .Alpha(AnimateFloatAsState(resolvedHovered.ToInt(), animationSpec: animationSpec).Value)
                        .Float()
                        .Position(top: 0.Px(), left: 0.Px())
                );

                if (!size.Value.HasValue)
                    return;
                var resolvedSize = Math.Max(size.Value.Value.Width, size.Value.Value.Height);
                var clickProgress = AnimateFloatAsState(resolvedPressed.ToInt(), animationSpec).Value;
                var clickIndicationSize = clickProgress.Px() * resolvedSize * 1.1f;
                Spacer(
                    Modifier
                        .Size(clickIndicationSize)
                        .Border(clickIndicationSize / 2)
                        .Background(resolvedPressedColor)
                        .Offset(x: -clickIndicationSize / 2, y: -clickIndicationSize / 2)
                        .Alpha(clickProgress)
                        .Float()
                        .Position(
                            top: 50.Percent(),
                            left: 50.Percent()
                        )
                );
            }
        );
    }
}

public readonly record struct DsClickIndicationScope(
    bool IsHovered,
    bool IsPressed
);