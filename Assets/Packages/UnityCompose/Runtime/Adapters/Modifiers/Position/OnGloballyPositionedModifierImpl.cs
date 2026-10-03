// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnGloballyPositionedModifierImpl : UnityModifier<OnGloballyPositionedModifierImpl>
{
    private readonly Action<LayoutCoordinates> _onGloballyPositioned;

    public OnGloballyPositionedModifierImpl(Action<LayoutCoordinates> onGloballyPositioned)
    {
        _onGloballyPositioned = onGloballyPositioned;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (element.UserData().ContainsKey(_onGloballyPositioned)) return;
        var previousCoordinates = Optional.Empty<LayoutCoordinates>();
        var callback = () =>
        {
            var newCoordinates = element.LayoutCoordinates();
            if (previousCoordinates.Equals(newCoordinates)) return;
            previousCoordinates = newCoordinates;
            _onGloballyPositioned(newCoordinates);
        };
        var onGloballyPositionedCallback = element.schedule.Execute(callback).Every(0);
        callback();
        element.UserData()[_onGloballyPositioned] = onGloballyPositionedCallback;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.UserData().GetOrDefault(_onGloballyPositioned, null)?.CastTo<IVisualElementScheduledItem>().Pause();
    }

    protected override bool Equals(OnGloballyPositionedModifierImpl other)
    {
        return _onGloballyPositioned == other._onGloballyPositioned;
    }

    public override int GetHashCode() => HashCode.Combine(_onGloballyPositioned);
}