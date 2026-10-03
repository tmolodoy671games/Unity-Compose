// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerCancelModifierImpl : UnityModifier<OnPointerCancelModifierImpl>
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

    protected override bool Equals(OnPointerCancelModifierImpl other)
    {
        return _onPointerCancel == other._onPointerCancel &&
               _parameterlessOnPointerCancel == other._parameterlessOnPointerCancel;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onPointerCancel, _parameterlessOnPointerCancel, _button);
    }

    private void OnPointerCancel(PointerCancelEvent evt)
    {
        if (_button >= 0 && evt.button != _button)
            return;
        _onPointerCancel?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.position.ToVector2().ToOffset(),
                LocalPosition: evt.localPosition.ToVector2().ToOffset()
            )
        );
        _parameterlessOnPointerCancel?.Invoke();
        evt.StopPropagation();
    }
}