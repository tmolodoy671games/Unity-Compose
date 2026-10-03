using System;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class ExpandHorizontallyEnterTransitionImpl : IEnterTransition
{
    private readonly Func<float, float> _initialWidth;
    private readonly AnimationSpec _animationSpec;
    private readonly bool _clip;
    private readonly Alignment.Horizontal _expandFrom;
    private readonly ReferenceKey _key;

    public ExpandHorizontallyEnterTransitionImpl(
        AnimationSpec animationSpec,
        Alignment.Horizontal expandFrom,
        bool clip,
        Func<float, float> initialWidth
    )
    {
        _key = new ReferenceKey(this);
        _clip = clip;
        _expandFrom = expandFrom;
        _initialWidth = initialWidth;
        _animationSpec = animationSpec;
    }

    public void Apply(TimeSpan timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var element = node.VisualElement();
        var child = element.GetOrNull(0);
        element.UserData()[_key] = progress;
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
        element.style.maxWidth = StyleKeyword.Null;
        element.UserData().Remove(_key);
    }

    public TimeSpan TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(ExpandHorizontallyEnterTransitionImpl other)
    {
        return _initialWidth.Equals(other._initialWidth) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ExpandHorizontallyEnterTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_initialWidth, _animationSpec);
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        var element = evt.VisualElement();
        var progress = element.UserData().GetOrNull(_key) as float? ?? 0;
        UpdateParentSize(element, progress);
    }

    private void UpdateParentSize(VisualElement element, float progress)
    {
        var child = element.GetOrNull(0);
        if (child == null)
            return;
        var initialWidth = _initialWidth(child.contentRect.width);
        var targetWidth = child.contentRect.width;
        element.style.maxWidth = Mathf.LerpUnclamped(initialWidth, targetWidth, progress);
    }
}