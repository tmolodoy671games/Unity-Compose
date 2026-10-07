using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal record OnMouseUpModifierImpl(
    Action<PointerClickInfo>? OnMouseUp,
    Action? ParameterlessOnMouseUp,
    int Button
) : UnityModifier
{
    private readonly EventCallback<MouseUpEvent> _callback = evt =>
    {
        if (Button >= 0 && evt.button != Button)
            return;
        OnMouseUp?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.mousePosition.ToOffset(),
                LocalPosition: evt.localMousePosition.ToOffset()
            )
        );
        ParameterlessOnMouseUp?.Invoke();
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