// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerLeaveModifierImpl : UnityModifier<OnPointerLeaveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onPointerLeave;
    private readonly Action? _parameterlessOnPointerLeave;
    private readonly EventCallback<PointerLeaveEvent> _callback;
    private readonly int _pointerId;

    public OnPointerLeaveModifierImpl(Action<PointerMoveInfo> onPointerLeave, int pointerId)
    {
        _onPointerLeave = onPointerLeave;
        _callback = OnPointerLeave;
        _pointerId = pointerId;
    }

    public OnPointerLeaveModifierImpl(Action onPointerLeave, int pointerId)
    {
        _parameterlessOnPointerLeave = onPointerLeave;
        _callback = OnPointerLeave;
        _pointerId = pointerId;
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

    protected override bool Equals(OnPointerLeaveModifierImpl other)
    {
        return _onPointerLeave == other._onPointerLeave &&
               _parameterlessOnPointerLeave == other._parameterlessOnPointerLeave;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onPointerLeave, _parameterlessOnPointerLeave);
    }

    private void OnPointerLeave(PointerLeaveEvent evt)
    {
        if (_pointerId >= 0 && _pointerId != evt.pointerId)
            return;
        _onPointerLeave?.Invoke(
            new PointerMoveInfo(
                Position: evt.position.ToVector2().ToOffset(),
                LocalPosition: evt.localPosition.ToVector2().ToOffset()
            )
        );
        _parameterlessOnPointerLeave?.Invoke();
        evt.StopPropagation();
    }
}