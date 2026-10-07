// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record OnPointerLeaveModifierImpl(
    Action<PointerMoveInfo>? OnPointerLeave,
    Action? ParameterlessOnPointerLeave,
    int PointerId
) : UnityModifier
{
    private readonly EventCallback<PointerLeaveEvent> _callback = evt =>
    {
        if (PointerId >= 0 && PointerId != evt.pointerId)
            return;
        OnPointerLeave?.Invoke(
            new PointerMoveInfo(
                Position: evt.position.ToVector2().ToOffset(),
                LocalPosition: evt.localPosition.ToVector2().ToOffset()
            )
        );
        ParameterlessOnPointerLeave?.Invoke();
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