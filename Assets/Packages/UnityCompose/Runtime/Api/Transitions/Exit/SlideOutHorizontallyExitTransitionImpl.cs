// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition SlideOutHorizontally(
        Func<float, float> targetOffsetX,
        AnimationCurve? animationCurve = null
    )
    {
        return new SlideOutHorizontallyExitTransitionImpl(targetOffsetX, animationCurve);
    }
}

internal class SlideOutHorizontallyExitTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetOffsetX;
    private readonly AnimationCurve _curve;

    public SlideOutHorizontallyExitTransitionImpl(Func<float, float> targetOffsetX, AnimationCurve? curve)
    {
        _targetOffsetX = targetOffsetX;
        _curve = curve ?? ComposeDefaults.DefaultCurve;
    }

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
    {
        var resolvedProgress = _curve.Evaluate(progress);
        return Modifier
            .Then(
                scope.Position(
                    left: Mathf.Lerp(
                        a: 0,
                        b: _targetOffsetX(parent.Width + parent.PaddingLeft),
                        t: resolvedProgress
                    )
                )
            );
    }
}