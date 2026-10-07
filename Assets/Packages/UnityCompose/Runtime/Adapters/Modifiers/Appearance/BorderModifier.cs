// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using Rect = Compose.Net.Rect;

namespace UnityCompose;

internal class BorderModifierImpl : UnityModifier<BorderModifierImpl>, IAppearanceModifier
{
    private readonly Dp _borderWidth;
    private readonly IBrush _brush;
    private readonly Shape _shape;

    public BorderModifierImpl(Dp borderWidth, IBrush brush, Shape shape)
    {
        _borderWidth = borderWidth;
        _brush = brush;
        _shape = shape;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        // BRUH
        element.style.borderBottomWidth = _borderWidth.Value;
        element.style.borderTopWidth = _borderWidth.Value;
        element.style.borderLeftWidth = _borderWidth.Value;
        element.style.borderRightWidth = _borderWidth.Value;
        var rect = new Rect(
            Offset: Offset.Zero,
            Size: new Size(
                element.layout.width,
                element.layout.height
            )
        );
        // _brush.Apply(element, rect, 1, DrawStyle.Stroke(_borderWidth.Value));
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        // BRUH
        element.style.borderBottomWidth = StyleKeyword.Null;
        element.style.borderTopWidth = StyleKeyword.Null;
        element.style.borderLeftWidth = StyleKeyword.Null;
        element.style.borderRightWidth = StyleKeyword.Null;
        var rect = new Rect(
            Offset: Offset.Zero,
            Size: new Size(
                element.layout.width,
                element.layout.height
            )
        );
        // _brush.Revert(element, rect, 1, DrawStyle.Stroke(_borderWidth.Value));
    }

    protected override bool Equals(BorderModifierImpl other)
    {
        return _borderWidth == other._borderWidth &&
               _brush == other._brush;
    }

    public override int GetHashCode() => HashCode.Combine(_borderWidth, _brush);
}