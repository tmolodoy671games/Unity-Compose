// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerEnterModifierImpl : UnityModifier<OnPointerEnterModifierImpl>
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

    protected override bool Equals(OnPointerEnterModifierImpl other)
    {
        return _onPointerEnter == other._onPointerEnter &&
               _parameterlessOnPointerEnter == other._parameterlessOnPointerEnter;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onPointerEnter, _parameterlessOnPointerEnter);
    }

    private void OnPointerEnterEvent(PointerEnterEvent evt)
    {
        if (_pointerId >= 0 && _pointerId != evt.pointerId)
            return;
        _onPointerEnter?.Invoke(
            new PointerMoveInfo(
                Position: evt.position.ToVector2().ToOffset(),
                LocalPosition: evt.localPosition.ToVector2().ToOffset()
            )
        );
        _parameterlessOnPointerEnter?.Invoke();
        evt.StopPropagation();
    }
}