// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class ScaleModifierImpl : UnityModifier<ScaleModifierImpl>
{
    private readonly float _scaleX;
    private readonly float _scaleY;

    public ScaleModifierImpl(float scaleX, float scaleY)
    {
        _scaleX = scaleX;
        _scaleY = scaleY;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var newScale = element.style.scale.value.value.ToVector2() + new Vector2(_scaleX, _scaleY);
        var newScaleValue = newScale == Vector2.zero ? StyleKeyword.Null : new StyleScale(newScale);
        element.style.scale = newScaleValue;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var newScale = element.style.scale.value.value.ToVector2() - new Vector2(_scaleX, _scaleY);
        var newScaleValue = newScale == Vector2.zero ? StyleKeyword.Null : new StyleScale(newScale);
        element.style.scale = newScaleValue;
    }

    protected override bool Equals(ScaleModifierImpl other)
    {
        return _scaleX.AlmostEquals(other._scaleX) &&
               _scaleY.AlmostEquals(other._scaleY);
    }

    public override int GetHashCode() => HashCode.Combine(_scaleX, _scaleY);
}