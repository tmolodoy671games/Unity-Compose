using System;
using Compose.Net;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;

internal record ShrinkHorizontallyExitTransitionImpl(
    AnimationSpec AnimationSpec,
    bool Clip,
    Func<float, float> TargetWidth
) : IExitTransition
{
    public TimeSpan TotalDuration => AnimationSpec.TotalDuration;

    public void Apply(TimeSpan timeElapsed, IReusableComposeNode node)
    {
        var element = node.VisualElement();
        var progress = AnimationSpec.GetProgress(timeElapsed);
        var width = element.layout.width;
        if (width != 0 && !float.IsNaN(width))
        {
            progress = Mathf.LerpUnclamped(width, TargetWidth(width), progress);
            progress /= width;
        }

        element.style.flexShrink = 0;
        element.parent.style.maxWidth = ShrinkUtils.GetLength(progress);
        if (Clip)
            element.Clip().Increment();
    }

    public void Revert(IReusableComposeNode node)
    {
        var element = node.VisualElement();
        element.style.flexShrink = StyleKeyword.Null;
        element.style.maxWidth = StyleKeyword.Null;
        if (Clip)
            element.Clip().Decrement();
    }
}