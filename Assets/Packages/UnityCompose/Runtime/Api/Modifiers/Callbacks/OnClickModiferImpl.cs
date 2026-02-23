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
        Action<MouseClickInfo> onClick,
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
        Action<MouseClickInfo> onClick,
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
        Action<MouseClickInfo> onClick,
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
        Action<MouseClickInfo> onClick,
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

public readonly record struct MouseClickInfo(
    int Button,
    Vector2 Position,
    Vector2 LocalPosition
);

internal class OnClickModiferImpl : BaseModifier<OnClickModiferImpl>
{
    private readonly Action? _parameterlessLambda;
    private readonly Action<MouseClickInfo>? _lambda;
    private readonly int _allowedButton;
    private Action<ClickEvent>? _onClick;

    public OnClickModiferImpl(Action<MouseClickInfo> onClick, int allowedButton = -1)
    {
        _lambda = onClick;
        _allowedButton = allowedButton;
    }

    public OnClickModiferImpl(Action onClick, int allowedButton = -1)
    {
        _parameterlessLambda = onClick;
        _allowedButton = allowedButton;
    }

    public override void Apply(VisualElement element)
    {
        EnsureOnClick();
        element.ComposePickingMode().Increment();
        element.GetComposeCallback<ClickEvent>().Add(Key, _onClick!);
    }

    public override void Revert(VisualElement element)
    {
        EnsureOnClick();
        element.ComposePickingMode().Decrement();
        element.GetComposeCallback<ClickEvent>().Remove(Key);
    }

    private void EnsureOnClick()
    {
        _onClick ??= it =>
        {
            if (_allowedButton >= 0 && it.button != _allowedButton)
                return;
            _lambda?.Invoke(
                new MouseClickInfo(
                    Button: it.button,
                    Position: it.position,
                    LocalPosition: it.localPosition
                )
            );
            _parameterlessLambda?.Invoke();
        };
    }

    private object? Key => _lambda as object ?? _parameterlessLambda;

    protected override bool Equals(OnClickModiferImpl other)
    {
        return Key == other.Key && _allowedButton == other._allowedButton;
    }
}