// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition FadeOut(
        float targetAlpha = 0f,
        AnimationCurve? curve = null
    ) => new FadeOutExitTransitionImpl(targetAlpha, curve);
}

internal class FadeOutExitTransitionImpl : IExitTransition
{
    private readonly float _targetAlpha;
    private readonly AnimationCurve _curve;

    public FadeOutExitTransitionImpl(float targetAlpha, AnimationCurve? curve)
    {
        _targetAlpha = targetAlpha;
        _curve = curve ?? ComposeDefaults.DefaultCurve;
    }

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
    {
        var resolvedProgress = _curve.Evaluate(progress);
        return Modifier
            .Alpha(Mathf.Lerp(1, _targetAlpha, resolvedProgress));
    }
}