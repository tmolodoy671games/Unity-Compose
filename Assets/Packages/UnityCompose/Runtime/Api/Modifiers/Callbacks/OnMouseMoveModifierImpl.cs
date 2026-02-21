// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnMouseMove(
        this IModifier modifier,
        Action<MouseMoveInfo> onMouseMove,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseMoveModifierImpl(onMouseMove);
    }

    public static IModifier OnMouseMove(
        this IModifier modifier,
        Action onMouseMove,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseMoveModifierImpl(onMouseMove);
    }
}

internal class OnMouseMoveModifierImpl : BaseModifier<OnMouseMoveModifierImpl>
{
    private readonly Action<MouseMoveInfo>? _callback;
    private readonly Action? _parameterlessCallback;
    private Action<MouseMoveEvent>? _onMouseMove;

    public OnMouseMoveModifierImpl(Action<MouseMoveInfo> onMouseMove)
    {
        _callback = onMouseMove;
    }
    
    public OnMouseMoveModifierImpl(Action onMouseMove)
    {
        _parameterlessCallback = onMouseMove;
    }

    public override void Apply(VisualElement element)
    {
        EnsureOnMouseMove();
        element.ComposePickingMode().Increment();
        element.GetComposeCallback<MouseMoveEvent>().Add(_onMouseMove!);
    }

    public override void Revert(VisualElement element)
    {
        EnsureOnMouseMove();
        element.ComposePickingMode().Decrement();
        element.GetComposeCallback<MouseMoveEvent>().Add(_onMouseMove!);
    }

    protected override bool Equals(OnMouseMoveModifierImpl other)
    {
        return _callback == other._callback && _parameterlessCallback == other._parameterlessCallback;
    }

    private void EnsureOnMouseMove()
    {
        _onMouseMove = it =>
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