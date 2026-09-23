// ReSharper disable CheckNamespace

using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class AlphaModifierImpl : BaseUnityModifier<AlphaModifierImpl>
{
    private readonly float _alpha;

    public AlphaModifierImpl(float alpha)
    {
        _alpha = alpha;
    }

    public override void Apply(VisualElement element)
    {
        element.style.opacity = _alpha;
    }

    public override void Revert(VisualElement element)
    {
        element.style.opacity = StyleKeyword.Null;
    }

    protected override bool Equals(AlphaModifierImpl other)
    {
        return _alpha.AlmostEquals(other._alpha);
    }
}