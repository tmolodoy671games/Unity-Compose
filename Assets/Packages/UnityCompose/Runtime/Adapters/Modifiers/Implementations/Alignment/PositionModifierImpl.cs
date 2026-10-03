using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class PositionModifierImpl : UnityModifier<PositionModifierImpl>
{
    private readonly Optional<Dp> _top;
    private readonly Optional<Dp> _bottom;
    private readonly Optional<Dp> _left;
    private readonly Optional<Dp> _right;

    public PositionModifierImpl(
        Optional<Dp> top,
        Optional<Dp> bottom,
        Optional<Dp> left,
        Optional<Dp> right
    )
    {
        _top = top;
        _bottom = bottom;
        _left = left;
        _right = right;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.position = Position.Absolute;
        if (_top.HasValue)
            element.style.top = _top.Value.Value;
        if (_bottom.HasValue)
            element.style.bottom = _bottom.Value.Value;
        if (_left.HasValue)
            element.style.left = _left.Value.Value;
        if (_right.HasValue)
            element.style.right = _right.Value.Value;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (_top.HasValue)
            element.style.top = StyleKeyword.Null;
        if (_bottom.HasValue)
            element.style.bottom = StyleKeyword.Null;
        if (_left.HasValue)
            element.style.left = StyleKeyword.Null;
        if (_right.HasValue)
            element.style.right = StyleKeyword.Null;
    }

    protected override bool Equals(PositionModifierImpl other)
    {
        return _top.Equals(other._top) &&
               _bottom.Equals(other._bottom) &&
               _left.Equals(other._left) &&
               _right.Equals(other._right);
    }

    public override int GetHashCode() => HashCode.Combine(_top, _bottom, _left, _right);
}