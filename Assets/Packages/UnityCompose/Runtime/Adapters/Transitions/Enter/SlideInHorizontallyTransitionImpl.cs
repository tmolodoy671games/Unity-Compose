using System;
using Compose.Net;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class SlideInHorizontallyTransitionImpl : IEnterTransition
{
    private readonly Func<float, float> _initialOffsetX;
    private readonly AnimationSpec _animationSpec;

    public SlideInHorizontallyTransitionImpl(Func<float, float> initialOffsetX, AnimationSpec animationSpec)
    {
        _initialOffsetX = initialOffsetX;
        _animationSpec = animationSpec;
    }

    public void Apply(float timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var element = node.VisualElement();
        var initialOffset = _initialOffsetX(element.LayoutCoordinates().Size.X);
        var offset = Mathf.LerpUnclamped(initialOffset, 0, progress);
        element.style.translate = new Vector2(offset, 0f);
    }

    public void Revert(IReusableComposeNode node)
    {
        node.VisualElement().style.translate = StyleKeyword.None;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SlideInHorizontallyTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_initialOffsetX, _animationSpec);
    }
    
    private bool Equals(SlideInHorizontallyTransitionImpl other)
    {
        return _initialOffsetX.Equals(other._initialOffsetX) && _animationSpec.Equals(other._animationSpec);
    }
}