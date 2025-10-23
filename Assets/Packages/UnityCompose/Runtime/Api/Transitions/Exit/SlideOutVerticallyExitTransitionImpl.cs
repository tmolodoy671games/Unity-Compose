// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition SlideOutVertically(
        Func<float, float> targetOffsetY,
        AnimationCurve? animationCurve = null
    )
    {
        return new SlideOutVerticallyExitTransitionImpl(targetOffsetY, animationCurve);
    }
}

internal class SlideOutVerticallyExitTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetOffsetY;
    private readonly AnimationCurve _curve;

    public SlideOutVerticallyExitTransitionImpl(Func<float, float> targetOffsetY, AnimationCurve? curve)
    {
        _targetOffsetY = targetOffsetY;
        _curve = curve ?? ComposeDefaults.DefaultCurve;
    }

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
    {
        var resolvedProgress = _curve.Evaluate(progress);
        return Modifier
            .Top(
                Mathf.Lerp(
                    a: 0,
                    b: _targetOffsetY(parent.Height + parent.PaddingTop),
                    t: resolvedProgress
                )
            );
    }
}