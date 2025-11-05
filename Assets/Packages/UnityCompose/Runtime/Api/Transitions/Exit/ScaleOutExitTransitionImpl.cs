// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition ScaleOut(
        Optional<Vector2> initialScale = default,
        Optional<Vector2> targetScale = default,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return new ScaleOutExitTransitionImpl(
            initialScale: initialScale.GetOrDefault(Vector2.one),
            targetScale: targetScale.GetOrDefault(Vector2.zero),
            animationSpec: animationSpec.GetOrDefault()
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
        _animationSpec = animationSpec;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, VisualElement parent)
    {
        if (parent == null) throw new ArgumentNullException(nameof(parent));
        if (timeElapsed < _animationSpec.Delay)
            return Modifier;
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Scale(
                Vector2.Lerp(_initialScale, _targetScale, resolvedProgress)
            );
    }

    public IExitTransition With(AnimationSpec animationSpec)
    {
        return new ScaleOutExitTransitionImpl(
            initialScale: _initialScale,
            targetScale: _targetScale,
            animationSpec: animationSpec
        );
    }
}