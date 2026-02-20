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
        return modifier + new OnMouseDownModifierImpl(onMouseDown);
    }

    public static IModifier OnMouseDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(_ => onMouseDown());
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
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseDown(it);
        });
    }

    public static IModifier OnLmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseDown();
        });
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
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseDown(it);
        });
    }

    public static IModifier OnRmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseDown();
        });
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
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown(it);
        });
    }

    public static IModifier OnMmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown();
        });
    }

    #endregion
}

internal class OnMouseDownModifierImpl : BaseModifier<OnMouseDownModifierImpl>
{
    private readonly Action<MouseDownEvent> _onMouseDown;

    public OnMouseDownModifierImpl(Action<MouseClickInfo> onMouseDown)
    {
        _onMouseDown = it => onMouseDown(
            new MouseClickInfo(
                Button: it.button,
                Position: it.mousePosition,
                LocalPosition: it.localMousePosition
            )
        );
    }

    public override void Apply(VisualElement element)
    {
        element.pickingMode = PickingMode.Position;
        element.GetComposeCallback<MouseDownEvent>().Add(_onMouseDown);
    }

    public override void Revert(VisualElement element)
    {
        element.pickingMode = PickingMode.Ignore;
        element.GetComposeCallback<MouseDownEvent>().Remove(_onMouseDown);
    }

    protected override bool Equals(OnMouseDownModifierImpl other)
    {
        return _onMouseDown == other._onMouseDown;
    }
}