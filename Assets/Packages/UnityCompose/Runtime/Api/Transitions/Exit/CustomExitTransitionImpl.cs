// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using SharpExtensions;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IExitTransition Exit(
        Func<float, LayoutInfo, IModifier> transition,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return new CustomExitTransitionImpl(transition, animationSpec.GetOrDefault());
    }
}

internal class CustomExitTransitionImpl : IExitTransition
{
    private readonly Func<float, LayoutInfo, IModifier> _transition;
    private readonly AnimationSpec _animationSpec;

    public CustomExitTransitionImpl(Func<float, LayoutInfo, IModifier> transition, AnimationSpec animationSpec)
    {
        _transition = transition;
        _animationSpec = animationSpec;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        if (resolvedProgress <= 0f)
            return Modifier;
        return _transition(resolvedProgress, parent);
    }

    public IExitTransition With(AnimationSpec animationSpec)
    {
        return new CustomExitTransitionImpl(_transition, animationSpec);
    }
}