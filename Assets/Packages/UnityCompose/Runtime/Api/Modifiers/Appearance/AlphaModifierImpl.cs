// ReSharper disable CheckNamespace

using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Alpha(
        this IModifier modifier,
        float alpha,
        Optional<ComposeTransition> transition = default
    ) => modifier + new AlphaModifierImpl(alpha, transition);
}

internal class AlphaModifierImpl : BaseModifier<AlphaModifierImpl>
{
    private readonly float _alpha;
    private readonly Optional<ComposeTransition> _transition;

    public AlphaModifierImpl(float alpha, Optional<ComposeTransition> transition)
    {
        _alpha = alpha;
        _transition = transition;
    }

    public override void Apply(VisualElement element)
    {
        element.style.opacity = _alpha;
        if (_transition.HasValue)
            element.AddTransition(_transition.Value, "opacity");
    }

    public override void Revert(VisualElement element)
    {
        element.style.opacity = StyleKeyword.Null;
        if (_transition.HasValue)
            element.RemoveTransition("opacity");
    }

    protected override bool Equals(AlphaModifierImpl other)
    {
        return _alpha.AlmostEquals(other._alpha) && _transition.Equals(other._transition);
    }
}