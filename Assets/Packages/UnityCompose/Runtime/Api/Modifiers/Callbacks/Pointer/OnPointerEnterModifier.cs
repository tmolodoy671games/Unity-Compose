// ReSharper disable CheckNamespace

using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnPointerEnter(
        this IModifier modifier,
        Action<PointerMoveInfo> onPointerEnter,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerEnterModifierImpl(onPointerEnter);
    }

    public static IModifier OnPointerEnter(
        this IModifier modifier,
        Action onPointerEnter,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerEnterModifierImpl(onPointerEnter);
    }
}

internal class OnPointerEnterModifierImpl : BaseModifier<OnPointerEnterModifierImpl>
{
    private readonly Action? _parameterlessOnPointerEnter;
    private readonly Action<PointerMoveInfo>? _onPointerEnter;
    private readonly EventCallback<PointerEnterEvent> _callback;

    public OnPointerEnterModifierImpl(Action<PointerMoveInfo> onPointerEnter)
    {
        _onPointerEnter = onPointerEnter;
        _callback = OnPointerEnterEvent;
    }

    public OnPointerEnterModifierImpl(Action onPointerEnter)
    {
        _parameterlessOnPointerEnter = onPointerEnter;
        _callback = OnPointerEnterEvent;
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

    protected override bool Equals(OnPointerEnterModifierImpl other)
    {
        return _onPointerEnter == other._onPointerEnter &&
               _parameterlessOnPointerEnter == other._parameterlessOnPointerEnter;
    }

    private void OnPointerEnterEvent(PointerEnterEvent evt)
    {
        _onPointerEnter?.Invoke(
            new PointerMoveInfo(
                Position: evt.position,
                LocalPosition: evt.localPosition
            )
        );
        _parameterlessOnPointerEnter?.Invoke();
        evt.StopPropagation();
    }
}