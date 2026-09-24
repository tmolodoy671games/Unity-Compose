using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class OnMouseMoveModifierImpl : BaseUnityModifier<OnMouseMoveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onMouseMove;
    private readonly Action? _parameterlessOnMouseMove;
    private readonly EventCallback<MouseMoveEvent> _callback;

    public OnMouseMoveModifierImpl(Action<PointerMoveInfo> onMouseMove)
    {
        _onMouseMove = onMouseMove;
        _callback = OnMouseMove;
    }

    public OnMouseMoveModifierImpl(Action onMouseMove)
    {
        _parameterlessOnMouseMove = onMouseMove;
        _callback = OnMouseMove;
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

    protected override bool Equals(OnMouseMoveModifierImpl other)
    {
        return _onMouseMove == other._onMouseMove &&
               _parameterlessOnMouseMove == other._parameterlessOnMouseMove;
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        _onMouseMove?.Invoke(
            new PointerMoveInfo(
                Position: evt.mousePosition.ToOffset(),
                LocalPosition: evt.localMousePosition.ToOffset()
            )
        );
        _parameterlessOnMouseMove?.Invoke();
        evt.StopPropagation();
    }
}