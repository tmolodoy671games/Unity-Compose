// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    #region OnMouseDown

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseDown(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(onMouseDown);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(_ => onMouseDown());
    }

    #endregion

    #region OnLmbDown

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnLmbDown(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnLmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseDown();
        });
    }

    #endregion
    
    #region OnRmbDown

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnRmbDown(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnRmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseDown();
        });
    }

    #endregion
    
    #region OnMmbDown

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbDown(
        this IModifier modifier,
        bool enabled,
        Action<MouseClickInfo> onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbDown(
        this IModifier modifier,
        bool enabled,
        Action onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown();
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbDown(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseDownModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbDown(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
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

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.PickingMode);
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