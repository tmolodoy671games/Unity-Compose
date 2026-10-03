// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class RotateModifierImpl : UnityModifier<RotateModifierImpl>
{
    private readonly float _degrees;

    public RotateModifierImpl(float degrees)
    {
        _degrees = degrees;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        StyleRotate newRotate = new Rotate(element.style.rotate.value.angle.value + _degrees);
        if (newRotate.value.angle.value == 0f)
            newRotate = StyleKeyword.Null;
        element.style.rotate = newRotate;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        StyleRotate newRotate = ToNullIfNeeded(new Rotate(element.style.rotate.value.angle.value - _degrees));
        if (newRotate.value.angle.value == 0f)
            newRotate = StyleKeyword.Null;
        element.style.rotate = newRotate;
    }

    protected override bool Equals(RotateModifierImpl other) => _degrees.AlmostEquals(other._degrees);
    public override int GetHashCode() => HashCode.Combine(_degrees);

    private static StyleRotate ToNullIfNeeded(Rotate rotate)
    {
        return rotate.angle.value.AlmostEquals(0f) ? StyleKeyword.Null : rotate;
    }
}