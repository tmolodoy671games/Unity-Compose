// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerCancelModifierImpl : BaseUnityModifier<OnPointerCancelModifierImpl>
{
    private readonly Action<PointerClickInfo>? _onPointerCancel;
    private readonly Action? _parameterlessOnPointerCancel;
    private readonly EventCallback<PointerCancelEvent> _callback;
    private readonly int _button;

    public OnPointerCancelModifierImpl(Action<PointerClickInfo> onPointerCancel, int button)
    {
        _onPointerCancel = onPointerCancel;
        _button = button;
        _callback = OnPointerCancel;
    }

    public OnPointerCancelModifierImpl(Action onPointerCancel, int button)
    {
        _parameterlessOnPointerCancel = onPointerCancel;
        _button = button;
        _callback = OnPointerCancel;
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

    protected override bool Equals(OnPointerCancelModifierImpl other)
    {
        return _onPointerCancel == other._onPointerCancel &&
               _parameterlessOnPointerCancel == other._parameterlessOnPointerCancel;
    }

    private void OnPointerCancel(PointerCancelEvent evt)
    {
        if (_button >= 0 && evt.button != _button)
            return;
        _onPointerCancel?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.position.ToOffset(),
                LocalPosition: evt.localPosition.ToOffset()
            )
        );
        _parameterlessOnPointerCancel?.Invoke();
        evt.StopPropagation();
    }
}