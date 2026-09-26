using System;
using Compose.Net;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class SlideInVerticallyTransitionImpl : IEnterTransition
{
    private readonly Func<float, float> _initialOffsetY;
    private readonly AnimationSpec _animationSpec;

    public SlideInVerticallyTransitionImpl(Func<float, float> initialOffsetY, AnimationSpec animationSpec)
    {
        _initialOffsetY = initialOffsetY;
        _animationSpec = animationSpec;
    }

    public void Apply(float timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var element = node.VisualElement();
        var initialOffset = _initialOffsetY(progress);
        var offset = Mathf.LerpUnclamped(initialOffset, 0, progress);
        element.style.translate = new Vector2(0f, offset);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SlideInVerticallyTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_initialOffsetY, _animationSpec);
    }
    
    private bool Equals(SlideInVerticallyTransitionImpl other)
    {
        return _initialOffsetY.Equals(other._initialOffsetY) && _animationSpec.Equals(other._animationSpec);
    }
}