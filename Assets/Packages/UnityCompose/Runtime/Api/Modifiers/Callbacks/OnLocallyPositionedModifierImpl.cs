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
    private readonly Action<GeometryChangedEvent> _onGeometryChanged;

    public OnLocallyPositionedModifierImpl(Action<LayoutCoordinates> onLocallyPositioned)
    {
        _onGeometryChanged = it => onLocallyPositioned(LayoutCoordinates.Create(it.VisualElement()));
    }

    public override void Apply(VisualElement element)
    {
        element.GetComposeCallback<GeometryChangedEvent>().Add(_onGeometryChanged);
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
    }

    public override void Revert(VisualElement element)
    {
        element.GetComposeCallback<GeometryChangedEvent>().Remove(_onGeometryChanged);
    }

    protected override bool Equals(OnLocallyPositionedModifierImpl other)
    {
        return _onGeometryChanged == other._onGeometryChanged;
    }
}