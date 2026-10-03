// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OnGloballyPositionedModifierImpl : UnityModifier<OnGloballyPositionedModifierImpl>
{
    private readonly Action<ILayoutCoordinates> _onGloballyPositioned;
    private readonly ReferenceKey _key;

    public OnGloballyPositionedModifierImpl(Action<ILayoutCoordinates> onGloballyPositioned)
    {
        _key = new ReferenceKey(this);
        _onGloballyPositioned = onGloballyPositioned;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (element.UserData().ContainsKey(_key)) return;
        var previousCoordinates = Optional.Empty<ILayoutCoordinates>();
        var callback = () =>
        {
            var newCoordinates = element.LayoutCoordinates();
            if (previousCoordinates.Equals(newCoordinates)) return;
            previousCoordinates = newCoordinates.ToOptional();
            _onGloballyPositioned(newCoordinates);
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
    }

    protected override bool Equals(OnGloballyPositionedModifierImpl other)
    {
        return _onGloballyPositioned == other._onGloballyPositioned;
    }

    public override int GetHashCode() => HashCode.Combine(_onGloballyPositioned);
}