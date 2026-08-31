// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnPointerDown(
        this IModifier modifier,
        Action<PointerClickInfo> onPointerDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerDownModifierImpl(onPointerDown, -1);
    }

    public static IModifier OnPointerDown(
        this IModifier modifier,
        Action onPointerDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerDownModifierImpl(onPointerDown, -1);
    }
}

internal class OnPointerDownModifierImpl : BaseModifier<OnPointerDownModifierImpl>
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

    protected override bool Equals(OnPointerDownModifierImpl other)
    {
        return _onPointerDown == other._onPointerDown &&
               _parameterlessOnPointerDown == other._parameterlessOnPointerDown &&
               _button == other._button;
    }

    private void OnPointerDown(PointerDownEvent it)
    {
        if (_button >= 0 && _button != it.button)
            return;
        _onPointerDown?.Invoke(
            new PointerClickInfo(
                Button: it.button,
                Position: it.position,
                LocalPosition: it.localPosition
            )
        );
        _parameterlessOnPointerDown?.Invoke();
    }
}