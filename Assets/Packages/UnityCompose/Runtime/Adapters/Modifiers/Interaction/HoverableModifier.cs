// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record HoverableModiferImpl(
    IMutableInteractionSource InteractionSource
) : UnityModifier
{
    private readonly EventCallback<PointerEnterEvent> _pointerEnterCallback = evt =>
    {
        var visualElement = evt.VisualElement();
        var enterInteraction = new IHoverInteraction.Enter();
        visualElement.EnterInteractions().Add(enterInteraction);
        InteractionSource.Emit(enterInteraction);
    };

    private readonly EventCallback<PointerLeaveEvent> _pointerLeaveCallback = evt =>
    {
        var visualElement = evt.VisualElement();
        var enterInteractions = visualElement.EnterInteractions();
        if (enterInteractions.IsEmpty())
            return;
        var enterInteraction = enterInteractions[enterInteractions.LastIndex];
        enterInteractions.RemoveAt(enterInteractions.LastIndex);
        InteractionSource.Emit(new IHoverInteraction.Exit(enterInteraction));
    };

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Increment();
        element.RegisterCallback(_pointerEnterCallback);
        element.RegisterCallback(_pointerLeaveCallback);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Decrement();
        element.UnregisterCallback(_pointerEnterCallback);
        element.UnregisterCallback(_pointerLeaveCallback);
    }
}

public static partial class VisualElementExtensions
{
    private const string EnterInteractionsKey = "UnityCompose_HoverEvents";

    internal static IMutableStableList<IHoverInteraction.Enter> EnterInteractions(this VisualElement element)
    {
        return (IMutableStableList<IHoverInteraction.Enter>)element.UserData().GetOrPut(
            EnterInteractionsKey,
            static () => MutableStableListOf<IHoverInteraction.Enter>()
        ).NotNull();
    }
}