// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnPointerLeave(
        this IModifier modifier,
        Action<PointerMoveInfo> onPointerLeave,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerLeaveModifierImpl(onPointerLeave);
    }

    public static IModifier OnPointerLeave(
        this IModifier modifier,
        Action onPointerLeave,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerLeaveModifierImpl(onPointerLeave);
    }
}

internal class OnPointerLeaveModifierImpl : BaseModifier<OnPointerLeaveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onPointerLeave;
    private readonly Action? _parameterlessOnPointerLeave;
    private readonly EventCallback<PointerLeaveEvent> _callback;

    public OnPointerLeaveModifierImpl(Action<PointerMoveInfo> onPointerLeave)
    {
        _onPointerLeave = onPointerLeave;
        _callback = OnPointerLeave;
    }

    public OnPointerLeaveModifierImpl(Action onPointerLeave)
    {
        _parameterlessOnPointerLeave = onPointerLeave;
        _callback = OnPointerLeave;
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

    protected override bool Equals(OnPointerLeaveModifierImpl other)
    {
        return _onPointerLeave == other._onPointerLeave &&
               _parameterlessOnPointerLeave == other._parameterlessOnPointerLeave;
    }

    private void OnPointerLeave(PointerLeaveEvent evt)
    {
        _onPointerLeave?.Invoke(
            new PointerMoveInfo(
                Position: evt.position,
                LocalPosition: evt.localPosition
            )
        );
        _parameterlessOnPointerLeave?.Invoke();
    }
}