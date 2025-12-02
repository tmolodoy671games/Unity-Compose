// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Scale(
        this IModifier modifier,
        Optional<float> scale = default,
        Optional<float> scaleX = default,
        Optional<float> scaleY = default,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new ScaleModifierImpl(
            new Vector2(
                ParamUtils.Resolve(scaleX, scale).GetOrDefault(1),
                ParamUtils.Resolve(scaleY, scale).GetOrDefault(1)
            ),
            transition
        );
    }

    public static IModifier Scale(
        this IModifier modifier,
        Vector2 scale,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new ScaleModifierImpl(scale, transition);
    }
}

internal class ScaleModifierImpl : BaseModifier<ScaleModifierImpl>
{
    private readonly Vector2 _scale;
    private readonly Optional<ComposeTransition> _transition;

    public ScaleModifierImpl(Vector2 scale, Optional<ComposeTransition> transition)
    {
        _scale = scale;
        _transition = transition;
    }

    public override void Apply(VisualElement element)
    {
        element.style.scale = _scale;
        if (_transition.HasValue)
            element.AddTransition(_transition.Value, "scale");
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.Scale);
    }

    public override void Revert(VisualElement element)
    {
        element.style.scale = StyleKeyword.Null;
    }

    protected override bool Equals(ScaleModifierImpl other)
    {
        return _scale.Equals(other._scale) && _transition.Equals(other._transition);
    }
}