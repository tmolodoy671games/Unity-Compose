// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerLeaveModifierImpl : BaseUnityModifier<OnPointerLeaveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onPointerLeave;
    private readonly Action? _parameterlessOnPointerLeave;
    private readonly EventCallback<PointerLeaveEvent> _callback;
    private readonly int  _pointerId;

    public OnPointerLeaveModifierImpl(Action<PointerMoveInfo> onPointerLeave, int pointerId)
    {
        _onPointerLeave = onPointerLeave;
        _callback = OnPointerLeave;
        _pointerId = pointerId;
    }

    public OnPointerLeaveModifierImpl(Action onPointerLeave,int pointerId)
    {
        _parameterlessOnPointerLeave = onPointerLeave;
        _callback = OnPointerLeave;
        _pointerId = pointerId;
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

    protected override bool Equals(OnPointerLeaveModifierImpl other)
    {
        return _onPointerLeave == other._onPointerLeave &&
               _parameterlessOnPointerLeave == other._parameterlessOnPointerLeave;
    }

    private void OnPointerLeave(PointerLeaveEvent evt)
    {
        if (_pointerId >= 0 && _pointerId != evt.pointerId)
            return;
        _onPointerLeave?.Invoke(
            new PointerMoveInfo(
                Position: evt.position.ToOffset(),
                LocalPosition: evt.localPosition.ToOffset()
            )
        );
        _parameterlessOnPointerLeave?.Invoke();
        evt.StopPropagation();
    }
}