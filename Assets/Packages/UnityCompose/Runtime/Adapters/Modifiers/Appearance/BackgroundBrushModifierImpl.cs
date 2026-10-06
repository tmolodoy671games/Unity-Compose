// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using Rect = Compose.Net.Rect;

namespace UnityCompose;

internal class BackgroundBrushModifierImpl : UnityModifier<BackgroundBrushModifierImpl>, IAppearanceModifier
{
    private readonly IBrush _brush;
    private readonly Optional<Shape> _shape;

    public BackgroundBrushModifierImpl(
        IBrush brush,
        Optional<Shape> shape
    )
    {
        _brush = brush;
        _shape = shape;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        _brush.Apply(
            element,
            drawRect: new Rect(Offset.Zero, element.contentRect.size.ToSize()),
            alpha: 1,
            style: DrawStyle.Fill
        );
        if (_shape.HasValue)
        {
            var shapeValue = _shape.Value;
            element.style.borderTopLeftRadius = shapeValue.TopLeft.ToLength();
            element.style.borderTopRightRadius = shapeValue.TopRight.ToLength();
            element.style.borderBottomLeftRadius = shapeValue.BottomLeft.ToLength();
            element.style.borderBottomRightRadius = shapeValue.BottomRight.ToLength();
        }
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.backgroundColor = StyleKeyword.Null;
        if (_shape.HasValue)
        {
            element.style.borderTopLeftRadius = StyleKeyword.Null;
            element.style.borderTopRightRadius = StyleKeyword.Null;
            element.style.borderBottomLeftRadius = StyleKeyword.Null;
            element.style.borderBottomRightRadius = StyleKeyword.Null;
        }
    }

    protected override bool Equals(BackgroundBrushModifierImpl other)
    {
        return Equals(_brush, other._brush) &&
               _shape.Equals(other._shape);
    }

    public override int GetHashCode() => HashCode.Combine(_shape, _brush);
}