// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class SizeModifierImpl : UnityModifier<SizeModifierImpl>, IAppearanceModifier
{
    private readonly Optional<Dp> _width;
    private readonly Optional<Dp> _height;

    public SizeModifierImpl(Optional<Dp> width, Optional<Dp> height)
    {
        _width = width;
        _height = height;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (_width.HasValue)
            element.style.width = _width.Value.ToLength();
        if (_height.HasValue)
            element.style.height = _height.Value.ToLength();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (_width.HasValue)
            element.style.width = StyleKeyword.Null;
        if (_height.HasValue)
            element.style.height = StyleKeyword.Null;
    }

    protected override bool Equals(SizeModifierImpl other)
    {
        return _width.Equals(other._width) && _height.Equals(other._height);
    }

    public override int GetHashCode() => HashCode.Combine(_width, _height);
}