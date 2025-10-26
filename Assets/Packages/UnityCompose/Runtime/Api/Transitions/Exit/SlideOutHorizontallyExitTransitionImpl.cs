// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition SlideOutHorizontally(
        Func<float, float> targetOffsetX,
        AnimationSpec animationSpec = default
    )
    {
        return new SlideOutHorizontallyExitTransitionImpl(targetOffsetX, animationSpec);
    }
}

internal class SlideOutHorizontallyExitTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetOffsetX;
    private readonly AnimationSpec _animationSpec;

    public SlideOutHorizontallyExitTransitionImpl(Func<float, float> targetOffsetX, AnimationSpec animationSpec)
    {
        _targetOffsetX = targetOffsetX;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration();

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Position(
                left: Mathf.Lerp(
                    a: 0,
                    b: _targetOffsetX(parent.Width + parent.PaddingLeft),
                    t: resolvedProgress
                )
            );
    }
}