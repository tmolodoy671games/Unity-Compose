// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record OnPointerDownModifierImpl(
    Action<PointerClickInfo>? OnPointerDown,
    Action? ParameterlessOnPointerDown,
    int Button
) : UnityModifier
{
    private readonly EventCallback<PointerDownEvent> _callback = it =>
    {
        if (Button >= 0 && Button != it.button)
            return;
        OnPointerDown?.Invoke(
            new PointerClickInfo(
                Button: it.button,
                Position: it.position.ToVector2().ToOffset(),
                LocalPosition: it.localPosition.ToVector2().ToOffset()
            )
        );
        ParameterlessOnPointerDown?.Invoke();
        it.StopPropagation();
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