// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using NUnit.Framework;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnScrollModifierImpl : UnityModifier<OnScrollModifierImpl>
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

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Increment();
        element.RegisterCallback(_callback);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Decrement();
        element.UnregisterCallback(_callback);
    }

    protected override bool Equals(OnScrollModifierImpl other)
    {
        return _callback == other._callback;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onScroll, _onOnVerticalScroll, _onOnHorizontalScroll);
    }

    private void OnScroll(WheelEvent evt)
    {
        _onScroll?.Invoke(evt.delta.ToVector2().ToOffset());
        if (evt.delta.x != 0)
            _onOnHorizontalScroll?.Invoke(evt.delta.x);
        if (evt.delta.y != 0)
            _onOnVerticalScroll?.Invoke(evt.delta.y);
    }
}