// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnPointerUp(
        this IModifier modifier,
        Action<PointerClickInfo> onPointerUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerUpModifierImpl(onPointerUp, -1);
    }

    public static IModifier OnPointerUp(
        this IModifier modifier,
        Action onPointerUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerUpModifierImpl(onPointerUp, -1);
    }
}

internal class OnPointerUpModifierImpl : BaseModifier<OnPointerUpModifierImpl>
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


    public override void Apply(VisualElement element)
    {
        element.ComposePickingMode().Increment();
        element.RegisterCallback(_callback);
    }

    public override void Revert(VisualElement element)
    {
        element.ComposePickingMode().Decrement();
        element.UnregisterCallback(_callback);
    }

    protected override bool Equals(OnPointerUpModifierImpl other)
    {
        return _onPointerUp == other._onPointerUp &&
               _parameterlessOnPointerUp == other._parameterlessOnPointerUp;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (_button >= 0 && evt.button != _button)
            return;
        _onPointerUp?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.position,
                LocalPosition: evt.localPosition
            )
        );
        _parameterlessOnPointerUp?.Invoke();
    }
}