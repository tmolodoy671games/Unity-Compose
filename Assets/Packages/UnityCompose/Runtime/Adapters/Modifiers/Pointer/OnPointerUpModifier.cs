// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerUpModifierImpl : UnityModifier<OnPointerUpModifierImpl>
{
    private readonly Action<PointerClickInfo>? _onPointerUp;
    private readonly Action? _parameterlessOnPointerUp;
    private readonly EventCallback<PointerUpEvent> _callback;
    private readonly int _button;

    public OnPointerUpModifierImpl(Action<PointerClickInfo> onPointerUp, int button)
    {
        _onPointerUp = onPointerUp;
        _button = button;
        _callback = OnPointerUp;
    }

    public OnPointerUpModifierImpl(Action onPointerUp, int button)
    {
        _parameterlessOnPointerUp = onPointerUp;
        _button = button;
        _callback = OnPointerUp;
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

    protected override bool Equals(OnPointerUpModifierImpl other)
    {
        return _onPointerUp == other._onPointerUp &&
               _parameterlessOnPointerUp == other._parameterlessOnPointerUp;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onPointerUp, _button, _parameterlessOnPointerUp);
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (_button >= 0 && evt.button != _button)
            return;
        _onPointerUp?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.position.ToVector2().ToOffset(),
                LocalPosition: evt.localPosition.ToVector2().ToOffset()
            )
        );
        _parameterlessOnPointerUp?.Invoke();
        evt.StopPropagation();
    }
}