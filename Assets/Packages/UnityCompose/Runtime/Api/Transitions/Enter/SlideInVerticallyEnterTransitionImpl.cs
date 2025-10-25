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
        return new SlideInEnterTransitionImpl(null, it => initialOffsetY(it.y), animationCurve);
    }
}