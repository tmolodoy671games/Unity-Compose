// ReSharper disable CheckNamespace

using System.Threading;
using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class HorizontalScrollModifierImpl : BaseModifier<HorizontalScrollModifierImpl>
{
    private readonly IScrollState _state;
    private readonly float _scrollMultiplier;
    private readonly bool _reverseScrolling;
    private readonly IMutableInteractionSource? _interactionSource;
    private readonly EventCallback<WheelEvent> _callback;

    public HorizontalScrollModifierImpl(
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
    }

    public override void Apply(IReusableComposeNode node)
    {
        var contentContainer = new HorizontalScroll();
        contentContainer.RegisterCallback<GeometryChangedEvent>(it =>
            _state.ContentSize = it.VisualElement().contentRect.width
        );
        contentContainer.style.translate = new Vector2(_state.Value, 0);
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
        element.style.height = StyleKeyword.Null;
        node.CastTo<UnityReusableComposeNode>().RemoveContentContainer();
    }

    protected override bool Equals(HorizontalScrollModifierImpl other)
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
        _state.ViewportSize = element.contentRect.width;
        var offset = -evt.delta.y;
        if (offset.AlmostEquals(0f))
            return;
        var multiplier = _reverseScrolling ? -1 : 1;
        offset *= multiplier * _scrollMultiplier;
        _state.ScrollBy(offset);
        _interactionSource?.Emit(new IScrollInteraction.Scroll(offset));
    }
}

internal class HorizontalScroll : VisualElement
{
    public HorizontalScroll()
    {
        RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        pickingMode = PickingMode.Ignore;
        style.flexDirection = FlexDirection.Row;
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        style.position = Position.Absolute;
        if (parent == null)
            return;
        if (parent.style.height == StyleKeyword.Null)
            parent.style.height = resolvedStyle.height;
    }
}