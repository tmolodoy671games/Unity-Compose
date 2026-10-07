using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal record PositionModifierImpl(
    Optional<Dp> Top,
    Optional<Dp> Bottom,
    Optional<Dp> Left,
    Optional<Dp> Right
) : UnityModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var style = element.style;
        style.position = Position.Absolute;
        if (Top.HasValue)
            style.top = Top.Value.Value;
        if (Bottom.HasValue)
            style.bottom = Bottom.Value.Value;
        if (Left.HasValue)
            style.left = Left.Value.Value;
        if (Right.HasValue)
            style.right = Right.Value.Value;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var style = element.style;
        if (Top.HasValue)
            style.top = style.top.CompareAndSetNull(Top.Value.Value);
        if (Bottom.HasValue)
            style.bottom = style.top.CompareAndSetNull(Bottom.Value.Value);
        if (Left.HasValue)
            style.left = style.top.CompareAndSetNull(Left.Value.Value);
        if (Right.HasValue)
            style.right = style.top.CompareAndSetNull(Right.Value.Value);
    }
}