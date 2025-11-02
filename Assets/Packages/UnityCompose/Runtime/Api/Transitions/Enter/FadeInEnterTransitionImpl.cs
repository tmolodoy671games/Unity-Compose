// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition FadeIn(
        float initialAlpha = 0f,
        float targetAlpha = 1f,
        AnimationSpec animationSpec = default
    ) => new FadeInEnterTransitionImpl(initialAlpha, targetAlpha, animationSpec);
}

internal class FadeInEnterTransitionImpl : IEnterTransition
{
    private readonly float _initialAlpha;
    private readonly float _targetAlpha;
    private readonly AnimationSpec _animationSpec;

    public FadeInEnterTransitionImpl(
        float initialAlpha,
        float targetAlpha,
        AnimationSpec animationSpec
    )
    {
        _initialAlpha = initialAlpha;
        _targetAlpha = targetAlpha;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        if (timeElapsed < _animationSpec.Delay)
            return Modifier;
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Alpha(Mathf.LerpUnclamped(_initialAlpha, _targetAlpha, resolvedProgress));
    }

    public IEnterTransition With(AnimationSpec animationSpec)
    {
        return new FadeInEnterTransitionImpl(
            initialAlpha: _initialAlpha,
            targetAlpha: _targetAlpha,
            animationSpec: animationSpec
        );
    }
}