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
        return new SlideInEnterTransitionImpl(it => initialOffsetX(it.x), null, animationCurve);
    }
}