// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition SlideInVertically(
        Func<float, float> initialOffsetY,
        AnimationCurve? animationCurve = null
    )
    {
        return new SlideInVerticallyEnterTransitionImpl(initialOffsetY, animationCurve);
    }
}

internal class SlideInVerticallyEnterTransitionImpl : IEnterTransition
{
    private readonly Func<float, float> _initialOffsetY;
    private readonly AnimationCurve _curve;

    public SlideInVerticallyEnterTransitionImpl(Func<float, float> initialOffsetY, AnimationCurve? curve)
    {
        _initialOffsetY = initialOffsetY;
        _curve = curve ?? ComposeDefaults.DefaultCurve;
    }

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
    {
        var resolvedProgress = _curve.Evaluate(progress);
        return Modifier
            .Then(
                scope.Position(
                    top: Mathf.Lerp(
                        a: _initialOffsetY(parent.Height + parent.PaddingTop),
                        b: 0,
                        t: resolvedProgress
                    )
                )
            );
    }
}