using System;
using Compose.Net;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class FadeInEnterTransitionImpl : IEnterTransition
{
    private readonly float _initialAlpha;
    private readonly AnimationSpec _animationSpec;

    public FadeInEnterTransitionImpl(float initialAlpha, AnimationSpec animationSpec)
    {
        _initialAlpha = initialAlpha;
        _animationSpec = animationSpec;
    }

    public void Apply(float timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        node.VisualElement().style.opacity = Mathf.LerpUnclamped(_initialAlpha, 1f, progress);
    }

    private bool Equals(FadeInEnterTransitionImpl other)
    {
        return _initialAlpha.Equals(other._initialAlpha) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FadeInEnterTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_initialAlpha, _animationSpec);
    }
}