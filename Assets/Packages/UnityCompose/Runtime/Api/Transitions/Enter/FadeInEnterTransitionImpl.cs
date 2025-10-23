// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition FadeIn(
        float initialAlpha = 0f,
        AnimationCurve? curve = null
    ) => new FadeInEnterTransitionImpl(initialAlpha, curve);
}

internal class FadeInEnterTransitionImpl : IEnterTransition
{
    private readonly float _initialAlpha;
    private readonly AnimationCurve _curve;

    public FadeInEnterTransitionImpl(float initialAlpha, AnimationCurve? curve)
    {
        _initialAlpha = initialAlpha;
        _curve = curve ?? ComposeDefaults.DefaultCurve;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        var resolvedProgress = _curve.Evaluate(progress);
        return Modifier
            .Alpha(Mathf.Lerp(_initialAlpha, 1, resolvedProgress));
    }
}