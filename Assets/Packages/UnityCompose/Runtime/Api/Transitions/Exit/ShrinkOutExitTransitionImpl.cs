// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition ShrinkOut(
        Func<Vector2>? targetScale = null,
        AnimationSpec animationSpec = default
    )
    {
        return new ShrinkOutExitTransitionImpl(targetScale ?? (() => Vector2.zero), animationSpec);
    }

    public static IExitTransition ShrinkOutHorizontally(
        Func<float>? targetScaleX = null,
        AnimationSpec animationSpec = default
    )
    {
        var scaleDelegate = targetScaleX ?? (() => 0f);
        return new ShrinkOutExitTransitionImpl(() => new Vector2(scaleDelegate(), 1f), animationSpec);
    }


    public static IExitTransition ShrinkOutVertically(
        Func<float>? targetScaleY = null,
        AnimationSpec animationSpec = default
    )
    {
        var scaleDelegate = targetScaleY ?? (() => 0f);
        return new ShrinkOutExitTransitionImpl(() => new Vector2(1f, scaleDelegate()), animationSpec);
    }
}

internal class ShrinkOutExitTransitionImpl : IExitTransition
{
    private readonly Func<Vector2> _targetScale;
    private readonly AnimationSpec _animationSpec;

    public ShrinkOutExitTransitionImpl(Func<Vector2> targetScale, AnimationSpec animationSpec)
    {
        _targetScale = targetScale;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Scale(
                Vector2.Lerp(Vector2.one, _targetScale(), resolvedProgress)
            );
    }
}