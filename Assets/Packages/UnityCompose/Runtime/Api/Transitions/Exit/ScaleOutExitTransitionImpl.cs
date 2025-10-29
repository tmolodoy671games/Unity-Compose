// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition ScaleOut(
        Optional<Vector2> initialScale = default,
        Optional<Vector2> targetScale = default,
        AnimationSpec animationSpec = default
    )
    {
        return new ScaleOutExitTransitionImpl(
            initialScale: initialScale.GetOrDefault(Vector2.one),
            targetScale: targetScale.GetOrDefault(Vector2.zero),
            animationSpec: animationSpec
        );
    }


    public static IExitTransition ScaleOut(
        float initialScale = 1f,
        float targetScale = 0f,
        AnimationSpec animationSpec = default
    )
    {
        return new ScaleOutExitTransitionImpl(
            initialScale: initialScale * Vector2.one,
            targetScale: targetScale * Vector2.one,
            animationSpec: animationSpec
        );
    }
}

internal class ScaleOutExitTransitionImpl : IExitTransition
{
    private readonly Vector2 _initialScale;
    private readonly Vector2 _targetScale;
    private readonly AnimationSpec _animationSpec;

    public ScaleOutExitTransitionImpl(
        Vector2 initialScale,
        Vector2 targetScale,
        AnimationSpec animationSpec
    )
    {
        _initialScale = initialScale;
        _targetScale = targetScale;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Scale(
                Vector2.Lerp(_initialScale, _targetScale, resolvedProgress)
            );
    }
}