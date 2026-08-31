// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    #region OnMouseUp

    public static IModifier OnMouseUp(
        this IModifier modifier,
        Action<PointerClickInfo> onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp, -1);
    }

    public static IModifier OnMouseUp(
        this IModifier modifier,
        Action onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp, -1);
    }

    #endregion

    #region OnLmbUp

    public static IModifier OnLmbUp(
        this IModifier modifier,
        Action<PointerClickInfo> onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp, 0);
    }

    public static IModifier OnLmbUp(
        this IModifier modifier,
        Action onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp, 0);
    }

    #endregion

    #region OnRmbUp

    public static IModifier OnRmbUp(
        this IModifier modifier,
        Action<PointerClickInfo> onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp, 1);
    }

    public static IModifier OnRmbUp(
        this IModifier modifier,
        Action onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp, 1);
    }

    #endregion

    #region OnMmbUp

    public static IModifier OnMmbUp(
        this IModifier modifier,
        Action<PointerClickInfo> onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp, 2);
    }

    public static IModifier OnMmbUp(
        this IModifier modifier,
        Action onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp, 2);
    }

    #endregion
}

internal class OnMouseUpModifierImpl : BaseModifier<OnMouseUpModifierImpl>
{
    private readonly Action<PointerClickInfo>? _onMouseUp;
    private readonly Action? _parameterlessOnMouseUp;
    private readonly EventCallback<MouseUpEvent> _callback;
    private readonly int _button;

    public OnMouseUpModifierImpl(Action<PointerClickInfo> onMouseUp, int button)
    {
        _onMouseUp = onMouseUp;
        _button = button;
        _callback = OnMouseUp;
    }

    public OnMouseUpModifierImpl(Action onMouseUp, int button)
    {
        _parameterlessOnMouseUp = onMouseUp;
        _button = button;
        _callback = OnMouseUp;
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

    protected override bool Equals(OnMouseUpModifierImpl other)
    {
        return _onMouseUp == other._onMouseUp && 
               _parameterlessOnMouseUp == other._parameterlessOnMouseUp;
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        if (_button >= 0 && evt.button != _button)
            return;
        _onMouseUp?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.mousePosition,
                LocalPosition: evt.localMousePosition
            )
        );
        _parameterlessOnMouseUp?.Invoke();
    }
}