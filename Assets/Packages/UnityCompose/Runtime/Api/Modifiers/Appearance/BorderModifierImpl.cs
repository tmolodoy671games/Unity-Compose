// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Border(
        this IModifier modifier,
        LayoutLength radius = default,
        LayoutLength verticalRadius = default,
        LayoutLength horizontalRadius = default,
        LayoutLength topRadius = default,
        LayoutLength bottomRadius = default,
        LayoutLength leftRadius = default,
        LayoutLength rightRadius = default,
        LayoutLength topLeftRadius = default,
        LayoutLength topRightRadius = default,
        LayoutLength bottomLeftRadius = default,
        LayoutLength bottomRightRadius = default,
        float width = -1,
        float verticalWidth = -1,
        float horizontalWidth = -1,
        float topWidth = -1,
        float bottomWidth = -1,
        float leftWidth = -1,
        float rightWidth = -1,
        Optional<Color> color = default,
        Optional<Color> verticalColor = default,
        Optional<Color> horizontalColor = default,
        Optional<Color> topColor = default,
        Optional<Color> bottomColor = default,
        Optional<Color> leftColor = default,
        Optional<Color> rightColor = default,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new BorderModifierImpl(
            topLeftRadius: ParamUtils
                .Resolve(topLeftRadius, topRadius, leftRadius, verticalRadius, horizontalRadius, radius),
            topRightRadius: ParamUtils
                .Resolve(topRightRadius, topRadius, rightRadius, verticalRadius, horizontalRadius, radius),
            bottomLeftRadius: ParamUtils
                .Resolve(bottomLeftRadius, bottomRadius, leftRadius, verticalRadius, horizontalRadius, radius),
            bottomRightRadius: ParamUtils
                .Resolve(bottomRightRadius, bottomRadius, rightRadius, verticalRadius, horizontalRadius, radius),
            topWidth: ParamUtils.Resolve(topWidth, verticalWidth, width),
            bottomWidth: ParamUtils.Resolve(bottomWidth, verticalWidth, width),
            leftWidth: ParamUtils.Resolve(leftWidth, horizontalWidth, width),
            rightWidth: ParamUtils.Resolve(rightWidth, horizontalWidth, width),
            topColor: ParamUtils.Resolve(topColor, verticalColor, color),
            bottomColor: ParamUtils.Resolve(bottomColor, verticalColor, color),
            leftColor: ParamUtils.Resolve(leftColor, horizontalColor, color),
            rightColor: ParamUtils.Resolve(rightColor, horizontalColor, color),
            transition: transition
        );
    }
}

internal class BorderModifierImpl : BaseModifier<BorderModifierImpl>
{
    private readonly LayoutLength _topLeftRadius;
    private readonly LayoutLength _topRightRadius;
    private readonly LayoutLength _bottomLeftRadius;
    private readonly LayoutLength _bottomRightRadius;
    private readonly float _topWidth;
    private readonly float _bottomWidth;
    private readonly float _leftWidth;
    private readonly float _rightWidth;
    private readonly Optional<Color> _topColor;
    private readonly Optional<Color> _bottomColor;
    private readonly Optional<Color> _leftColor;
    private readonly Optional<Color> _rightColor;
    private readonly Optional<ComposeTransition> _transition;

    public BorderModifierImpl(
        LayoutLength topLeftRadius,
        LayoutLength topRightRadius,
        LayoutLength bottomLeftRadius,
        LayoutLength bottomRightRadius,
        float topWidth,
        float bottomWidth,
        float leftWidth,
        float rightWidth,
        Optional<Color> topColor,
        Optional<Color> bottomColor,
        Optional<Color> leftColor,
        Optional<Color> rightColor,
        Optional<ComposeTransition> transition
    )
    {
        _topLeftRadius = topLeftRadius;
        _topRightRadius = topRightRadius;
        _bottomLeftRadius = bottomLeftRadius;
        _bottomRightRadius = bottomRightRadius;
        _topWidth = topWidth;
        _bottomWidth = bottomWidth;
        _leftWidth = leftWidth;
        _rightWidth = rightWidth;
        _topColor = topColor;
        _bottomColor = bottomColor;
        _leftColor = leftColor;
        _rightColor = rightColor;
        _transition = transition;
    }

