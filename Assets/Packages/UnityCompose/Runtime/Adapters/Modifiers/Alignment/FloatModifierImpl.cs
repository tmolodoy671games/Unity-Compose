// ReSharper disable CheckNamespace

using Compose.Net;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record FloatModifierImpl : UnityModifier
{
    public static readonly FloatModifierImpl Instance = new();

    private FloatModifierImpl()
    {
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.position = Position.Absolute;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.position = element.style.position.CompareAndSetNull(Position.Absolute);
    }
}