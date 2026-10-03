using System;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class ExpandEnterTransitionImpl : IEnterTransition
{
    private readonly Func<FloatSize, FloatSize> _initialSize;
    private readonly AnimationSpec _animationSpec;
    private readonly Alignment _expandFrom;
    private readonly bool _clip;
    private readonly ReferenceKey _key;

    public ExpandEnterTransitionImpl(
        AnimationSpec animationSpec,
        Alignment expandFrom,
        bool clip,
        Func<FloatSize, FloatSize> initialSize
    )
    {
        _key = new ReferenceKey(this);
        _expandFrom = expandFrom;
        _clip = clip;
        _initialSize = initialSize;
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
        element.style.maxHeight = StyleKeyword.Null;
        element.UserData().Remove(_key);
    }

    public TimeSpan TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(ExpandEnterTransitionImpl other)
    {
        return _initialSize.Equals(other._initialSize) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ExpandEnterTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_initialSize, _animationSpec);
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        var element = evt.VisualElement();
        var progress = element.UserData().GetOrNull(_key) as float? ?? 0f;
        UpdateParentSize(element, progress);
    }

    private void UpdateParentSize(VisualElement element, float progress)
    {
        var child = element.GetOrNull(0);
        if (child == null)
            return;
        var childSize = child.LayoutCoordinates().Size;
        var initialSize = _initialSize(childSize);
        var targetSize = childSize;
        var size = FloatSize.LerpUnclamped(initialSize, targetSize, progress);
        element.style.maxWidth = size.Width;
        element.style.maxHeight = size.Height;
    }
}