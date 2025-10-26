// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition ExpandIn(
        Func<Vector2>? initialScale = null,
        AnimationSpec animationSpec = default
    )
    {
        return new ExpandInEnterTransitionImpl(initialScale ?? (() => Vector2.zero), animationSpec);
    }

    public static IEnterTransition ExpandInHorizontally(
        Func<float>? initialScaleX = null,
        AnimationSpec animationSpec = default
    )
    {
        var scaleDelegate = initialScaleX ?? (() => 0f);
        return new ExpandInEnterTransitionImpl(() => new Vector2(scaleDelegate(), 1f), animationSpec);
    }


    public static IEnterTransition ExpandInVertically(
        Func<float>? initialScaleY = null,
        AnimationSpec animationSpec = default
    )
    {
        var scaleDelegate = initialScaleY ?? (() => 0f);
        return new ExpandInEnterTransitionImpl(() => new Vector2(1f, scaleDelegate()), animationSpec);
    }
}

internal class ExpandInEnterTransitionImpl : IEnterTransition
{
    private readonly Func<Vector2> _initialScale;
    private readonly AnimationSpec _animationSpec;

    public ExpandInEnterTransitionImpl(Func<Vector2> initialScale, AnimationSpec animationSpec)
    {
        _initialScale = initialScale;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration();

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Scale(
                Vector2.Lerp(_initialScale(), Vector2.one, resolvedProgress)
            );
    }
}