    public override void Apply(VisualElement element)
    {
        if (_topLeftRadius.HasValue)
        {
            element.style.borderTopLeftRadius = _topLeftRadius;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-top-left-radius");
        }

        if (_topRightRadius.HasValue)
        {
            element.style.borderTopRightRadius = _topRightRadius;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-top-right-radius");
        }

        if (_bottomLeftRadius.HasValue)
        {
            element.style.borderBottomLeftRadius = _bottomLeftRadius;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-bottom-left-radius");
        }

        if (_bottomRightRadius.HasValue)
        {
            element.style.borderBottomRightRadius = _bottomRightRadius;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-bottom-right-radius");
        }

        if (_topWidth >= 0)
        {
            element.style.borderTopWidth = _topWidth;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-top-width");
        }

        if (_bottomWidth >= 0)
        {
            element.style.borderBottomWidth = _bottomWidth;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-bottom-width");
        }

        if (_leftWidth >= 0)
        {
            element.style.borderLeftWidth = _leftWidth;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-left-width");
        }

        if (_rightWidth >= 0)
        {
            element.style.borderRightWidth = _rightWidth;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-right-width");
        }

        if (_topColor.HasValue)
        {
            element.style.borderTopColor = _topColor.Value;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-top-color");
        }

        if (_bottomColor.HasValue)
        {
            element.style.borderBottomColor = _bottomColor.Value;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-bottom-color");
        }

        if (_leftColor.HasValue)
        {
            element.style.borderLeftColor = _leftColor.Value;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-left-color");
        }

        if (_rightColor.HasValue)
        {
            element.style.borderRightColor = _rightColor.Value;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-right-color");
        }
    }

    public override void Revert(VisualElement element)
    {
        if (_topLeftRadius.HasValue)
        {
            element.style.borderTopLeftRadius = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-top-left-radius");
        }

        if (_topRightRadius.HasValue)
        {
            element.style.borderTopRightRadius = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-top-right-radius");
        }

        if (_bottomLeftRadius.HasValue)
        {
            element.style.borderBottomLeftRadius = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-bottom-left-radius");
        }

        if (_bottomRightRadius.HasValue)
        {
            element.style.borderBottomRightRadius = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-bottom-right-radius");
        }

        if (_topWidth >= 0)
        {
            element.style.borderTopWidth = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-top-width");
        }

        if (_bottomWidth >= 0)
        {
            element.style.borderBottomWidth = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-bottom-width");
        }

        if (_leftWidth >= 0)
        {
            element.style.borderLeftWidth = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-left-width");
        }

        if (_rightWidth >= 0)
        {
            element.style.borderRightWidth = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-right-width");
        }

        if (_topColor.HasValue)
        {
            element.style.borderTopColor = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-top-color");
        }

        if (_bottomColor.HasValue)
        {
            element.style.borderBottomColor = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-bottom-color");
        }

        if (_leftColor.HasValue)
        {
            element.style.borderLeftColor = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-left-color");
        }

        if (_rightColor.HasValue)
        {
            element.style.borderRightColor = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("border-right-color");
        }
    }

    protected override bool Equals(BorderModifierImpl other)
    {
        return _topLeftRadius.Equals(other._topLeftRadius) &&
               _topRightRadius.Equals(other._topRightRadius) &&
               _bottomLeftRadius.Equals(other._bottomLeftRadius) &&
               _bottomRightRadius.Equals(other._bottomRightRadius) &&
               _topWidth.AlmostEquals(other._topWidth) &&
               _bottomWidth.AlmostEquals(other._bottomWidth) &&
               _leftWidth.AlmostEquals(other._leftWidth) &&
               _rightWidth.AlmostEquals(other._rightWidth) &&
               _topColor.Equals(other._topColor) &&
               _bottomColor.Equals(other._bottomColor) &&
               _leftColor.Equals(other._leftColor) &&
               _rightColor.Equals(other._rightColor);
    }
}