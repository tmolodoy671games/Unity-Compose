using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class OnMouseEnterModifierImpl : UnityModifier<OnMouseEnterModifierImpl>
{
    private readonly Action? _parameterlessOnMouseEnter;
    private readonly Action<PointerMoveInfo>? _onMouseEnter;
    private readonly EventCallback<MouseEnterEvent> _callback;

    public OnMouseEnterModifierImpl(Action<PointerMoveInfo> onMouseEnter)
    {
        _onMouseEnter = onMouseEnter;
        _callback = OnMouseEnterEvent;
    }

    public OnMouseEnterModifierImpl(Action onMouseEnter)
    {
        _parameterlessOnMouseEnter = onMouseEnter;
        _callback = OnMouseEnterEvent;
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

    public override int GetHashCode()
    {
        return HashCode.Combine(_onMouseEnter, _parameterlessOnMouseEnter);
    }

    protected override bool Equals(OnMouseEnterModifierImpl other)
    {
        return _onMouseEnter == other._onMouseEnter &&
               _parameterlessOnMouseEnter == other._parameterlessOnMouseEnter;
    }

    private void OnMouseEnterEvent(MouseEnterEvent evt)
    {
        _onMouseEnter?.Invoke(
            new PointerMoveInfo(
                Position: evt.mousePosition.ToOffset(),
                LocalPosition: evt.localMousePosition.ToOffset()
            )
        );
        _parameterlessOnMouseEnter?.Invoke();
        evt.StopPropagation();
    }
}