// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition FadeIn(
        float initialAlpha = 0f,
        AnimationSpec animationSpec = default
    ) => new FadeInEnterTransitionImpl(initialAlpha, animationSpec);
}

internal class FadeInEnterTransitionImpl : IEnterTransition
{
    private readonly float _initialAlpha;
    private readonly AnimationSpec _animationSpec;

    public FadeInEnterTransitionImpl(float initialAlpha, AnimationSpec animationSpec)
    {
        _initialAlpha = initialAlpha;
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration();

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        return Modifier
            .Alpha(Mathf.Lerp(_initialAlpha, 1, resolvedProgress));
    }
}