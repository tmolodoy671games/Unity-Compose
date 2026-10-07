// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record OnPointerCancelModifierImpl(
    Action<PointerClickInfo>? OnPointerCancel,
    Action? ParameterlessOnPointerCancel,
    int Button
) : UnityModifier
{
    private readonly EventCallback<PointerCancelEvent> _callback = evt =>
    {
        if (Button >= 0 && evt.button != Button)
            return;
        OnPointerCancel?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.position.ToVector2().ToOffset(),
                LocalPosition: evt.localPosition.ToVector2().ToOffset()
            )
        );
        ParameterlessOnPointerCancel?.Invoke();
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