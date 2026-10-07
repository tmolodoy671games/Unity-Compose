// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record PressableModifierImpl(
    IMutableInteractionSource InteractionSource
) : UnityModifier
{
    private readonly EventCallback<PointerDownEvent> _pointerDownCallback = evt =>
    {
        if (evt.button != 0)
            return;
        var pressInteraction = new IPressInteraction.Press(evt.localPosition.ToVector2().ToOffset());
        evt.VisualElement().PressInteractions().Add(pressInteraction);
        InteractionSource.Emit(pressInteraction);
    };

    private readonly EventCallback<PointerUpEvent> _pointerUpCallback = evt =>
    {
        if (evt.button != 0)
            return;
        var pressInteractions = evt.VisualElement().PressInteractions();
        if (pressInteractions.IsEmpty())
            return;
        var pressInteraction = pressInteractions[0];
        pressInteractions.RemoveAt(0);
        InteractionSource.Emit(new IPressInteraction.Release(pressInteraction));
    };

    private readonly EventCallback<PointerCancelEvent> _pointerCancelCallback = evt =>
    {
        var pressInteractions = evt.VisualElement().PressInteractions();
        if (pressInteractions.IsEmpty())
            return;
        var pressInteraction = pressInteractions[0];
        pressInteractions.RemoveAt(0);
        InteractionSource.Emit(new IPressInteraction.Cancel(pressInteraction));
    };

    private readonly EventCallback<PointerLeaveEvent> _pointerLeaveCallback = evt =>
    {
        var pressInteractions = evt.VisualElement().PressInteractions();
        if (pressInteractions.IsEmpty())
            return;
        var pressInteraction = pressInteractions[0];
        pressInteractions.RemoveAt(0);
        InteractionSource.Emit(new IPressInteraction.Cancel(pressInteraction));
    };

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Increment();
        element.RegisterCallback(_pointerDownCallback);
        element.RegisterCallback(_pointerUpCallback);
        element.RegisterCallback(_pointerCancelCallback);
        element.RegisterCallback(_pointerLeaveCallback);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Decrement();
        element.UnregisterCallback(_pointerDownCallback);
        element.UnregisterCallback(_pointerUpCallback);
        element.UnregisterCallback(_pointerCancelCallback);
        element.UnregisterCallback(_pointerLeaveCallback);
    }
}

public static partial class VisualElementExtensions
{
    private const string PressInteractionsKey = "UnityCompose_PressInteractions";

    internal static IMutableStableList<IPressInteraction.Press> PressInteractions(this VisualElement element)
    {
        return (IMutableStableList<IPressInteraction.Press>)element.UserData().GetOrPut(
            PressInteractionsKey,
            static () => MutableStableListOf<IPressInteraction.Press>()
        ).NotNull();
    }
}