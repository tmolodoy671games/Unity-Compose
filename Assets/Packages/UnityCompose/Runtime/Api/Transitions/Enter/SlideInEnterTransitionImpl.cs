using System;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition SlideIn(
        Func<Vector2, Vector2> initialOffset,
        IEasing? easing = null
    )
    {
        return new SlideInEnterTransitionImpl(it => initialOffset(it).x, it => initialOffset(it).y, easing);
    }
}

internal class SlideInEnterTransitionImpl : IEnterTransition
{
    private readonly Func<Vector2, float>? _initialOffsetX;
    private readonly Func<Vector2, float>? _initialOffsetY;
    private readonly IEasing _easing;

    public SlideInEnterTransitionImpl(
        Func<Vector2, float>? initialOffsetX,
        Func<Vector2, float>? initialOffsetY,
        IEasing? easing
    )
    {
        _initialOffsetX = initialOffsetX;
        _initialOffsetY = initialOffsetY;
        _easing = easing ?? EaseInOut;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        var resolvedProgress = _easing.Transform(progress);
        var result = Modifier;
        var parentSize = new Vector2(
            parent.Width + parent.PaddingLeft,
            parent.Height + parent.PaddingTop
        );
        if (_initialOffsetX != null)
        {
            result = result
                .Position(
                    left: Mathf.Lerp(
                        a: _initialOffsetX(parentSize),
                        b: 0,
                        t: resolvedProgress
                    )
                );
        }

        if (_initialOffsetY != null)
        {
            result = result
                .Position(
                    top: Mathf.Lerp(
                        a: _initialOffsetY(parentSize),
                        b: 0,
                        t: resolvedProgress
                    )
                );
        }

        return result;
    }
}