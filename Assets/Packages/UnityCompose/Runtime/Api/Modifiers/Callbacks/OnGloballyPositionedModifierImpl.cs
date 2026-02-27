// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnGloballyPositioned(
        this IModifier modifier,
        Action<LayoutCoordinates> onGloballyPositioned,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnGloballyPositionedModifierImpl(onGloballyPositioned);
    }
}

internal class OnGloballyPositionedModifierImpl : BaseModifier<OnGloballyPositionedModifierImpl>
{
    private record Key(
        object? Value
    );

    private readonly Action<LayoutCoordinates> _onGloballyPositioned;
    private readonly Key _key;

    public OnGloballyPositionedModifierImpl(Action<LayoutCoordinates> onGloballyPositioned)
    {
        _onGloballyPositioned = onGloballyPositioned;
        _key = new Key(onGloballyPositioned);
    }

    public override void Apply(VisualElement element)
    {
        if (element.UserData().ContainsKey(_key)) return;
        var previousCoordinates = Optional.Empty<LayoutCoordinates>();
        var callback = () =>
        {
            var newCoordinates = LayoutCoordinates.Create(element);
            if (previousCoordinates.Equals(newCoordinates)) return;
            previousCoordinates = newCoordinates;
            _onGloballyPositioned(newCoordinates);
        };
        var onGloballyPositionedCallback = element.schedule.Execute(callback).Every(0);
        callback();
        element.UserData()[_key] = onGloballyPositionedCallback;
    }

    public override void Revert(VisualElement element)
    {
        element.UserData().GetOrDefault(_key, null)?.CastTo<IVisualElementScheduledItem>().Pause();
    }

    protected override bool Equals(OnGloballyPositionedModifierImpl other)
    {
        return _onGloballyPositioned == other._onGloballyPositioned;
    }
}