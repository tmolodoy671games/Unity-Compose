// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition FadeOut(
        float targetAlpha = 0f,
        IEasing? easing = null
    ) => new FadeOutExitTransitionImpl(targetAlpha, easing);
}

internal class FadeOutExitTransitionImpl : IExitTransition
{
    private readonly float _targetAlpha;
    private readonly IEasing _easing;

    public FadeOutExitTransitionImpl(float targetAlpha, IEasing? easing)
    {
        _targetAlpha = targetAlpha;
        _easing = easing ?? EaseInOut;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        var resolvedProgress = _easing.Transform(progress);
        return Modifier
            .Alpha(Mathf.Lerp(1, _targetAlpha, resolvedProgress));
    }
}