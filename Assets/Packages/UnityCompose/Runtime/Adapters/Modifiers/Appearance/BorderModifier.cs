// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class BorderModifierImpl : UnityModifier<BorderModifierImpl>
{
    private readonly Dp _borderWidth;
    private readonly Color _borderColor;

    public BorderModifierImpl(Dp borderWidth, Color borderColor)
    {
        _borderWidth = borderWidth;
        _borderColor = borderColor;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.borderBottomWidth = _borderWidth.Value;
        element.style.borderTopWidth = _borderWidth.Value;
        element.style.borderLeftWidth = _borderWidth.Value;
        element.style.borderRightWidth = _borderWidth.Value;

        element.style.borderTopColor = _borderColor;
        element.style.borderBottomColor = _borderColor;
        element.style.borderLeftColor = _borderColor;
        element.style.borderRightColor = _borderColor;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.borderBottomWidth = _borderWidth.Value;
        element.style.borderTopWidth = _borderWidth.Value;
        element.style.borderLeftWidth = _borderWidth.Value;
        element.style.borderRightWidth = _borderWidth.Value;

        element.style.borderTopColor = _borderColor;
        element.style.borderBottomColor = _borderColor;
        element.style.borderLeftColor = _borderColor;
        element.style.borderRightColor = _borderColor;
    }

    protected override bool Equals(BorderModifierImpl other)
    {
        return _borderWidth == other._borderWidth &&
               _borderColor == other._borderColor;
    }

    public override int GetHashCode() => HashCode.Combine(_borderWidth, _borderColor);
}