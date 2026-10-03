// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnLocallyPositionedModifierImpl : UnityModifier<OnLocallyPositionedModifierImpl>
{
    private readonly Action<ILayoutCoordinates> _onLocallyPositioned;
    private readonly EventCallback<GeometryChangedEvent>? _callback;

    public OnLocallyPositionedModifierImpl(Action<ILayoutCoordinates> onLocallyPositioned)
    {
        _onLocallyPositioned = onLocallyPositioned;
        _callback = OnGeometryChanged;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.RegisterCallback(_callback);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.UnregisterCallback(_callback);
    }

    protected override bool Equals(OnLocallyPositionedModifierImpl other)
    {
        return _onLocallyPositioned == other._onLocallyPositioned;
    }

    public override int GetHashCode() => HashCode.Combine(_onLocallyPositioned);

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        _onLocallyPositioned(evt.VisualElement().LayoutCoordinates());
    }
}