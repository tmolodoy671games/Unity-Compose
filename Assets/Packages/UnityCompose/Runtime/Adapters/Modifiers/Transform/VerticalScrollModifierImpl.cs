// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class VerticalScrollModifierImpl : BaseModifier<VerticalScrollModifierImpl>
{
    private readonly IScrollState _state;
    private readonly bool _reverseScrolling;
    private readonly IMutableInteractionSource? _interactionSource;
    private readonly EventCallback<WheelEvent> _callback;

    public VerticalScrollModifierImpl(
        IScrollState state,
        bool reverseScrolling,
        IMutableInteractionSource? interactionSource
    )
    {
        _state = state;
        _reverseScrolling = reverseScrolling;
        _interactionSource = interactionSource;
        _callback = OnWheelEvent;
    }

    public override void Apply(IReusableComposeNode node)
    {
        var contentContainer = new Scrollable();
        contentContainer.RegisterCallback<GeometryChangedEvent>(it =>
            _state.ContentSize = it.VisualElement().contentRect.height
        );
        contentContainer.style.translate = new Vector2(0, _state.Value);
        var element = node.VisualElement();
        element.RegisterCallback(_callback, TrickleDown.TrickleDown);
        element.PickingMode().Increment();
        element.UserData()[this] = true;
        element.style.overflow = Overflow.Hidden;
        node.CastTo<UnityReusableComposeNode>().SetContentContainer(contentContainer);
    }

    public override void Revert(IReusableComposeNode node)
    {
        var element = node.VisualElement();
        element.UnregisterCallback(_callback, TrickleDown.TrickleDown);
        element.PickingMode().Decrement();
        element.UserData().Remove(this);
        element.style.overflow = Overflow.Visible;
        element.style.width = StyleKeyword.Null;
        node.CastTo<UnityReusableComposeNode>().RemoveContentContainer();
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
        offset *= multiplier * 50;
        _state.ScrollBy(offset);
        _interactionSource?.Emit(new IScrollInteraction.Scroll(offset));
    }
}

internal class Scrollable : VisualElement
{
    public Scrollable()
    {
        RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        pickingMode = PickingMode.Ignore;
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        style.position = Position.Absolute;
        if (parent == null)
            return;
        if (parent.style.width == StyleKeyword.Null)
            parent.style.width = resolvedStyle.width;
    }
}