// ReSharper disable CheckNamespace

using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Weight(this IModifier modifier, int weight)
    {
        return modifier + new WeightModifierImpl(weight);
    }
}

internal class WeightModifierImpl : BaseModifier<WeightModifierImpl>
{
    private readonly float _weight;

    public WeightModifierImpl(float weight)
    {
        _weight = weight;
    }

    public override void Apply(VisualElement element)
    {
        element.style.flexGrow = _weight;
    }

    public override void Revert(VisualElement element)
    {
        element.style.flexGrow = StyleKeyword.Null;
    }

    protected override bool Equals(WeightModifierImpl other)
    {
        return _weight.AlmostEquals(other._weight);
    }
}