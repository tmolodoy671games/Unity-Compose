// ReSharper disable CheckNamespace

using System;
using System.Threading;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class HorizontalScrollModifierImpl : ContentContainerUnityModifier<HorizontalScrollModifierImpl>
{
    private readonly IScrollState _state;
    private readonly float _scrollMultiplier;
    private readonly bool _reverseScrolling;
    private readonly IMutableInteractionSource? _interactionSource;
    private readonly EventCallback<WheelEvent> _callback;
    private readonly EventCallback<GeometryChangedEvent> _onGeometryChanged;
    private readonly ReferenceKey _key;

    public HorizontalScrollModifierImpl(
        IScrollState state,
        float scrollMultiplier,
        bool reverseScrolling,
        IMutableInteractionSource? interactionSource
    )
    {
        _key = new ReferenceKey(this);
        _state = state;
        _scrollMultiplier = scrollMultiplier;
        _reverseScrolling = reverseScrolling;
        _interactionSource = interactionSource;
        _callback = OnWheelEvent;
        _onGeometryChanged = OnGeometryChanged;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement contentContainer,
        IStableList<IModifier> newModifiers
    )
    {
        contentContainer.style.flexDirection = FlexDirection.Row;
        contentContainer.RegisterCallback(_onGeometryChanged);
        contentContainer.style.translate = new Vector2(_state.Value, 0);
        element.RegisterCallback(_callback, TrickleDown.TrickleDown);
        element.PickingMode().Increment();
        element.UserData()[_key] = true;
        element.style.overflow = Overflow.Hidden;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement contentContainer,
        IStableList<IModifier> newModifiers
    )
    {
        element.UnregisterCallback(_callback, TrickleDown.TrickleDown);
        element.PickingMode().Decrement();
        element.UserData().Remove(_key);
        element.style.overflow = Overflow.Visible;
        element.style.height = StyleKeyword.Null;
        contentContainer.UnregisterCallback(_onGeometryChanged);
    }

    protected override bool Equals(HorizontalScrollModifierImpl other)
    {
        return _reverseScrolling == other._reverseScrolling &&
               _scrollMultiplier.AlmostEquals(other._scrollMultiplier) &&
               _interactionSource == other._interactionSource &&
               _callback == other._callback;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_state, _reverseScrolling, _scrollMultiplier, _interactionSource);
    }

    private void OnWheelEvent(WheelEvent evt)
    {
        var element = evt.VisualElement();
        if (!element.UserData().ContainsKey(_key))
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

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        var element = evt.VisualElement();
        _state.ContentSize = element.contentRect.width;
        element.style.position = Position.Absolute;
        if (element.parent == null)
            return;
        if (element.parent.style.height == StyleKeyword.Null)
            element.parent.style.height = element.resolvedStyle.height;
    }
}