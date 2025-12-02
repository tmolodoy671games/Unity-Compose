// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Border(
        this IModifier modifier,
        float radius = -1,
        float verticalRadius = -1,
        float horizontalRadius = -1,
        float topLeftRadius = -1,
        float topRightRadius = -1,
        float bottomLeftRadius = -1,
        float bottomRightRadius = -1,
        float allWidth = -1,
        float verticalWidth = -1,
        float horizontalWidth = -1,
        float topWidth = -1,
        float bottomWidth = -1,
        float leftWidth = -1,
        float rightWidth = -1,
        Optional<Color> allColor = default,
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
            topLeftRadius: ParamUtils.Resolve(topLeftRadius, verticalRadius, horizontalRadius, radius),
            topRightRadius: ParamUtils.Resolve(topRightRadius, verticalRadius, horizontalRadius, radius),
            bottomLeftRadius: ParamUtils.Resolve(bottomLeftRadius, verticalRadius, horizontalRadius, radius),
            bottomRightRadius: ParamUtils.Resolve(bottomRightRadius, verticalRadius, horizontalRadius, radius),
            topWidth: ParamUtils.Resolve(topWidth, verticalWidth, allWidth),
            bottomWidth: ParamUtils.Resolve(bottomWidth, verticalWidth, allWidth),
            leftWidth: ParamUtils.Resolve(leftWidth, horizontalWidth, allWidth),
            rightWidth: ParamUtils.Resolve(rightWidth, horizontalWidth, allWidth),
            topColor: ParamUtils.Resolve(topColor, verticalColor, allColor),
            bottomColor: ParamUtils.Resolve(bottomColor, verticalColor, allColor),
            leftColor: ParamUtils.Resolve(leftColor, horizontalColor, allColor),
            rightColor: ParamUtils.Resolve(rightColor, horizontalColor, allColor),
            transition: transition
        );
    }
}

internal class BorderModifierImpl : BaseModifier<BorderModifierImpl>
{
    private readonly float _topLeftRadius;
    private readonly float _topRightRadius;
    private readonly float _bottomLeftRadius;
    private readonly float _bottomRightRadius;
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
        float topLeftRadius,
        float topRightRadius,
        float bottomLeftRadius,
        float bottomRightRadius,
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
        if (_topLeftRadius >= 0)
        {
            element.style.borderTopLeftRadius = _topLeftRadius;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-top-left-radius");
        }

        if (_topRightRadius >= 0)
        {
            element.style.borderTopRightRadius = _topRightRadius;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-top-right-radius");
        }

        if (_bottomLeftRadius >= 0)
        {
            element.style.borderBottomLeftRadius = _bottomLeftRadius;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "border-bottom-left-radius");
        }

        if (_bottomRightRadius >= 0)
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

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        if (_topLeftRadius >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.BorderTopLeftRadius);

        if (_topRightRadius >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.BorderTopRightRadius);

        if (_bottomLeftRadius >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.BorderBottomLeftRadius);

        if (_bottomRightRadius >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.BorderBottomRightRadius);

        if (_topWidth >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.BorderTopWidth);

        if (_bottomWidth >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.BorderBottomWidth);

        if (_leftWidth >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.BorderLeftWidth);

        if (_rightWidth >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.BorderRightWidth);

        if (_topColor.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.BorderTopColor);

        if (_bottomColor.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.BorderBottomColor);

        if (_leftColor.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.BorderLeftColor);

        if (_rightColor.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.BorderRightColor);
    }

    public override void Revert(VisualElement element)
    {
        if (_topLeftRadius >= 0)
            element.style.borderTopLeftRadius = StyleKeyword.Null;

        if (_topRightRadius >= 0)
            element.style.borderTopRightRadius = StyleKeyword.Null;

        if (_bottomLeftRadius >= 0)
            element.style.borderBottomLeftRadius = StyleKeyword.Null;

        if (_bottomRightRadius >= 0)
            element.style.borderBottomRightRadius = StyleKeyword.Null;

        if (_topWidth >= 0)
            element.style.borderTopWidth = StyleKeyword.Null;

        if (_bottomWidth >= 0)
            element.style.borderBottomWidth = StyleKeyword.Null;

        if (_leftWidth >= 0)
            element.style.borderLeftWidth = StyleKeyword.Null;

        if (_rightWidth >= 0)
            element.style.borderRightWidth = StyleKeyword.Null;

        if (_topColor.HasValue)
            element.style.borderTopColor = StyleKeyword.Null;

        if (_bottomColor.HasValue)
            element.style.borderBottomColor = StyleKeyword.Null;

        if (_leftColor.HasValue)
            element.style.borderLeftColor = StyleKeyword.Null;

        if (_rightColor.HasValue)
            element.style.borderRightColor = StyleKeyword.Null;
    }

    protected override bool Equals(BorderModifierImpl other)
    {
        return _topLeftRadius.AlmostEquals(other._topLeftRadius) &&
               _topRightRadius.AlmostEquals(other._topRightRadius) &&
               _bottomLeftRadius.AlmostEquals(other._bottomLeftRadius) &&
               _bottomRightRadius.AlmostEquals(other._bottomRightRadius) &&
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