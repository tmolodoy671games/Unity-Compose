// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Background(
        this IModifier modifier,
        Color color,
        Optional<RoundedCornerShape> shape = default,
        Optional<ComposeTransition> transition = default
    ) => modifier + new BackgroundColorModifierImpl(color, shape, transition);
}

internal class BackgroundColorModifierImpl : BaseModifier<BackgroundColorModifierImpl>
{
    private readonly Color _backgroundColor;
    private readonly Optional<RoundedCornerShape> _shape;
    private readonly Optional<ComposeTransition> _transition;

    public BackgroundColorModifierImpl(
        Color backgroundColor,
        Optional<RoundedCornerShape> shape,
        Optional<ComposeTransition> transition
    )
    {
        _backgroundColor = backgroundColor;
        _shape = shape;
        _transition = transition;
    }

    public override void Apply(VisualElement element)
    {
        element.style.backgroundColor = _backgroundColor;
        if (_transition.HasValue)
            element.AddTransition(_transition.Value, "background-color");
        if (_shape.HasValue)
        {
            var shapeValue = _shape.Value;
            element.style.borderTopLeftRadius = shapeValue.TopLeft.ToLength();
            element.style.borderTopRightRadius = shapeValue.TopRight.ToLength();
            element.style.borderBottomLeftRadius = shapeValue.BottomLeft.ToLength();
            element.style.borderBottomRightRadius = shapeValue.BottomRight.ToLength();
            if (_transition.HasValue)
            {
                element.AddTransition(_transition.Value, "border-top-left-radius");
                element.AddTransition(_transition.Value, "border-top-right-radius");
                element.AddTransition(_transition.Value, "border-bottom-left-radius");
                element.AddTransition(_transition.Value, "border-bottom-right-radius");
            }
        }
    }

    public override void Revert(VisualElement element)
    {
        element.style.backgroundColor = StyleKeyword.Null;
        if (_transition.HasValue)
            element.RemoveTransition("background-color");
        if (_shape.HasValue)
        {
            element.style.borderTopLeftRadius = StyleKeyword.Null;
            element.style.borderTopRightRadius = StyleKeyword.Null;
            element.style.borderBottomLeftRadius = StyleKeyword.Null;
            element.style.borderBottomRightRadius = StyleKeyword.Null;
            if (_transition.HasValue)
            {
                element.RemoveTransition("border-top-left-radius");
                element.RemoveTransition("border-top-right-radius");
                element.RemoveTransition("border-bottom-left-radius");
                element.RemoveTransition("border-bottom-right-radius");
            }
        }
    }

    protected override bool Equals(BackgroundColorModifierImpl other)
    {
        return _backgroundColor == other._backgroundColor &&
               _shape.Equals(other._shape) &&
               _transition == other._transition;
    }
}