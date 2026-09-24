using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class OnMouseUpModifierImpl : BaseUnityModifier<OnMouseUpModifierImpl>
{
    private readonly Action<PointerClickInfo>? _onMouseUp;
    private readonly Action? _parameterlessOnMouseUp;
    private readonly EventCallback<MouseUpEvent> _callback;
    private readonly int _button;

    public OnMouseUpModifierImpl(Action<PointerClickInfo> onMouseUp, int button)
    {
        _onMouseUp = onMouseUp;
        _button = button;
        _callback = OnMouseUp;
    }

    public OnMouseUpModifierImpl(Action onMouseUp, int button)
    {
        _parameterlessOnMouseUp = onMouseUp;
        _button = button;
        _callback = OnMouseUp;
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

    protected override bool Equals(OnMouseUpModifierImpl other)
    {
        return _onMouseUp == other._onMouseUp && 
               _parameterlessOnMouseUp == other._parameterlessOnMouseUp;
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        if (_button >= 0 && evt.button != _button)
            return;
        _onMouseUp?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.mousePosition.ToOffset(),
                LocalPosition: evt.localMousePosition.ToOffset()
            )
        );
        _parameterlessOnMouseUp?.Invoke();
        evt.StopPropagation();
    }
}