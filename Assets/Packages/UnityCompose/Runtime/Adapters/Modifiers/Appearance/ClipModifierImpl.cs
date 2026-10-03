// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class ClipModifierImpl : UnityModifier<ClipModifierImpl>, IAppearanceModifier
{
    private readonly Optional<RoundedCornerShape> _shape;

    public ClipModifierImpl(Optional<RoundedCornerShape> shape)
    {
        _shape = shape;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.overflow = Overflow.Hidden;
        if (!_shape.HasValue)
            return;
        var shapeValue = _shape.Value;
        element.style.borderTopLeftRadius = shapeValue.TopLeft.ToLength();
        element.style.borderTopRightRadius = shapeValue.TopRight.ToLength();
        element.style.borderBottomLeftRadius = shapeValue.BottomLeft.ToLength();
        element.style.borderBottomRightRadius = shapeValue.BottomRight.ToLength();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.overflow = StyleKeyword.Null;
        if (!_shape.HasValue)
            return;
        element.style.borderTopLeftRadius = StyleKeyword.Null;
        element.style.borderTopRightRadius = StyleKeyword.Null;
        element.style.borderBottomLeftRadius = StyleKeyword.Null;
        element.style.borderBottomRightRadius = StyleKeyword.Null;
    }

    public override int GetHashCode() => HashCode.Combine(_shape);
    protected override bool Equals(ClipModifierImpl other) => _shape.Equals(other._shape);
}