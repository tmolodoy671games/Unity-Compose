// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerDownModifierImpl : BaseUnityModifier<OnPointerDownModifierImpl>
{
    private readonly Action? _parameterlessOnPointerDown;
    private readonly Action<PointerClickInfo>? _onPointerDown;
    private readonly EventCallback<PointerDownEvent> _callback;
    private readonly int _button;

    public OnPointerDownModifierImpl(Action<PointerClickInfo> onPointerDown, int button)
    {
        _onPointerDown = onPointerDown;
        _button = button;
        _callback = OnPointerDown;
    }

    public OnPointerDownModifierImpl(Action onPointerDown, int button)
    {
        _parameterlessOnPointerDown = onPointerDown;
        _button = button;
        _callback = OnPointerDown;
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

    protected override bool Equals(OnPointerDownModifierImpl other)
    {
        return _onPointerDown == other._onPointerDown &&
               _parameterlessOnPointerDown == other._parameterlessOnPointerDown &&
               _button == other._button;
    }

    private void OnPointerDown(PointerDownEvent it)
    {
        if (_button >= 0 && _button != it.button)
            return;
        _onPointerDown?.Invoke(
            new PointerClickInfo(
                Button: it.button,
                Position: it.position.ToOffset(),
                LocalPosition: it.localPosition.ToOffset()
            )
        );
        _parameterlessOnPointerDown?.Invoke();
        it.StopPropagation();
    }
}