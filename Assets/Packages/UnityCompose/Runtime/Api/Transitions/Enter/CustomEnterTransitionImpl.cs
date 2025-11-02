// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnterTransition Enter(
        Func<float, LayoutInfo, IModifier> transition,
        AnimationSpec animationSpec = default
    )
    {
        return new CustomEnterTransitionImpl(transition, animationSpec);
    }
}

internal class CustomEnterTransitionImpl : IEnterTransition
{
    private readonly Func<float, LayoutInfo, IModifier> _transition;
    private readonly AnimationSpec _animationSpec;

    public CustomEnterTransitionImpl(Func<float, LayoutInfo, IModifier> transition, AnimationSpec animationSpec)
    {
        _transition = transition;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
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