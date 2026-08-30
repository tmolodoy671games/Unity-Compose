// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnPointerMove(
        this IModifier modifier,
        Action<PointerMoveInfo> onPointerMove,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerMoveModifierImpl(onPointerMove);
    }

    public static IModifier OnPointerMove(
        this IModifier modifier,
        Action onPointerMove,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnPointerMoveModifierImpl(onPointerMove);
    }
}

internal class OnPointerMoveModifierImpl : BaseModifier<OnPointerMoveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onPointerMove;
    private readonly Action? _parameterlessOnPointerMove;
    private readonly EventCallback<PointerMoveEvent> _callback;

    public OnPointerMoveModifierImpl(Action<PointerMoveInfo> onPointerMove)
    {
        _onPointerMove = onPointerMove;
        _callback = OnPointerMove;
    }

    public OnPointerMoveModifierImpl(Action onPointerMove)
    {
        _parameterlessOnPointerMove = onPointerMove;
        _callback = OnPointerMove;
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

    protected override bool Equals(OnPointerMoveModifierImpl other)
    {
        return _onPointerMove == other._onPointerMove &&
               _parameterlessOnPointerMove == other._parameterlessOnPointerMove;
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        _onPointerMove?.Invoke(
            new PointerMoveInfo(
                Position: evt.position,
                LocalPosition: evt.localPosition
            )
        );
        _parameterlessOnPointerMove?.Invoke();
    }
}