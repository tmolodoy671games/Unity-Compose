// ReSharper disable CheckNamespace

using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnScroll(this IModifier modifier, Action<Vector2> onScroll)
    {
        return modifier + new OnScrollModifierImpl(callback: onScroll);
    }

    public static IModifier OnVerticalScroll(this IModifier modifier, Action<float> onVerticalScroll)
    {
        return modifier + new OnScrollModifierImpl(verticalCallback: onVerticalScroll);
    }

    public static IModifier OnHorizontalScroll(this IModifier modifier, Action<float> onHorizontalScroll)
    {
        return modifier + new OnScrollModifierImpl(horizontalCallback: onHorizontalScroll);
    }
}

internal class OnScrollModifierImpl : BaseModifier<OnScrollModifierImpl>
{
    private readonly Action<Vector2>? _callback;
    private readonly Action<float>? _verticalCallback;
    private readonly Action<float>? _horizontalCallback;
    private Action<WheelEvent>? _onWheel;

    public OnScrollModifierImpl(
        Action<Vector2>? callback = null,
        Action<float>? verticalCallback = null,
        Action<float>? horizontalCallback = null
    )
    {
        _callback = callback;
        _verticalCallback = verticalCallback;
        _horizontalCallback = horizontalCallback;
    }

    private object? Key => _callback as object ?? _verticalCallback ?? _horizontalCallback;

    public override void Apply(VisualElement element)
    {
        _onWheel ??= CreateWheelEvent();
        element.GetComposeCallback<WheelEvent>().Add(Key, _onWheel);
    }

    public override void Revert(VisualElement element)
    {
        element.GetComposeCallback<WheelEvent>().Remove(Key);
    }

    protected override bool Equals(OnScrollModifierImpl other)
    {
        return _callback == other._callback;
    }

    private Action<WheelEvent> CreateWheelEvent()
    {
        return it =>
        {
            _callback?.Invoke(it.delta);
            if (it.delta.x != 0)
                _horizontalCallback?.Invoke(it.delta.x);
            if (it.delta.y != 0)
                _verticalCallback?.Invoke(it.delta.y);
        };
    }
}