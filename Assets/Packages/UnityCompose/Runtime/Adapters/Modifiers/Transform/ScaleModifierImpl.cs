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
        element.style.scale = new Vector2(_scaleX, _scaleY);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.scale = StyleKeyword.Null;
    }

    protected override bool Equals(ScaleModifierImpl other)
    {
        return _scaleX.AlmostEquals(other._scaleX) &&
               _scaleY.AlmostEquals(other._scaleY);
    }

    public override int GetHashCode() => HashCode.Combine(_scaleX, _scaleY);

    private static Vector2 GetScale(VisualElement element)
    {
        var scaleValue = element.style.scale;
        return scaleValue == StyleKeyword.Null || scaleValue == StyleKeyword.None
            ? Vector2.one
            : scaleValue.value.value.ToVector2();
    }
}