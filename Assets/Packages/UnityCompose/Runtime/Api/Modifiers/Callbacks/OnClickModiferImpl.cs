// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnClick(
        this IModifier modifier,
        Action<PointerClickInfo> onClick,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnClickModiferImpl(onClick);
    }

    public static IModifier OnClick(
        this IModifier modifier,
        Action onClick,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnClickModiferImpl(onClick);
    }

    public static IModifier OnLmbClick(
        this IModifier modifier,
        Action<PointerClickInfo> onClick,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnClickModiferImpl(onClick, 0);
    }

    public static IModifier OnLmbClick(
        this IModifier modifier,
        Action onClick,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnClickModiferImpl(onClick, 0);
    }

    public static IModifier OnRmbClick(
        this IModifier modifier,
        Action<PointerClickInfo> onClick,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onClick, 1);
    }

    public static IModifier OnRmbClick(
        this IModifier modifier,
        Action onClick,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onClick, 1);
    }

    public static IModifier OnMmbClick(
        this IModifier modifier,
        Action<PointerClickInfo> onClick,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onClick, 2);
    }

    public static IModifier OnMmbClick(
        this IModifier modifier,
        Action onClick,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseUpModifierImpl(onClick, 2);
    }
}

public readonly record struct PointerClickInfo(
    int Button,
    Vector2 Position,
    Vector2 LocalPosition
);

internal class OnClickModiferImpl : BaseModifier<OnClickModiferImpl>
{
    private readonly Action<PointerClickInfo>? _onClick;
    private readonly Action? _parameterlessOnClick;
    private readonly EventCallback<ClickEvent> _callback;
    private readonly int _allowedButton;

    public OnClickModiferImpl(Action<PointerClickInfo> onClick, int allowedButton = -1)
    {
        _onClick = onClick;
        _allowedButton = allowedButton;
        _callback = OnClickCallback;
    }

    public OnClickModiferImpl(Action onClick, int allowedButton = -1)
    {
        _parameterlessOnClick = onClick;
        _allowedButton = allowedButton;
        _callback = OnClickCallback;
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

    private void OnClickCallback(ClickEvent it)
    {
        if (_allowedButton >= 0 && it.button != _allowedButton)
            return;
        _onClick?.Invoke(
            new PointerClickInfo(
                Button: it.button,
                Position: it.position,
                LocalPosition: it.localPosition
            )
        );
        _parameterlessOnClick?.Invoke();
    }

    protected override bool Equals(OnClickModiferImpl other)
    {
        return _onClick == other._onClick &&
               _parameterlessOnClick == other._parameterlessOnClick &&
               _allowedButton == other._allowedButton;
    }
}