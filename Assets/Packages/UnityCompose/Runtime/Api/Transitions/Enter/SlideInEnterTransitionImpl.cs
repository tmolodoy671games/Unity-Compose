using System;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition SlideIn(
        Func<Vector2, Vector2> initialOffset,
        AnimationSpec animationSpec = default
    )
    {
        return new SlideInEnterTransitionImpl(it => initialOffset(it).x, it => initialOffset(it).y, animationSpec);
    }

    public static IEnterTransition SlideInHorizontally(
        Func<float, float> initialOffsetX,
        AnimationSpec animationSpec = default
    )
    {
        return new SlideInEnterTransitionImpl(it => initialOffsetX(it.x), null, animationSpec);
    }
    
    public static IEnterTransition SlideInVertically(
        Func<float, float> initialOffsetY,
        AnimationSpec animationSpec = default
    )
    {
        return new SlideInEnterTransitionImpl(null, it => initialOffsetY(it.y), animationSpec);
    }
}

internal class SlideInEnterTransitionImpl : IEnterTransition
{
    private readonly Func<Vector2, float>? _initialOffsetX;
    private readonly Func<Vector2, float>? _initialOffsetY;
    private readonly AnimationSpec _animationSpec;

    public SlideInEnterTransitionImpl(
        Func<Vector2, float>? initialOffsetX,
        Func<Vector2, float>? initialOffsetY,
        AnimationSpec animationSpec
    )
    {
        _initialOffsetX = initialOffsetX;
        _initialOffsetY = initialOffsetY;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration();

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        var result = Modifier;
        var parentSize = new Vector2(
            parent.Width + parent.PaddingLeft,
            parent.Height + parent.PaddingTop
        );
        if (_initialOffsetX != null)
        {
            result = result
                .Position(
                    left: Mathf.Lerp(
                        a: _initialOffsetX(parentSize),
                        b: 0,
                        t: resolvedProgress
                    )
                );
        }

        if (_initialOffsetY != null)
        {
            result = result
                .Position(
                    top: Mathf.Lerp(
                        a: _initialOffsetY(parentSize),
                        b: 0,
                        t: resolvedProgress
                    )
                );
        }

        return result;
    }
}