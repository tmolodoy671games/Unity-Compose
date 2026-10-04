// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnLayoutRectChangedModifierImpl : UnityModifier<OnLayoutRectChangedModifierImpl>
{
    private readonly Action<RelativeLayoutBounds> _onLayoutRectChanged;
    private readonly ReferenceKey _key;

    public OnLayoutRectChangedModifierImpl(Action<RelativeLayoutBounds> onLayoutRectChanged)
    {
        _key = new ReferenceKey(this);
        _onLayoutRectChanged = onLayoutRectChanged;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var previousRect = Optional.Empty<RelativeLayoutBounds>();
        var callback = () =>
        {
            var newCoordinates = element.LayoutCoordinates();
            var newRect = newCoordinates.ToRelativeLayoutBounds();
            if (previousRect.Equals(newRect)) return;
            previousRect = newRect.ToOptional();
            _onLayoutRectChanged(newRect);
        };
        var onGloballyPositionedCallback = element.schedule.Execute(callback).Every(0);
        callback();
        element.UserData()[_key] = onGloballyPositionedCallback;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.UserData().GetOrDefault(_key, null)?.CastTo<IVisualElementScheduledItem>().Pause();
        element.UserData().Remove(_key);
    }

    protected override bool Equals(OnLayoutRectChangedModifierImpl other)
    {
        return _onLayoutRectChanged == other._onLayoutRectChanged;
    }

    public override int GetHashCode() => HashCode.Combine(_onLayoutRectChanged);
}