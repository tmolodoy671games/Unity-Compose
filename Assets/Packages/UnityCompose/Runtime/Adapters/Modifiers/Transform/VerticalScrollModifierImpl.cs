// ReSharper disable CheckNamespace

using System.Threading;
using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class VerticalScrollModifierImpl : BaseModifier<VerticalScrollModifierImpl>
{
    private readonly IScrollState _state;
    private readonly float _scrollMultiplier;
    private readonly bool _reverseScrolling;
    private readonly IMutableInteractionSource? _interactionSource;
    private readonly EventCallback<WheelEvent> _callback;
    private readonly EventCallback<GeometryChangedEvent> _onGeometryChanged;

    public VerticalScrollModifierImpl(
        IScrollState state,
        float scrollMultiplier,
        bool reverseScrolling,
        IMutableInteractionSource? interactionSource
    )
    {
        _state = state;
        _scrollMultiplier = scrollMultiplier;
        _reverseScrolling = reverseScrolling;
        _interactionSource = interactionSource;
        _callback = OnWheelEvent;
        _onGeometryChanged = OnGeometryChanged;
    }

    public override void Apply(IReusableComposeNode node)
    {
        var unityNode = node.CastTo<UnityReusableComposeNode>();
        var contentContainer = unityNode.SetupContentContainer();
        contentContainer.RegisterCallback(_onGeometryChanged);
        contentContainer.style.translate = new Vector2(0, _state.Value);
        var element = node.VisualElement();
        element.RegisterCallback(_callback, TrickleDown.TrickleDown);
        element.PickingMode().Increment();
        element.UserData()[this] = true;
        element.style.overflow = Overflow.Hidden;
    }

    public override void Revert(IReusableComposeNode node)
    {
        var unityNode = node.CastTo<UnityReusableComposeNode>();
        var element = node.VisualElement();
        element.UnregisterCallback(_callback, TrickleDown.TrickleDown);
        element.PickingMode().Decrement();
        element.UserData().Remove(this);
        element.style.overflow = Overflow.Visible;
        element.style.width = StyleKeyword.Null;
        
        var contentContainer = unityNode.SetupContentContainer();
        contentContainer.UnregisterCallback(_onGeometryChanged);
        unityNode.RemoveContentContainer();
    }

    protected override bool Equals(VerticalScrollModifierImpl other)
    {
        return _reverseScrolling == other._reverseScrolling &&
               _interactionSource == other._interactionSource &&
               _callback == other._callback;
    }

    private void OnWheelEvent(WheelEvent evt)
    {
        var element = evt.VisualElement();
        if (!element.UserData().ContainsKey(this))
            return;
        _state.ViewportSize = element.contentRect.height;
        var offset = -evt.delta.y;
        if (offset.AlmostEquals(0f))
            return;
        var multiplier = _reverseScrolling ? -1 : 1;
        offset *= multiplier * _scrollMultiplier;
        _state.ScrollBy(offset);
        _interactionSource?.Emit(new IScrollInteraction.Scroll(offset));
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        var element = evt.VisualElement();
        element.style.position = Position.Absolute;
        _state.ContentSize = element.contentRect.height;
        if (element.parent == null)
            return;
        if (element.parent.style.width == StyleKeyword.Null)
            element.parent.style.width = element.resolvedStyle.width;
    }
}
