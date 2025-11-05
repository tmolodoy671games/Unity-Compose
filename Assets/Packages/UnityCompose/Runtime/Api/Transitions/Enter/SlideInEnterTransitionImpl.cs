using System;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition SlideIn(
        Func<Vector2, Vector2> initialOffset,
        Func<Vector2, Vector2>? targetOffset = null,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return new SlideInEnterTransitionImpl(
            initialOffsetX: it => initialOffset(it).x,
            initialOffsetY: it => initialOffset(it).y,
            targetOffsetX: targetOffset != null ? it => targetOffset(it).x : null,
            targetOffsetY: targetOffset != null ? it => targetOffset(it).y : null,
            animationSpec: animationSpec.GetOrDefault()
        );
    }

    public static IEnterTransition SlideInHorizontally(
        Func<float, float> initialOffsetX,
        Func<float, float>? targetOffsetX = null,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return new SlideInEnterTransitionImpl(
            initialOffsetX: it => initialOffsetX(it.x),
            targetOffsetX: targetOffsetX != null ? it => targetOffsetX(it.x) : null,
            initialOffsetY: null,
            targetOffsetY: null,
            animationSpec: animationSpec.GetOrDefault()
        );
    }

    public static IEnterTransition SlideInVertically(
        Func<float, float> initialOffsetY,
        Func<float, float>? targetOffsetY = null,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return new SlideInEnterTransitionImpl(
            initialOffsetX: null,
            targetOffsetX: null,
            initialOffsetY: it => initialOffsetY(it.y),
            targetOffsetY: targetOffsetY != null ? it => targetOffsetY(it.x) : null,
            animationSpec: animationSpec.GetOrDefault()
        );
    }
}

internal class SlideInEnterTransitionImpl : IEnterTransition
{
    private readonly Func<Vector2, float>? _initialOffsetX;
    private readonly Func<Vector2, float>? _initialOffsetY;
    private readonly Func<Vector2, float>? _targetOffsetX;
    private readonly Func<Vector2, float>? _targetOffsetY;
    private readonly AnimationSpec _animationSpec;

    public SlideInEnterTransitionImpl(
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
        _animationSpec = animationSpec;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, VisualElement parent)
    {
        if (timeElapsed < _animationSpec.Delay)
            return Modifier;
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        var result = Modifier;
        var parentSize = new Vector2(
            parent.resolvedStyle.width + parent.resolvedStyle.paddingLeft + parent.resolvedStyle.paddingRight,
            parent.resolvedStyle.height + parent.resolvedStyle.paddingTop + parent.resolvedStyle.paddingBottom
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

    public IEnterTransition With(AnimationSpec animationSpec)
    {
        return new SlideInEnterTransitionImpl(
            initialOffsetX: _initialOffsetX,
            initialOffsetY: _initialOffsetY,
            targetOffsetX: _targetOffsetX,
            targetOffsetY: _targetOffsetY,
            animationSpec: animationSpec
        );
    }
}