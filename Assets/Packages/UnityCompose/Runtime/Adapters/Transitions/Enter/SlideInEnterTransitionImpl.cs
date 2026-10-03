using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class SlideInEnterTransitionImpl : IEnterTransition
{
    private readonly Func<Size, Offset> _initialOffset;
    private readonly AnimationSpec _animationSpec;

    public SlideInEnterTransitionImpl(
        AnimationSpec animationSpec,
        Func<Size, Offset> initialOffset
    )
    {
        _initialOffset = initialOffset;
        _animationSpec = animationSpec;
    }

    public void Apply(TimeSpan timeElapsed, IReusableComposeNode node)
    {
        var progress = _animationSpec.GetProgress(timeElapsed);
        var initialOffset = _initialOffset(node.VisualElement().LayoutCoordinates().Size);
        var targetOffset = new Offset(0, 0);
        var offset = Offset.LerpUnclamped(initialOffset, targetOffset, progress);
        node.VisualElement().style.translate = offset.ToVector2();
    }

    public void Revert(IReusableComposeNode node)
    {
        node.VisualElement().style.translate = StyleKeyword.None;
    }

    public TimeSpan TotalDuration => _animationSpec.TotalDuration;

    private bool Equals(SlideInEnterTransitionImpl other)
    {
        return _initialOffset.Equals(other._initialOffset) && _animationSpec.Equals(other._animationSpec);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SlideInEnterTransitionImpl)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_initialOffset, _animationSpec);
    }
}