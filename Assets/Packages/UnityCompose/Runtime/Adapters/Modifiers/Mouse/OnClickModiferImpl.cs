// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnClickModiferImpl : BaseUnityModifier<OnClickModiferImpl>
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
                Position: it.position.ToOffset(),
                LocalPosition: it.localPosition.ToOffset()
            )
        );
        _parameterlessOnClick?.Invoke();
        it.StopPropagation();
    }

    protected override bool Equals(OnClickModiferImpl other)
    {
        return _onClick == other._onClick &&
               _parameterlessOnClick == other._parameterlessOnClick &&
               _allowedButton == other._allowedButton;
    }
}