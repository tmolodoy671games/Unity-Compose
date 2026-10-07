using System;
using Compose.Net;
using NUnit.Framework;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal record OnMouseLeaveModifierImpl(
    Action<PointerMoveInfo>? OnMouseLeave,
    Action? ParameterlessOnMouseLeave
) : UnityModifier
{
    private readonly EventCallback<MouseLeaveEvent> _callback = evt =>
    {
        OnMouseLeave?.Invoke(
            new PointerMoveInfo(
                Position: evt.mousePosition.ToOffset(),
                LocalPosition: evt.localMousePosition.ToOffset()
            )
        );
        ParameterlessOnMouseLeave?.Invoke();
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