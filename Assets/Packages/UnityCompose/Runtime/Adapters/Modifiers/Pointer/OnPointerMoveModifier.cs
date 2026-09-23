// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerMoveModifierImpl : BaseUnityModifier<OnPointerMoveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onPointerMove;
    private readonly Action? _parameterlessOnPointerMove;
    private readonly EventCallback<PointerMoveEvent> _callback;
    private readonly int _pointerId;

    public OnPointerMoveModifierImpl(Action<PointerMoveInfo> onPointerMove, int pointerId)
    {
        _onPointerMove = onPointerMove;
        _callback = OnPointerMove;
        _pointerId = pointerId;
    }

    public OnPointerMoveModifierImpl(Action onPointerMove, int pointerId)
    {
        _parameterlessOnPointerMove = onPointerMove;
        _callback = OnPointerMove;
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

    protected override bool Equals(OnPointerMoveModifierImpl other)
    {
        return _onPointerMove == other._onPointerMove &&
               _parameterlessOnPointerMove == other._parameterlessOnPointerMove;
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (_pointerId >= 0 && _pointerId != evt.pointerId)
            return;
        _onPointerMove?.Invoke(
            new PointerMoveInfo(
                Position: evt.position.ToOffset(),
                LocalPosition: evt.localPosition.ToOffset()
            )
        );
        _parameterlessOnPointerMove?.Invoke();
        evt.StopPropagation();
    }
}