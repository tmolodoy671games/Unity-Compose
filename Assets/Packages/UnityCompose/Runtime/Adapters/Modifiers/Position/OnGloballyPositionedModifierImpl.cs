// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;
using TimeUtils = UnityCompose.Packages.UnityCompose.Runtime.Adapters.Utils.TimeUtils;

namespace UnityCompose;

internal record OnGloballyPositionedModifierImpl(
    Action<ILayoutCoordinates> OnGloballyPositioned
) : UnityModifier
{
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
            OnGloballyPositioned(newCoordinates);
        };
        var onGloballyPositionedCallback = element.schedule.Execute(callback).Every(TimeUtils.Frametime);
        callback();
        element.UserData()[new ReferenceKey(this)] = onGloballyPositionedCallback;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var key = new ReferenceKey(this);
        element.UserData().GetOrDefault(key, null)?.CastTo<IVisualElementScheduledItem>().Pause();
        element.UserData().Remove(key);
    }
}