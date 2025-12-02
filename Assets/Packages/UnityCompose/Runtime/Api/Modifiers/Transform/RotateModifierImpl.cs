// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Rotate(
        this IModifier modifier,
        float degrees,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new RotateModifierImpl(degrees, transition);
    }
}

internal class RotateModifierImpl : BaseModifier<RotateModifierImpl>
{
    private readonly float _degrees;
    private readonly Optional<ComposeTransition> _transition;

    public RotateModifierImpl(float degrees, Optional<ComposeTransition> transition)
    {
        _degrees = degrees;
        _transition = transition;
    }

    public override void Apply(VisualElement element)
    {
        element.style.rotate = new Rotate(_degrees);
        if (_transition.HasValue)
            element.AddTransition(_transition.Value, "rotate");
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.Rotate);
    }

    public override void Revert(VisualElement element)
    {
        element.style.rotate = StyleKeyword.Null;
    }

    protected override bool Equals(RotateModifierImpl other)
    {
        return _degrees.AlmostEquals(other._degrees) && _transition.Equals(other._transition);
    }
}