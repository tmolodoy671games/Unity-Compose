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
        Action<MouseClickInfo> onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onMouseUp);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseUp(
        this IModifier modifier,
        Action onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(_ => onMouseUp());
    }

    #endregion

    #region OnLmbUp

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnLmbUp(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseUp(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnLmbUp(
        this IModifier modifier,
        Action onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 0)
                onMouseUp();
        });
    }

    #endregion

    #region OnRmbUp
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnRmbUp(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseUp(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnRmbUp(
        this IModifier modifier,
        Action onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 1)
                onMouseUp();
        });
    }

    #endregion

    #region OnMmbUp

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbUp(
        this IModifier modifier,
        Action<MouseClickInfo> onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseUp(it);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMmbUp(
        this IModifier modifier,
        Action onMouseUp,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(it =>
        {
            if (it.Button == 2)
                onMouseUp();
        });
    }

    #endregion
}

internal class OnMouseUpModifierImpl : BaseModifier<OnMouseUpModifierImpl>
{
    private readonly Action<MouseUpEvent> _onMouseUp;

    public OnMouseUpModifierImpl(Action<MouseClickInfo> onMouseUp)
    {
        _onMouseUp = it => onMouseUp(
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