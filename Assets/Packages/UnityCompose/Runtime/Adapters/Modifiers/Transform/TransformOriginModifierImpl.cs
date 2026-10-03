// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class TransformOriginModifierImpl : UnityModifier<TransformOriginModifierImpl>
{
    private readonly Dp _x;
    private readonly Dp _y;

    public TransformOriginModifierImpl(Dp x, Dp y)
    {
        _x = x;
        _y = y;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.transformOrigin = new TransformOrigin(_x.ToLength(), _y.ToLength());
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.transformOrigin = StyleKeyword.Null;
    }

    protected override bool Equals(TransformOriginModifierImpl other) => _x == other._x && _y == other._y;
    public override int GetHashCode() => HashCode.Combine(_x, _y);
}