using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class OnMouseEnterModifierImpl : BaseUnityModifier<OnMouseEnterModifierImpl>
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