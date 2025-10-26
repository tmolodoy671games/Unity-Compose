// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition FadeIn(
        float initialAlpha = 0f,
        IEasing? easing = null
    ) => new FadeInEnterTransitionImpl(initialAlpha, easing);
}

internal class FadeInEnterTransitionImpl : IEnterTransition
{
    private readonly float _initialAlpha;
    private readonly IEasing _easing;

    public FadeInEnterTransitionImpl(float initialAlpha, IEasing? easing)
    {
        _initialAlpha = initialAlpha;
        _easing = easing ?? EaseInOut;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        var resolvedProgress = _easing.Transform(progress);
        return Modifier
            .Alpha(Mathf.Lerp(_initialAlpha, 1, resolvedProgress));
    }
}