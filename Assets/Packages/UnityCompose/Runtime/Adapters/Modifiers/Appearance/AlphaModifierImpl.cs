// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;
using HashCode = System.HashCode;

namespace UnityCompose;

internal class AlphaModifierImpl : UnityModifier<AlphaModifierImpl>
{
    private readonly float _alpha;

    public AlphaModifierImpl(float alpha)
    {
        _alpha = alpha;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.opacity = _alpha;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.opacity = StyleKeyword.Null;
    }

    protected override bool Equals(AlphaModifierImpl other)
    {
        return _alpha.AlmostEquals(other._alpha);
    }

    public override int GetHashCode() => HashCode.Combine(_alpha);
}