// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    #region OnMouseUp

    public static IModifier OnMouseUp(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseUp,
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
        Action<MouseClickInfo> onMouseUp,
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
        Action<MouseClickInfo> onMouseUp,
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
        Action<MouseClickInfo> onMouseUp,
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
    private readonly Action<MouseClickInfo>? _callback;
    private readonly Action? _parameterlessCallback;
    private readonly int _button;
    private Action<MouseUpEvent>? _onMouseUp;

    public OnMouseUpModifierImpl(Action<MouseClickInfo> onMouseUp, int button)
    {
        _callback = onMouseUp;
        _button = button;
    }
    
    public OnMouseUpModifierImpl(Action onMouseUp, int button)
    {
        _parameterlessCallback = onMouseUp;
        _button = button;
    }

    public override void Apply(VisualElement element)
    {
        EnsureOnMouseUp();
        element.pickingMode = PickingMode.Position;
        element.GetComposeCallback<MouseUpEvent>().Add(_onMouseUp!);
    }

    public override void Revert(VisualElement element)
    {
        EnsureOnMouseUp();
        element.pickingMode = PickingMode.Ignore;
        element.GetComposeCallback<MouseUpEvent>().Remove(_onMouseUp!);
    }

    protected override bool Equals(OnMouseUpModifierImpl other)
    {
        return _callback == other._callback && _parameterlessCallback == other._parameterlessCallback;
    }

    private void EnsureOnMouseUp()
    {
        _onMouseUp ??= it =>
        {
            if (_button >= 0 && it.button != _button)
                return;
            _callback?.Invoke(
                new MouseClickInfo(
                    Button: it.button,
                    Position: it.mousePosition,
                    LocalPosition: it.localMousePosition
                )
            );
            _parameterlessCallback?.Invoke();
        };
    }
}