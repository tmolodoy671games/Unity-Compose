using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;

internal class SlideOutVerticalTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetOffsetY;
    private readonly AnimationSpec _animationSpec;

    public SlideOutVerticalTransitionImpl(Func<float, float> targetOffsetY, AnimationSpec animationSpec)
    {
        _targetOffsetY = targetOffsetY;
        _animationSpec = animationSpec;
    }

    public void Apply(float timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var element = node.VisualElement();
        var targetOffsetY = _targetOffsetY(element.LayoutCoordinates().Size.Y);
        var targetOffset = Mathf.LerpUnclamped(0f, targetOffsetY, progress);
        element.style.translate = new Vector2(0f, targetOffset);
    }

    public void Revert(IReusableComposeNode node)
    {
        node.VisualElement().style.translate = StyleKeyword.None;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(SlideOutVerticalTransitionImpl other)
    {
        return _targetOffsetY.Equals(other._targetOffsetY) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SlideOutVerticalTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_targetOffsetY, _animationSpec);
    }
}