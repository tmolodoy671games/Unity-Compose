using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;

internal sealed class SlideOutExitTransitionImpl : IExitTransition
{
    private readonly Func<Offset, Offset> _targetOffset;
    private readonly AnimationSpec _animationSpec;

    public SlideOutExitTransitionImpl(Func<Offset, Offset> targetOffset, AnimationSpec animationSpec)
    {
        _targetOffset = targetOffset;
        _animationSpec = animationSpec;
    }

    public void Apply(float timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var initialOffset = new Offset();
        var targetOffset = _targetOffset(node.VisualElement().LayoutCoordinates().Size);
        node.VisualElement().style.translate = Offset.LerpUnclamped(initialOffset, targetOffset, progress).ToVector2();
    }

    public void Revert(IReusableComposeNode node)
    {
        node.VisualElement().style.translate = StyleKeyword.None;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(SlideOutExitTransitionImpl other)
    {
        return _targetOffset.Equals(other._targetOffset) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SlideOutExitTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_targetOffset, _animationSpec);
    }
}