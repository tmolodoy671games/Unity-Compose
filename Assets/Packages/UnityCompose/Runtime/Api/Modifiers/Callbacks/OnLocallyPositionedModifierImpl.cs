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
    private readonly Action<LayoutCoordinates> _callback;
    private Action<GeometryChangedEvent>? _onGeometryChanged;

    public OnLocallyPositionedModifierImpl(Action<LayoutCoordinates> onLocallyPositioned)
    {
        _callback = onLocallyPositioned;
    }

    public override void Apply(VisualElement element)
    {
        EnsureOnGeometryChanged();
        element.GetComposeCallback<GeometryChangedEvent>().Add(_onGeometryChanged!);
    }

    public override void Revert(VisualElement element)
    {
        EnsureOnGeometryChanged();
        element.GetComposeCallback<GeometryChangedEvent>().Remove(_onGeometryChanged!);
    }

    protected override bool Equals(OnLocallyPositionedModifierImpl other)
    {
        return _callback == other._callback;
    }

    private void EnsureOnGeometryChanged()
    {
        _onGeometryChanged ??= it => _callback(LayoutCoordinates.Create(it.VisualElement()));
    }
}