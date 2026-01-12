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
        Optional<ComposeTransition> transition = default
    ) => modifier + new BackgroundColorModifierImpl(color, transition);
}

internal class BackgroundColorModifierImpl : BaseModifier<BackgroundColorModifierImpl>
{
    private readonly Color _backgroundColor;
    private readonly Optional<ComposeTransition> _transition;

    public BackgroundColorModifierImpl(Color backgroundColor, Optional<ComposeTransition> transition)
    {
        _backgroundColor = backgroundColor;
        _transition = transition;
    }

    public override void Apply(VisualElement element)
    {
        element.style.backgroundColor = _backgroundColor;
        if (_transition.HasValue)
            element.AddTransition(_transition.Value, "background-color");
    }

    public override void Revert(VisualElement element)
    {
        element.style.backgroundColor = StyleKeyword.Null;
        if (_transition.HasValue)
            element.RemoveTransition("background-color");
    }

    protected override bool Equals(BackgroundColorModifierImpl other)
    {
        return _backgroundColor == other._backgroundColor && _transition == other._transition;
    }
}