// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerEnterModifierImpl : BaseUnityModifier<OnPointerEnterModifierImpl>
{
    private readonly Action? _parameterlessOnPointerEnter;
    private readonly Action<PointerMoveInfo>? _onPointerEnter;
    private readonly EventCallback<PointerEnterEvent> _callback;
    private readonly int _pointerId;

    public OnPointerEnterModifierImpl(Action<PointerMoveInfo> onPointerEnter, int pointerId)
    {
        _onPointerEnter = onPointerEnter;
        _callback = OnPointerEnterEvent;
        _pointerId = pointerId;
    }

    public OnPointerEnterModifierImpl(Action onPointerEnter, int pointerId)
    {
        _parameterlessOnPointerEnter = onPointerEnter;
        _callback = OnPointerEnterEvent;
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

    protected override bool Equals(OnPointerEnterModifierImpl other)
    {
        return _onPointerEnter == other._onPointerEnter &&
               _parameterlessOnPointerEnter == other._parameterlessOnPointerEnter;
    }

    private void OnPointerEnterEvent(PointerEnterEvent evt)
    {
        if (_pointerId >= 0 && _pointerId != evt.pointerId)
            return;
        _onPointerEnter?.Invoke(
            new PointerMoveInfo(
                Position: evt.position.ToOffset(),
                LocalPosition: evt.localPosition.ToOffset()
            )
        );
        _parameterlessOnPointerEnter?.Invoke();
        evt.StopPropagation();
    }
}