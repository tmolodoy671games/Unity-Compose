// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnterTransition Enter(
        Func<float, VisualElement, IModifier> transition,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return new CustomEnterTransitionImpl(transition, animationSpec.GetOrDefault());
    }
}

internal class CustomEnterTransitionImpl : IEnterTransition
{
    private readonly Func<float, VisualElement, IModifier> _transition;
    private readonly AnimationSpec _animationSpec;

    public CustomEnterTransitionImpl(Func<float, VisualElement, IModifier> transition, AnimationSpec animationSpec)
    {
        _transition = transition;
        _animationSpec = animationSpec;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, VisualElement parent)
    {
        if (timeElapsed < _animationSpec.Delay)
            return Modifier;
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return _transition(resolvedProgress, parent);
    }

    public IEnterTransition With(AnimationSpec animationSpec)
    {
        return new CustomEnterTransitionImpl(
            transition: _transition,
            animationSpec: animationSpec
        );
    }
}