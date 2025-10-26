// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition SlideOutVertically(
        Func<float, float> targetOffsetY,
        IEasing? easing = null
    )
    {
        return new SlideOutVerticallyExitTransitionImpl(targetOffsetY, easing);
    }
}

internal class SlideOutVerticallyExitTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetOffsetY;
    private readonly IEasing _easing;

    public SlideOutVerticallyExitTransitionImpl(Func<float, float> targetOffsetY, IEasing? easing)
    {
        _targetOffsetY = targetOffsetY;
        _easing = easing ?? EaseInOut;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        var resolvedProgress = _easing.Transform(progress);
        return Modifier
            .Position(
                top: Mathf.Lerp(
                    a: 0,
                    b: _targetOffsetY(parent.Height + parent.PaddingTop),
                    t: resolvedProgress
                )
            );
    }
}