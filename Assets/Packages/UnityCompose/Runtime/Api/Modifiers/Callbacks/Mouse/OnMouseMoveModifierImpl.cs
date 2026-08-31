// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnMouseMove(
        this IModifier modifier,
        Action<PointerMoveInfo> onMouseMove,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseMoveModifierImpl(onMouseMove);
    }

    public static IModifier OnMouseMove(
        this IModifier modifier,
        Action onMouseMove,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseMoveModifierImpl(onMouseMove);
    }
}

internal class OnMouseMoveModifierImpl : BaseModifier<OnMouseMoveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onMouseMove;
    private readonly Action? _parameterlessOnMouseMove;
    private readonly EventCallback<MouseMoveEvent> _callback;

    public OnMouseMoveModifierImpl(Action<PointerMoveInfo> onMouseMove)
    {
        _onMouseMove = onMouseMove;
        _callback = OnMouseMove;
    }

    public OnMouseMoveModifierImpl(Action onMouseMove)
    {
        _parameterlessOnMouseMove = onMouseMove;
        _callback = OnMouseMove;
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

    protected override bool Equals(OnMouseMoveModifierImpl other)
    {
        return _onMouseMove == other._onMouseMove &&
               _parameterlessOnMouseMove == other._parameterlessOnMouseMove;
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        _onMouseMove?.Invoke(
            new PointerMoveInfo(
                Position: evt.mousePosition,
                LocalPosition: evt.localMousePosition
            )
        );
        _parameterlessOnMouseMove?.Invoke();
    }
}