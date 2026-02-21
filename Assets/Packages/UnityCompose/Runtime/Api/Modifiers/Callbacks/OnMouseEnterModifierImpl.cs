// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnMouseEnter(
        this IModifier modifier,
        Action<MouseMoveInfo> onMouseEnter,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseEnterModifierImpl(onMouseEnter);
    }

    public static IModifier OnMouseEnter(
        this IModifier modifier,
        Action onMouseEnter,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseEnterModifierImpl(onMouseEnter);
    }
}

public readonly record struct MouseMoveInfo(
    Vector2 Position,
    Vector2 LocalPosition
);

internal class OnMouseEnterModifierImpl : BaseModifier<OnMouseEnterModifierImpl>
{
    private readonly Action? _parameterlessCallback;
    private readonly Action<MouseMoveInfo>? _callback;
    private Action<MouseEnterEvent>? _onMouseEnter;

    public OnMouseEnterModifierImpl(Action<MouseMoveInfo> onMouseEnter)
    {
        _callback = onMouseEnter;
    }
    
    public OnMouseEnterModifierImpl(Action onMouseEnter)
    {
        _parameterlessCallback = onMouseEnter;
    }

    public override void Apply(VisualElement element)
    {
        EnsureOnMouseEnter();
        element.ComposePickingMode().Increment();
        element.GetComposeCallback<MouseEnterEvent>().Add(_onMouseEnter!);
    }

    public override void Revert(VisualElement element)
    {
        EnsureOnMouseEnter();
        element.ComposePickingMode().Decrement();
        element.GetComposeCallback<MouseEnterEvent>().Remove(_onMouseEnter!);
    }

    protected override bool Equals(OnMouseEnterModifierImpl other)
    {
        return _callback == other._callback && _parameterlessCallback == other._parameterlessCallback;
    }

    private void EnsureOnMouseEnter()
    {
        _onMouseEnter ??= it =>
        {
            _callback?.Invoke(
                new MouseMoveInfo(
                    Position: it.mousePosition,
                    LocalPosition: it.localMousePosition
                )
            );
            _parameterlessCallback?.Invoke();
        };
    }
}