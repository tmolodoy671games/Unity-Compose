// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    #region OnMouseDown

    public static IModifier OnMouseDown(
        this IModifier modifier,
        Action<PointerClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown, -1);
    }

    public static IModifier OnMouseDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown, -1);
    }

    #endregion

    #region OnLmbDown

    public static IModifier OnLmbDown(
        this IModifier modifier,
        Action<PointerClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown, 0);
    }

    public static IModifier OnLmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown, 0);
    }

    #endregion

    #region OnRmbDown

    public static IModifier OnRmbDown(
        this IModifier modifier,
        Action<PointerClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown, 1);
    }

    public static IModifier OnRmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown, 1);
    }

    #endregion

    #region OnMmbDown

    public static IModifier OnMmbDown(
        this IModifier modifier,
        Action<PointerClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown, 2);
    }

    public static IModifier OnMmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown, 2);
    }

    #endregion
}

internal class OnMouseDownModifierImpl : BaseModifier<OnMouseDownModifierImpl>
{
    private readonly Action? _parameterlessOnMouseDown;
    private readonly Action<PointerClickInfo>? _onMouseDown;
    private readonly EventCallback<MouseDownEvent> _callback;
    private readonly int _button;

    public OnMouseDownModifierImpl(Action<PointerClickInfo> onMouseDown, int button)
    {
        _onMouseDown = onMouseDown;
        _button = button;
        _callback = OnMouseDown;
    }

    public OnMouseDownModifierImpl(Action onMouseDown, int button)
    {
        _parameterlessOnMouseDown = onMouseDown;
        _button = button;
        _callback = OnMouseDown;
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

    protected override bool Equals(OnMouseDownModifierImpl other)
    {
        return _onMouseDown == other._onMouseDown &&
               _parameterlessOnMouseDown == other._parameterlessOnMouseDown &&
               _button == other._button;
    }

    private void OnMouseDown(MouseDownEvent it)
    {
        if (_button >= 0 && _button != it.button)
            return;
        _onMouseDown?.Invoke(
            new PointerClickInfo(
                Button: it.button,
                Position: it.mousePosition,
                LocalPosition: it.localMousePosition
            )
        );
        _parameterlessOnMouseDown?.Invoke();
    }
}