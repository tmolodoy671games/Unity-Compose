// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerMoveModifierImpl : UnityModifier<OnPointerMoveModifierImpl>
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

    protected override bool Equals(OnPointerMoveModifierImpl other)
    {
        return _onPointerMove == other._onPointerMove &&
               _parameterlessOnPointerMove == other._parameterlessOnPointerMove;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onPointerMove, _parameterlessOnPointerMove);
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (_pointerId >= 0 && _pointerId != evt.pointerId)
            return;
        _onPointerMove?.Invoke(
            new PointerMoveInfo(
                Position: evt.position.ToVector2().ToOffset(),
                LocalPosition: evt.localPosition.ToVector2().ToOffset()
            )
        );
        _parameterlessOnPointerMove?.Invoke();
        evt.StopPropagation();
    }
}