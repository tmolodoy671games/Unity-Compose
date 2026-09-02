// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnMouseLeave(
        this IModifier modifier,
        Action<PointerMoveInfo> onMouseLeave,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseLeaveModifierImpl(onMouseLeave);
    }

    public static IModifier OnMouseLeave(
        this IModifier modifier,
        Action onMouseLeave,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseLeaveModifierImpl(onMouseLeave);
    }
}

internal class OnMouseLeaveModifierImpl : BaseModifier<OnMouseLeaveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onMouseLeave;
    private readonly Action? _parameterlessOnMouseLeave;
    private readonly EventCallback<MouseLeaveEvent> _callback;

    public OnMouseLeaveModifierImpl(Action<PointerMoveInfo> onMouseLeave)
    {
        _onMouseLeave = onMouseLeave;
        _callback = OnMouseLeave;
    }

    public OnMouseLeaveModifierImpl(Action onMouseLeave)
    {
        _parameterlessOnMouseLeave = onMouseLeave;
        _callback = OnMouseLeave;
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

    protected override bool Equals(OnMouseLeaveModifierImpl other)
    {
        return _onMouseLeave == other._onMouseLeave &&
               _parameterlessOnMouseLeave == other._parameterlessOnMouseLeave;
    }

    private void OnMouseLeave(MouseLeaveEvent evt)
    {
        _onMouseLeave?.Invoke(
            new PointerMoveInfo(
                Position: evt.mousePosition,
                LocalPosition: evt.localMousePosition
            )
        );
        _parameterlessOnMouseLeave?.Invoke();
        evt.StopPropagation();
    }
}