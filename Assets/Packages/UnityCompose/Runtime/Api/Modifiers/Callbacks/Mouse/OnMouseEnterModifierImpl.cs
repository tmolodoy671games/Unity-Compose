// ReSharper disable CheckNamespace

using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnMouseEnter(
        this IModifier modifier,
        Action<PointerMoveInfo> onMouseEnter,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseEnterModifierImpl(onMouseEnter);
    }

    public static IModifier OnMouseEnter(
        this IModifier modifier,
        Action onMouseEnter,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseEnterModifierImpl(onMouseEnter);
    }
}

public readonly record struct PointerMoveInfo(
    Vector2 Position,
    Vector2 LocalPosition
);

internal class OnMouseEnterModifierImpl : BaseModifier<OnMouseEnterModifierImpl>
{
    private readonly Action? _parameterlessOnMouseEnter;
    private readonly Action<PointerMoveInfo>? _onMouseEnter;
    private readonly EventCallback<MouseEnterEvent> _callback;

    public OnMouseEnterModifierImpl(Action<PointerMoveInfo> onMouseEnter)
    {
        _onMouseEnter = onMouseEnter;
        _callback = OnMouseEnterEvent;
    }

    public OnMouseEnterModifierImpl(Action onMouseEnter)
    {
        _parameterlessOnMouseEnter = onMouseEnter;
        _callback = OnMouseEnterEvent;
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

    protected override bool Equals(OnMouseEnterModifierImpl other)
    {
        return _onMouseEnter == other._onMouseEnter &&
               _parameterlessOnMouseEnter == other._parameterlessOnMouseEnter;
    }

    private void OnMouseEnterEvent(MouseEnterEvent evt)
    {
        _onMouseEnter?.Invoke(
            new PointerMoveInfo(
                Position: evt.mousePosition,
                LocalPosition: evt.localMousePosition
            )
        );
        _parameterlessOnMouseEnter?.Invoke();
        evt.StopPropagation();
    }
}