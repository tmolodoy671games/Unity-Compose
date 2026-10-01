using System;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;

public class ShrinkHorizontallyExitTransitionImpl : IExitTransition
{
    private readonly Func<float, float> _targetWidth;
    private readonly AnimationSpec _animationSpec;
    private readonly Alignment.Horizontal _shrinkTowards;
    private readonly bool _clip;

    public ShrinkHorizontallyExitTransitionImpl(
        AnimationSpec animationSpec,
        Alignment.Horizontal shrinkTowards,
        bool clip,
        Func<float, float> targetWidth
    )
    {
        _targetWidth = targetWidth;
        _animationSpec = animationSpec;
        _shrinkTowards = shrinkTowards;
        _clip = clip;
    }

    public void Apply(TimeSpan timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var element = node.VisualElement();
        var child = element.GetOrNull(0);
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
        element.parent.style.maxWidth = StyleKeyword.Null;
    }

    public TimeSpan TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(ShrinkHorizontallyExitTransitionImpl other)
    {
        return _targetWidth.Equals(other._targetWidth) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ShrinkHorizontallyExitTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_targetWidth, _animationSpec);
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
        var targetWidth = _targetWidth(child.resolvedStyle.width);
        var initialWidth = child.resolvedStyle.width;
        element.style.maxWidth = Mathf.LerpUnclamped(initialWidth, targetWidth, progress);
    }
}