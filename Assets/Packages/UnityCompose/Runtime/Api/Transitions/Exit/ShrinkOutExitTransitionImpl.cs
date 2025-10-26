// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition ShrinkOut(
        Func<Vector2>? targetScale = null,
        IEasing? easing = null
    )
    {
        return new ShrinkOutExitTransitionImpl(targetScale ?? (() => Vector2.zero), easing);
    }

    public static IExitTransition ShrinkOutHorizontally(
        Func<float>? targetScaleX = null,
        IEasing? easing = null
    )
    {
        var scaleDelegate = targetScaleX ?? (() => 0f);
        return new ShrinkOutExitTransitionImpl(() => new Vector2(scaleDelegate(), 1f), easing);
    }


    public static IExitTransition ShrinkOutVertically(
        Func<float>? targetScaleY = null,
        IEasing? easing = null
    )
    {
        var scaleDelegate = targetScaleY ?? (() => 0f);
        return new ShrinkOutExitTransitionImpl(() => new Vector2(1f, scaleDelegate()), easing);
    }
}

internal class ShrinkOutExitTransitionImpl : IExitTransition
{
    private readonly Func<Vector2> _targetScale;
    private readonly IEasing _easing;

    public ShrinkOutExitTransitionImpl(Func<Vector2> targetScale, IEasing? easing)
    {
        _targetScale = targetScale;
        _easing = easing ?? EaseInOut;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        var resolvedProgress = _easing.Transform(progress);
        return Modifier
            .Scale(
                Vector2.Lerp(Vector2.one, _targetScale(), resolvedProgress)
            );
    }
}