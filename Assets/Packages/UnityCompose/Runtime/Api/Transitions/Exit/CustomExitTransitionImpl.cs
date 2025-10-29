// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IExitTransition Exit(
        Func<float, LayoutInfo, IModifier> transition,
        AnimationSpec animationSpec = default
    )
    {
        return new CustomExitTransitionImpl(transition, animationSpec);
    }
}

internal class CustomExitTransitionImpl : IExitTransition
{
    private readonly Func<float, LayoutInfo, IModifier> _transition;
    private readonly AnimationSpec _animationSpec;

    public CustomExitTransitionImpl(Func<float, LayoutInfo, IModifier> transition, AnimationSpec animationSpec)
    {
        _transition = transition;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        if (resolvedProgress <= 0f)
            return Modifier;
        return _transition(resolvedProgress, parent);
    }
}