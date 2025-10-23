// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition SlideInHorizontally(
        Func<float, float> initialOffsetX,
        AnimationCurve? animationCurve = null
    )
    {
        return new SlideInHorizontallyEnterTransitionImpl(initialOffsetX, animationCurve);
    }
}

internal class SlideInHorizontallyEnterTransitionImpl : IEnterTransition
{
    private readonly Func<float, float> _initialOffsetX;
    private readonly AnimationCurve _curve;

    public SlideInHorizontallyEnterTransitionImpl(Func<float, float> initialOffsetX, AnimationCurve? curve)
    {
        _initialOffsetX = initialOffsetX;
        _curve = curve ?? ComposeDefaults.DefaultCurve;
    }

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
    {
        var resolvedProgress = _curve.Evaluate(progress);
        return Modifier
            .Left(
                Mathf.Lerp(
                    a: _initialOffsetX(parent.Width + parent.PaddingLeft),
                    b: 0,
                    t: resolvedProgress
                )
            );
    }
}