// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    #region OnMouseUp

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseUp(
        this IModifier modifier,
        bool enabled,
        Action<MouseClickInfo> onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseDown);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseUp(
        this IModifier modifier,
        bool enabled,
        Action onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(_ => onMouseDown());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseUp(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseDown);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseUp(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(_ => onMouseDown());
    }

    #endregion

    #region OnLmbUp

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnLmbUp(
        this IModifier modifier,
        bool enabled,
        Action<MouseClickInfo> onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnLmbUp(
        this IModifier modifier,
        bool enabled,
        Action onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseDown();
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnLmbUp(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnLmbUp(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseDown();
        });
    }

    #endregion

    #region OnRmbUp

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnRmbUp(
        this IModifier modifier,
        bool enabled,
        Action<MouseClickInfo> onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnRmbUp(
        this IModifier modifier,
        bool enabled,
        Action onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseDown();
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnRmbUp(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnRmbUp(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseDown();
        });
    }

    #endregion

    #region OnMmbUp

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbUp(
        this IModifier modifier,
        bool enabled,
        Action<MouseClickInfo> onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbUp(
        this IModifier modifier,
        bool enabled,
        Action onMouseDown
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown();
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbUp(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbUp(
        this IModifier modifier,
        Action onMouseDown,
        bool enabled = true
    )
    {
        if (enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseDown();
        });
    }

    #endregion
}

internal class OnMouseUpModifierImpl : BaseModifier<OnMouseUpModifierImpl>
{
    private readonly Action<MouseUpEvent> _onMouseUp;

    public OnMouseUpModifierImpl(Action<MouseClickInfo> onMouseDown)
    {
        _onMouseUp = it => onMouseDown(
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
        element.GetComposeCallback<MouseUpEvent>().Add(_onMouseUp);
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.PickingMode);
    }

    public override void Revert(VisualElement element)
    {
        element.pickingMode = PickingMode.Ignore;
        element.GetComposeCallback<MouseUpEvent>().Remove(_onMouseUp);
    }

    protected override bool Equals(OnMouseUpModifierImpl other)
    {
        return _onMouseUp == other._onMouseUp;
    }
}