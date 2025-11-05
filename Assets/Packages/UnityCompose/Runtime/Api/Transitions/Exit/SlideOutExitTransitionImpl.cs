// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition SlideOut(
        Func<Vector2, Vector2> targetOffset,
        Func<Vector2, Vector2>? initialOffset = null,
        AnimationSpec animationSpec = default
    )
    {
        return new SlideOutExitTransitionImpl(
            initialOffsetX: initialOffset != null ? it => initialOffset(it).x : _ => 0,
            initialOffsetY: initialOffset != null ? it => initialOffset(it).y : _ => 0,
            targetOffsetX: it => targetOffset(it).x,
            targetOffsetY: it => targetOffset(it).y,
            animationSpec: animationSpec
        );
    }

    public static IExitTransition SlideOutHorizontally(
        Func<float, float> targetOffsetX,
        Func<float, float>? initialOffsetX = null,
        AnimationSpec animationSpec = default
    )
    {
        return new SlideOutExitTransitionImpl(
            initialOffsetX: initialOffsetX != null ? it => initialOffsetX(it.x) : _ => 0,
            targetOffsetX: it => targetOffsetX(it.x),
            initialOffsetY: null,
            targetOffsetY: null,
            animationSpec: animationSpec
        );
    }

    public static IExitTransition SlideOutVertically(
        Func<float, float> targetOffsetY,
        Func<float, float>? initialOffsetY = null,
        AnimationSpec animationSpec = default
    )
    {
        return new SlideOutExitTransitionImpl(
            initialOffsetX: null,
            targetOffsetX: null,
            initialOffsetY: initialOffsetY != null ? it => initialOffsetY(it.y) : _ => 0,
            targetOffsetY: it => targetOffsetY(it.y),
            animationSpec: animationSpec
        );
    }
}

internal class SlideOutExitTransitionImpl : IExitTransition
{
    private readonly Func<Vector2, float>? _initialOffsetX;
    private readonly Func<Vector2, float>? _initialOffsetY;
    private readonly Func<Vector2, float>? _targetOffsetX;
    private readonly Func<Vector2, float>? _targetOffsetY;
    private readonly AnimationSpec _animationSpec;

    public SlideOutExitTransitionImpl(
        Func<Vector2, float>? initialOffsetX,
        Func<Vector2, float>? initialOffsetY,
        Func<Vector2, float>? targetOffsetX,
        Func<Vector2, float>? targetOffsetY,
        AnimationSpec animationSpec
    )
    {
        _initialOffsetX = initialOffsetX;
        _initialOffsetY = initialOffsetY;
        _targetOffsetX = targetOffsetX;
        _targetOffsetY = targetOffsetY;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        if (timeElapsed < _animationSpec.Delay)
            return Modifier;
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        var result = Modifier;
        var parentSize = new Vector2(
            parent.Width + parent.PaddingLeft,
            parent.Height + parent.PaddingTop
        );
        if (_initialOffsetX != null)
        {
            result = result
                .Offset(
                    x: Mathf.LerpUnclamped(
                        a: _initialOffsetX(parentSize),
                        b: _targetOffsetX?.Invoke(parentSize) ?? 0,
                        t: resolvedProgress
                    )
                );
        }

        if (_initialOffsetY != null)
        {
            result = result
                .Offset(
                    y: Mathf.LerpUnclamped(
                        a: _initialOffsetY(parentSize),
                        b: _targetOffsetY?.Invoke(parentSize) ?? 0,
                        t: resolvedProgress
                    )
                );
        }

        return result;
    }

    public IExitTransition With(AnimationSpec animationSpec)
    {
        return new SlideOutExitTransitionImpl(
            initialOffsetX: _initialOffsetX,
            targetOffsetX: _targetOffsetX,
            initialOffsetY: _initialOffsetY,
            targetOffsetY: _targetOffsetY,
            animationSpec: animationSpec
        );
    }
}