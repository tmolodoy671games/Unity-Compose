// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPointerDownModifierImpl : UnityModifier<OnPointerDownModifierImpl>
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

    protected override bool Equals(OnPointerDownModifierImpl other)
    {
        return _onPointerDown == other._onPointerDown &&
               _parameterlessOnPointerDown == other._parameterlessOnPointerDown &&
               _button == other._button;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onPointerDown, _parameterlessOnPointerDown, _button);
    }

    private void OnPointerDown(PointerDownEvent it)
    {
        if (_button >= 0 && _button != it.button)
            return;
        _onPointerDown?.Invoke(
            new PointerClickInfo(
                Button: it.button,
                Position: it.position.ToVector2().ToOffset(),
                LocalPosition: it.localPosition.ToVector2().ToOffset()
            )
        );
        _parameterlessOnPointerDown?.Invoke();
        it.StopPropagation();
    }
}