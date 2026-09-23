// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class BorderModifierImpl : BaseUnityModifier<BorderModifierImpl>
{
    private readonly Dp _borderWidth;
    private readonly Color _borderColor;

    public BorderModifierImpl(Dp borderWidth, Color borderColor)
    {
        _borderWidth = borderWidth;
        _borderColor = borderColor;
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
    }

    protected override bool Equals(BorderModifierImpl other)
    {
        return _borderWidth == other._borderWidth &&
               _borderColor == other._borderColor;
    }
}