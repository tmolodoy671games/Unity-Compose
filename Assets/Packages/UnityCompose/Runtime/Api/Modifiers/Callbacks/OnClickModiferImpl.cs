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
        bool enabled,
        Action<MouseClickInfo> onClick
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnClickModiferImpl(onClick);
    }

    public static IModifier OnClick(
        this IModifier modifier,
        bool enabled,
        Action onClick
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnClickModiferImpl(_ => onClick());
    }
    
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
        return modifier + new OnClickModiferImpl(_ => onClick());
    }
}

public readonly record struct MouseClickInfo(
    int Button,
    Vector2 Position,
    Vector2 LocalPosition
);

internal class OnClickModiferImpl : BaseModifier<OnClickModiferImpl>
{
    private readonly Action<ClickEvent> _onClick;

    public OnClickModiferImpl(Action<MouseClickInfo> onClick)
    {
        _onClick = it => onClick(
            new MouseClickInfo(
                Button: it.button,
                Position: it.position,
                LocalPosition: it.localPosition
            )
        );
    }

    public override void Apply(VisualElement element)
    {
        element.pickingMode = PickingMode.Position;
        element.GetComposeCallback<ClickEvent>().Add(_onClick);
    }

    public override void Revert(VisualElement element)
    {
        element.pickingMode = PickingMode.Ignore;
        element.GetComposeCallback<ClickEvent>().Remove(_onClick);
    }

    protected override bool Equals(OnClickModiferImpl other)
    {
        return _onClick == other._onClick;
    }
}