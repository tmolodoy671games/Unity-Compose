// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Border(
        this IModifier modifier,
        Dp borderWidth,
        Color borderColor,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new BorderModifierImpl(borderWidth, borderColor, transition);
    }
}

internal class BorderModifierImpl : BaseModifier<BorderModifierImpl>
{
    private readonly Dp _borderWidth;
    private readonly Color _borderColor;
    private readonly Optional<ComposeTransition> _transition;

    public BorderModifierImpl(Dp borderWidth, Color borderColor, Optional<ComposeTransition> transition)
    {
        _borderWidth = borderWidth;
        _borderColor = borderColor;
        _transition = transition;
    }

    public override void Apply(VisualElement element)
    {
        element.style.borderBottomWidth = _borderWidth.Value;
        element.style.borderTopWidth = _borderWidth.Value;
        element.style.borderLeftWidth = _borderWidth.Value;
        element.style.borderRightWidth = _borderWidth.Value;

        element.style.borderTopColor = _borderColor;
        element.style.borderBottomColor = _borderColor;
        element.style.borderLeftColor = _borderColor;
        element.style.borderRightColor = _borderColor;

        if (_transition.HasValue)
        {
            var transitionValue = _transition.Value;
            element.AddTransitions(
                transitionValue,
                "border-top-width",
                "border-top-color",
                "border-bottom-width",
                "border-bottom-color",
                "border-left-width",
                "border-left-color",
                "border-right-width",
                "border-right-color"
            );
        }
    }

    public override void Revert(VisualElement element)
    {
        element.style.borderBottomWidth = _borderWidth.Value;
        element.style.borderTopWidth = _borderWidth.Value;
        element.style.borderLeftWidth = _borderWidth.Value;
        element.style.borderRightWidth = _borderWidth.Value;

        element.style.borderTopColor = _borderColor;
        element.style.borderBottomColor = _borderColor;
        element.style.borderLeftColor = _borderColor;
        element.style.borderRightColor = _borderColor;

        if (_transition.HasValue)
        {
            element.RemoveTransitions(
                "border-top-color",
                "border-bottom-width",
                "border-bottom-color",
                "border-left-width",
                "border-left-color",
                "border-right-width",
                "border-right-color"
            );
        }
    }

    protected override bool Equals(BorderModifierImpl other)
    {
        return _borderWidth == other._borderWidth &&
               _borderColor == other._borderColor &&
               _transition == other._transition;
    }
}