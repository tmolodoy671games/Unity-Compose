// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition SlideInHorizontally(
        Func<float, float> initialOffsetX,
        IEasing? easing = null
    )
    {
        return new SlideInEnterTransitionImpl(it => initialOffsetX(it.x), null, easing);
    }
}