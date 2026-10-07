// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;
using UnityEngine.UIElements;
using HashCode = System.HashCode;

namespace UnityCompose;

internal record AlphaModifierImpl(
    float Alpha
) : UnityModifier, IAppearanceModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.opacity = Alpha;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.opacity = element.style.opacity.CompareAndSetNull(Alpha);
    }
}