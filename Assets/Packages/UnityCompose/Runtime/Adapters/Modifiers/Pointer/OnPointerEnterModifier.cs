// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record OnPointerEnterModifierImpl(
    Action<PointerMoveInfo>? OnPointerEnter,
    Action? ParameterlessOnPointerEnter,
    int PointerId
) : UnityModifier
{
    private readonly EventCallback<PointerEnterEvent> _callback = evt =>
    {
        if (PointerId >= 0 && PointerId != evt.pointerId)
            return;
        OnPointerEnter?.Invoke(
            new PointerMoveInfo(
                Position: evt.position.ToVector2().ToOffset(),
                LocalPosition: evt.localPosition.ToVector2().ToOffset()
            )
        );
        ParameterlessOnPointerEnter?.Invoke();
        evt.StopPropagation();
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