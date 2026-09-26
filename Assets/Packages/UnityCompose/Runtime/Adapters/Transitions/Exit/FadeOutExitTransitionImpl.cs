using System;
using Compose.Net;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;

internal class FadeOutExitTransitionImpl : IExitTransition
{
    private readonly float _targetAlpha;
    private readonly AnimationSpec _animationSpec;

    public FadeOutExitTransitionImpl(float targetAlpha, AnimationSpec animationSpec)
    {
        _targetAlpha = targetAlpha;
        _animationSpec = animationSpec;
    }

    public void Apply(float timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        node.VisualElement().style.opacity = Mathf.LerpUnclamped(1, _targetAlpha, progress);
    }

    public void Revert(IReusableComposeNode node)
    {
        node.VisualElement().style.opacity = StyleKeyword.None;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(FadeOutExitTransitionImpl other)
    {
        return _targetAlpha.Equals(other._targetAlpha) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FadeOutExitTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_targetAlpha, _animationSpec);
    }
}