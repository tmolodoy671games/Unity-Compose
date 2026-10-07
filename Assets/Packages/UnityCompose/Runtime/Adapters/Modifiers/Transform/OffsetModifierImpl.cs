// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record OffsetModifierImpl(
    Dp X,
    Dp Y
) : UnityModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.translate = new Translate(X.ToLength(), Y.ToLength());
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var style = element.style;
        style.translate = style.translate.CompareAndSetNull(new Translate(X.ToLength(), Y.ToLength()));
    }
}