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
        Action<MouseClickInfo> onMouseDown,
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
        Action<MouseClickInfo> onMouseDown,
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
        Action<MouseClickInfo> onMouseDown,
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
        Action<MouseClickInfo> onMouseDown,
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
    private readonly Action? _parameterlessCallback;
    private readonly Action<MouseClickInfo>? _callback;
    private readonly int _button;
    private Action<MouseDownEvent>? _onMouseDown;

    public OnMouseDownModifierImpl(Action<MouseClickInfo> onMouseDown, int button)
    {
        _callback = onMouseDown;
        _button = button;
    }

    public OnMouseDownModifierImpl(Action onMouseDown, int button)
    {
        _parameterlessCallback = onMouseDown;
        _button = button;
    }

    public override void Apply(VisualElement element)
    {
        EnsureOnMouseDown();
        element.pickingMode = PickingMode.Position;
        element.GetComposeCallback<MouseDownEvent>().Add(_onMouseDown!);
    }

    public override void Revert(VisualElement element)
    {
        EnsureOnMouseDown();
        element.pickingMode = PickingMode.Ignore;
        element.GetComposeCallback<MouseDownEvent>().Remove(_onMouseDown!);
    }

    protected override bool Equals(OnMouseDownModifierImpl other)
    {
        return _parameterlessCallback == other._parameterlessCallback && _callback == other._callback;
    }

    private void EnsureOnMouseDown()
    {
        _onMouseDown = it =>
        {
            if (_button >= 0 && _button != it.button)
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