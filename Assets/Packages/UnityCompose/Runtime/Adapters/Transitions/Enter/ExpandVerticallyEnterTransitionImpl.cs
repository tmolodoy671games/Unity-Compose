using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class ExpandVerticallyEnterTransitionImpl : IEnterTransition
{
    private readonly Func<float, float> _initialHeight;
    private readonly AnimationSpec _animationSpec;
    private readonly Alignment.Vertical _expandFrom;
    private readonly bool _clip;

    public ExpandVerticallyEnterTransitionImpl(
        AnimationSpec animationSpec,
        Alignment.Vertical expandFrom,
        bool clip,
        Func<float, float> initialHeight
    )
    {
        _expandFrom = expandFrom;
        _clip = clip;
        _initialHeight = initialHeight;
        _animationSpec = animationSpec;
    }

    public void Apply(TimeSpan timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var element = node.VisualElement();
        var child = element.GetOrNull(0);
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
        element.style.maxHeight = StyleKeyword.Null;
        element.UserData().Remove(this);
    }

    public TimeSpan TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(ExpandVerticallyEnterTransitionImpl other)
    {
        return _initialHeight.Equals(other._initialHeight) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ExpandVerticallyEnterTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_initialHeight, _animationSpec);
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
        var initialHeight = _initialHeight(child.contentRect.height);
        var targetHeight = child.contentRect.height;
        element.style.maxHeight = Mathf.LerpUnclamped(initialHeight, targetHeight, progress);
    }
}