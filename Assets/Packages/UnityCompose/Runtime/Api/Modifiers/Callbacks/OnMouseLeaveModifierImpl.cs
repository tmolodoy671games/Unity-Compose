// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnMouseLeave(
        this IModifier modifier,
        Action<MouseMoveInfo> onMouseLeave,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseLeaveModifierImpl(onMouseLeave);
    }

    public static IModifier OnMouseLeave(
        this IModifier modifier,
        Action onMouseLeave,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseLeaveModifierImpl(onMouseLeave);
    }
}

internal class OnMouseLeaveModifierImpl : BaseModifier<OnMouseLeaveModifierImpl>
{
    private readonly Action<MouseMoveInfo>? _callback;
    private readonly Action? _parameterlessCallback;
    private Action<MouseLeaveEvent>? _onMouseLeave;

    public OnMouseLeaveModifierImpl(Action<MouseMoveInfo> onMouseEnter)
    {
        _callback = onMouseEnter;
    }
    
    public OnMouseLeaveModifierImpl(Action onMouseEnter)
    {
        _parameterlessCallback = onMouseEnter;
    }

    public override void Apply(VisualElement element)
    {
        EnsureOnMouseDown();
        element.ComposePickingMode().Increment();
        element.GetComposeCallback<MouseLeaveEvent>().Add(_onMouseLeave!);
    }

    public override void Revert(VisualElement element)
    {
        EnsureOnMouseDown();
        element.ComposePickingMode().Decrement();
        element.GetComposeCallback<MouseLeaveEvent>().Remove(_onMouseLeave!);
    }

    protected override bool Equals(OnMouseLeaveModifierImpl other)
    {
        return _callback == other._callback && _parameterlessCallback == other._parameterlessCallback;
    }

    private void EnsureOnMouseDown()
    {
        _onMouseLeave ??= it =>
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