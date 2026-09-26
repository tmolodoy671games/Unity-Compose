using System;
using Compose.Net;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class ScaleInEnterTransitionImpl : IEnterTransition
{
    private readonly float _initialScale;
    private readonly AnimationSpec _animationSpec;

    public ScaleInEnterTransitionImpl(float initialScale, AnimationSpec animationSpec)
    {
        _initialScale = initialScale;
        _animationSpec = animationSpec;
    }

    public void Apply(float timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var scale = Mathf.LerpUnclamped(_initialScale, 1, progress);
        node.VisualElement().style.scale = new Vector2(scale, scale);
    }

    private bool Equals(ScaleInEnterTransitionImpl other)
    {
        return _initialScale.Equals(other._initialScale) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ScaleInEnterTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_initialScale, _animationSpec);
    }
}