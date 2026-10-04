// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnSizeChangedImpl : UnityModifier<OnSizeChangedImpl>
{
    private readonly Action<Size> _onSizeChanged;
    private readonly EventCallback<GeometryChangedEvent> _onGeometryChanged;

    public OnSizeChangedImpl(Action<Size> onSizeChanged)
    {
        _onSizeChanged = onSizeChanged;
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

    protected override bool Equals(OnSizeChangedImpl other) => _onSizeChanged == other._onSizeChanged;
    public override int GetHashCode() => _onSizeChanged.GetHashCode();

    private EventCallback<GeometryChangedEvent> OnGeometryChanged()
    {
        var previousSize = Optional.Empty<Size>();
        return it =>
        {
            var coordinates = it.VisualElement().LayoutCoordinates();
            var newSize = coordinates.Size;
            if (previousSize.Equals(newSize))
                return;
            previousSize = newSize;
            _onSizeChanged(newSize);
        };
    }
}