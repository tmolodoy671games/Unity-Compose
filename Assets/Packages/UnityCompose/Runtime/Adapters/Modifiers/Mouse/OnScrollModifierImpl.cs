// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using NUnit.Framework;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record OnScrollModifierImpl(
    Action<Offset>? OnScroll,
    Action<float>? OnOnHorizontalScroll,
    Action<float>? OnOnVerticalScroll
) : UnityModifier
{
    private readonly EventCallback<WheelEvent>? _callback = evt =>
    {
        OnScroll?.Invoke(evt.delta.ToVector2().ToOffset());
        if (evt.delta.x != 0)
            OnOnHorizontalScroll?.Invoke(evt.delta.x);
        if (evt.delta.y != 0)
            OnOnVerticalScroll?.Invoke(evt.delta.y);
    };

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
}