// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class WeightModifierImpl : UnityModifier<WeightModifierImpl>
{
    private readonly float _weight;

    public WeightModifierImpl(float weight)
    {
        _weight = weight;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.flexGrow = _weight;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.flexGrow = StyleKeyword.Null;
    }

    protected override bool Equals(WeightModifierImpl other) => _weight.AlmostEquals(other._weight);
    public override int GetHashCode() => HashCode.Combine(_weight);
}