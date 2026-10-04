// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnPlacedModifierImpl : UnityModifier<OnPlacedModifierImpl>
{
    private readonly Action<ILayoutCoordinates> _onPlaced;
    private readonly EventCallback<GeometryChangedEvent> _onGeometryChanged;

    public OnPlacedModifierImpl(Action<ILayoutCoordinates> onPlaced)
    {
        _onPlaced = onPlaced;
        _onGeometryChanged = OnGeometryChanged();
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.RegisterCallback(_onGeometryChanged);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.UnregisterCallback(_onGeometryChanged);
    }

    protected override bool Equals(OnPlacedModifierImpl other) => _onPlaced == other._onPlaced;
    public override int GetHashCode() => _onPlaced.GetHashCode();

    private EventCallback<GeometryChangedEvent> OnGeometryChanged()
    {
        var previousBounds = Optional.Empty<RelativeLayoutBounds>();
        return it =>
        {
            var coordinates = it.VisualElement().LayoutCoordinates();
            var newBounds = coordinates.ToRelativeLayoutBounds();
            if (previousBounds.Equals(newBounds))
                return;
            previousBounds = newBounds;
            _onPlaced(coordinates);
        };
    }
}