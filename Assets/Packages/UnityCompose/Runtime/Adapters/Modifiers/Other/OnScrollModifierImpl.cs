// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnScrollModifierImpl : BaseUnityModifier<OnScrollModifierImpl>
{
    private readonly Action<Offset>? _onScroll;
    private readonly Action<float>? _onOnVerticalScroll;
    private readonly Action<float>? _onOnHorizontalScroll;
    private readonly EventCallback<WheelEvent>? _callback;

    public OnScrollModifierImpl(
        Action<Offset>? onScroll = null,
        Action<float>? onVerticalScroll = null,
        Action<float>? onHorizontalScroll = null
    )
    {
        _onScroll = onScroll;
        _onOnVerticalScroll = onVerticalScroll;
        _onOnHorizontalScroll = onHorizontalScroll;
        _callback = OnScroll;
    }

    public override void Apply(VisualElement element)
    {
        element.PickingMode().Increment();
        element.RegisterCallback(_callback);
    }

    public override void Revert(VisualElement element)
    {
        element.PickingMode().Decrement();
        element.UnregisterCallback(_callback);
    }

    protected override bool Equals(OnScrollModifierImpl other)
    {
        return _callback == other._callback;
    }

    private void OnScroll(WheelEvent evt)
    {
        _onScroll?.Invoke(evt.delta.ToOffset());
        if (evt.delta.x != 0)
            _onOnHorizontalScroll?.Invoke(evt.delta.x);
        if (evt.delta.y != 0)
            _onOnVerticalScroll?.Invoke(evt.delta.y);
    }
}