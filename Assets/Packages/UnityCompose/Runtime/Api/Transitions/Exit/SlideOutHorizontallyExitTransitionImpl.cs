// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition SlideOutHorizontally(
        Func<float, float> targetOffsetX,
        IEasing? easing = null
    )
    {
        return new SlideOutHorizontallyExitTransitionImpl(targetOffsetX, easing);
    }
}

internal class SlideOutHorizontallyExitTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetOffsetX;
    private readonly IEasing _easing;

    public SlideOutHorizontallyExitTransitionImpl(Func<float, float> targetOffsetX, IEasing? easing)
    {
        _targetOffsetX = targetOffsetX;
        _easing = easing ?? EaseInOut;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        var resolvedProgress = _easing.Transform(progress);
        return Modifier
            .Position(
                left: Mathf.Lerp(
                    a: 0,
                    b: _targetOffsetX(parent.Width + parent.PaddingLeft),
                    t: resolvedProgress
                )
            );
    }
}