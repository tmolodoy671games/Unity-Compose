// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class BackgroundColorModifierImpl : UnityModifier<BackgroundColorModifierImpl>, IAppearanceModifier
{
    private readonly Color _backgroundColor;
    private readonly Optional<Shape> _shape;

    public BackgroundColorModifierImpl(
        Color backgroundColor,
        Optional<Shape> shape
    )
    {
        _backgroundColor = backgroundColor;
        _shape = shape;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.backgroundColor = _backgroundColor;
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

    protected override bool Equals(BackgroundColorModifierImpl other)
    {
        return _backgroundColor == other._backgroundColor &&
               _shape.Equals(other._shape);
    }

    public override int GetHashCode() => HashCode.Combine(_shape, _backgroundColor);
}