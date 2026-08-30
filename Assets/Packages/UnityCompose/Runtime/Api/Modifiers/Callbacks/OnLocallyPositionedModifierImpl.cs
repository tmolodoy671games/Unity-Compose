// ReSharper disable CheckNamespace

using System;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnLocallyPositioned(
        this IModifier modifier,
        Action<LayoutCoordinates> onLocallyPositioned
    )
    {
        return modifier + new OnLocallyPositionedModifierImpl(onLocallyPositioned);
    }
}

internal class OnLocallyPositionedModifierImpl : BaseModifier<OnLocallyPositionedModifierImpl>
{
    private readonly Action<LayoutCoordinates> _onLocallyPositioned;
    private readonly EventCallback<GeometryChangedEvent>? _callback;

    public OnLocallyPositionedModifierImpl(Action<LayoutCoordinates> onLocallyPositioned)
    {
        _onLocallyPositioned = onLocallyPositioned;
        _callback = OnGeometryChanged;
    }

    public override void Apply(VisualElement element)
    {
        element.RegisterCallback(_callback);
    }

    public override void Revert(VisualElement element)
    {
        element.UnregisterCallback(_callback);
    }

    protected override bool Equals(OnLocallyPositionedModifierImpl other)
    {
        return _onLocallyPositioned == other._onLocallyPositioned;
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        _onLocallyPositioned(LayoutCoordinates.Create(evt.VisualElement()));
    }
}