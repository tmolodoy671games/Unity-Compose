// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record OnClickModiferImpl(
    Action<PointerClickInfo>? OnClick,
    Action? ParameterlessOnClick,
    int AllowedButton
) : UnityModifier
{
    private readonly EventCallback<ClickEvent> _callback = it =>
    {
        if (AllowedButton >= 0 && it.button != AllowedButton)
            return;
        OnClick?.Invoke(
            new PointerClickInfo(
                Button: it.button,
                Position: it.position.ToVector2().ToOffset(),
                LocalPosition: it.localPosition.ToVector2().ToOffset()
            )
        );
        ParameterlessOnClick?.Invoke();
        it.StopPropagation();
    };

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Increment();
        element.RegisterCallback(_callback);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Decrement();
        element.UnregisterCallback(_callback);
    }
}