using System;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;

public class ShrinkOutExitTransitionImpl : IExitTransition
{
    private readonly Func<FloatSize, FloatSize> _targetSize;
    private readonly AnimationSpec _animationSpec;
    private readonly Alignment _shrinkTowards;
    private readonly bool _clip;

    public ShrinkOutExitTransitionImpl(
        AnimationSpec animationSpec,
        Alignment shrinkTowards,
        bool clip,
        Func<FloatSize, FloatSize> targetSize
    )
    {
        _targetSize = targetSize;
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
        element.parent.style.maxWidth = StyleKeyword.None;
        element.parent.style.maxHeight = StyleKeyword.None;
    }

    public TimeSpan TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(ShrinkOutExitTransitionImpl other)
    {
        return _targetSize.Equals(other._targetSize) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ShrinkOutExitTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_targetSize, _animationSpec);
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
        var childSize = child.LayoutCoordinates().Size;
        var targetSize = _targetSize(childSize);
        var initialSize = childSize;
        var size = FloatSize.LerpUnclamped(initialSize, targetSize, progress);
        element.style.maxWidth = size.Width;
        element.style.maxHeight = size.Height;
    }
}