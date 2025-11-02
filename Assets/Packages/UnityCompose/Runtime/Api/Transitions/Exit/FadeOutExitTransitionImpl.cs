// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition FadeOut(
        float initialAlpha = 1f,
        float targetAlpha = 0f,
        AnimationSpec animationSpec = default
    ) => new FadeOutExitTransitionImpl(initialAlpha, targetAlpha, animationSpec);
}

internal class FadeOutExitTransitionImpl : IExitTransition
{
    private readonly float _initialAlpha;
    private readonly float _targetAlpha;
    private readonly AnimationSpec _animationSpec;

    public FadeOutExitTransitionImpl(
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
            .Alpha(Mathf.Lerp(_initialAlpha, _targetAlpha, resolvedProgress));
    }

    public IExitTransition With(AnimationSpec animationSpec)
    {
        return new FadeOutExitTransitionImpl(
            initialAlpha: _initialAlpha,
            targetAlpha: _targetAlpha,
            animationSpec: animationSpec
        );
    }
}