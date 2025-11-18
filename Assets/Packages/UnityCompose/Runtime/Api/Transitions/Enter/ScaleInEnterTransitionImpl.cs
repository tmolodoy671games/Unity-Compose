// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition ScaleIn(
        Optional<Vector2> initialScale = default,
        Optional<Vector2> targetScale = default,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return new ScaleInEnterTransitionImpl(
            initialScale: initialScale.GetOrDefault(Vector2.zero),
            targetScale: targetScale.GetOrDefault(Vector2.one),
            animationSpec: animationSpec.GetOrDefault()
        );
    }
}

internal class ScaleInEnterTransitionImpl : IEnterTransition
{
    private readonly Vector2 _initialScale;
    private readonly Vector2 _targetScale;
    private readonly AnimationSpec _animationSpec;

    public ScaleInEnterTransitionImpl(
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
        if (timeElapsed <= _animationSpec.Delay)
            return Modifier.Alpha(0f);
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Scale(
                Vector2.LerpUnclamped(
                    _initialScale,
                    _targetScale,
                    resolvedProgress
                )
            );
    }

    public IEnterTransition With(AnimationSpec animationSpec)
    {
        return new ScaleInEnterTransitionImpl(
            initialScale: _initialScale,
            targetScale: _targetScale,
            animationSpec: animationSpec
        );
    }
}