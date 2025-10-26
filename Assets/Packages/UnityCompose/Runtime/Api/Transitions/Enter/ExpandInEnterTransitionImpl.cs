// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition ExpandIn(
        Func<Vector2>? initialScale = null,
        IEasing? easing = null
    )
    {
        return new ExpandInEnterTransitionImpl(initialScale ?? (() => Vector2.zero), easing);
    }

    public static IEnterTransition ExpandInHorizontally(
        Func<float>? initialScaleX = null,
        IEasing? easing = null
    )
    {
        var scaleDelegate = initialScaleX ?? (() => 0f);
        return new ExpandInEnterTransitionImpl(() => new Vector2(scaleDelegate(), 1f), easing);
    }


    public static IEnterTransition ExpandInVertically(
        Func<float>? initialScaleY = null,
        IEasing? easing = null
    )
    {
        var scaleDelegate = initialScaleY ?? (() => 0f);
        return new ExpandInEnterTransitionImpl(() => new Vector2(1f, scaleDelegate()), easing);
    }
}

internal class ExpandInEnterTransitionImpl : IEnterTransition
{
    private readonly Func<Vector2> _initialScale;
    private readonly IEasing _easing;

    public ExpandInEnterTransitionImpl(Func<Vector2> initialScale, IEasing? easing)
    {
        _initialScale = initialScale;
        _easing = easing ?? EaseInOut;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        var resolvedProgress = _easing.Transform(progress);
        return Modifier
            .Scale(
                Vector2.Lerp(_initialScale(), Vector2.one, resolvedProgress)
            );
    }
}