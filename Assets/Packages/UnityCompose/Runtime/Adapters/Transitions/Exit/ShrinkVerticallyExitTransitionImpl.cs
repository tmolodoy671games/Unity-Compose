using System;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;

public class ShrinkVerticallyExitTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetHeight;
    private readonly AnimationSpec _animationSpec;
    private readonly Alignment.Vertical _shrinkTowards;
    private readonly bool _clip;

    public ShrinkVerticallyExitTransitionImpl(
        AnimationSpec animationSpec,
        Alignment.Vertical shrinkTowards,
        bool clip,
        Func<float, float> targetHeight
    )
    {
        _targetHeight = targetHeight;
        _animationSpec = animationSpec;
        _shrinkTowards = shrinkTowards;
        _clip = clip;
    }

    public void Apply(float timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var element = node.VisualElement();
        var child = element.GetOrNull(0);
        element.style.overflow = Overflow.Hidden;
        element.UserData()[this] = progress;
        if (child == null)
        {
            element.RegisterCallbackOnce<GeometryChangedEvent>(OnGeometryChanged);
            return;
        }

        UpdateParentSize(element, progress);
    }

    public void Revert(IReusableComposeNode node)
    {
        var element = node.VisualElement();
        element.style.maxHeight = StyleKeyword.None;
        element.UserData().Remove(this);
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(ShrinkVerticallyExitTransitionImpl other)
    {
        return _targetHeight.Equals(other._targetHeight) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ShrinkVerticallyExitTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_targetHeight, _animationSpec);
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        var element = evt.VisualElement();
        var child = element.GetOrNull(0);
        if (child == null)
            return;
        var progress = element.UserData().GetOrNull(this) as float? ?? 0f;
        UpdateParentSize(element, progress);
    }

    private void UpdateParentSize(VisualElement element, float progress)
    {
        var child = element.GetOrNull(0);
        if (child == null)
            return;
        var targetHeight = _targetHeight(child.resolvedStyle.height);
        var initialHeight = child.resolvedStyle.height;
        element.style.maxHeight = Mathf.LerpUnclamped(initialHeight, targetHeight, progress);
    }
}