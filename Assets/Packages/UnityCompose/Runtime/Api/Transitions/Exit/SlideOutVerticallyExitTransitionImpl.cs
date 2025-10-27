// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition SlideOutVertically(
        Func<float, float> targetOffsetY,
        AnimationSpec animationSpec = default
    )
    {
        return new SlideOutVerticallyExitTransitionImpl(targetOffsetY, animationSpec);
    }
}

internal class SlideOutVerticallyExitTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetOffsetY;
    private readonly AnimationSpec _animationSpec;

    public SlideOutVerticallyExitTransitionImpl(Func<float, float> targetOffsetY, AnimationSpec animationSpec)
    {
        _targetOffsetY = targetOffsetY;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Position(
                top: Mathf.Lerp(
                    a: 0,
                    b: _targetOffsetY(parent.Height + parent.PaddingTop),
                    t: resolvedProgress
                )
            );
    }
}