// ReSharper disable CheckNamespace

using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnScroll(
        this IModifier modifier,
        Action<Vector2> onScroll,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnScrollModifierImpl(callback: onScroll);
    }

    public static IModifier OnVerticalScroll(
        this IModifier modifier,
        Action<float> onVerticalScroll,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnScrollModifierImpl(verticalCallback: onVerticalScroll);
    }

    public static IModifier OnHorizontalScroll(
        this IModifier modifier,
        Action<float> onHorizontalScroll,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnScrollModifierImpl(horizontalCallback: onHorizontalScroll);
    }
}

internal class OnScrollModifierImpl : BaseModifier<OnScrollModifierImpl>
{
    private readonly Action<Vector2>? _onScroll;
    private readonly Action<float>? _onVerticalScroll;
    private readonly Action<float>? _onHorizontalScroll;
    private readonly EventCallback<WheelEvent>? _callback;

    public OnScrollModifierImpl(
        Action<Vector2>? callback = null,
        Action<float>? verticalCallback = null,
        Action<float>? horizontalCallback = null
    )
    {
        _onScroll = callback;
        _onVerticalScroll = verticalCallback;
        _onHorizontalScroll = horizontalCallback;
        _callback = OnScroll;
    }

    public override void Apply(VisualElement element)
    {
        element.ComposePickingMode().Increment();
        element.RegisterCallback(_callback);
    }

    public override void Revert(VisualElement element)
    {
        element.ComposePickingMode().Decrement();
        element.UnregisterCallback(_callback);
    }

    protected override bool Equals(OnScrollModifierImpl other)
    {
        return _callback == other._callback;
    }

    private void OnScroll(WheelEvent evt)
    {
        _onScroll?.Invoke(evt.delta);
        if (evt.delta.x != 0)
            _onHorizontalScroll?.Invoke(evt.delta.x);
        if (evt.delta.y != 0)
            _onVerticalScroll?.Invoke(evt.delta.y);
    }
}