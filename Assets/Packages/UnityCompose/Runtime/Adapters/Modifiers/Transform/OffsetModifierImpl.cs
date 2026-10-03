// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OffsetModifierImpl : UnityModifier<OffsetModifierImpl>
{
    private readonly Dp _x;
    private readonly Dp _y;

    public OffsetModifierImpl(Dp x, Dp y)
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
        element.style.translate = new Translate(_x.ToLength(), _y.ToLength());
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.translate = StyleKeyword.Null;
    }

    protected override bool Equals(OffsetModifierImpl other)
    {
        return _x.Equals(other._x) && _y.Equals(other._y);
    }

    public override int GetHashCode() => HashCode.Combine(_x, _y);
    public override string ToString() => $"Offset({_x}, {_y})";
}