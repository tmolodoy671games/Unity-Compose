// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class HoverableModiferImpl : UnityModifier<HoverableModiferImpl>
{
    private readonly IMutableInteractionSource _interactionSource;
    private readonly EventCallback<PointerEnterEvent> _pointerEnterCallback;
    private readonly EventCallback<PointerLeaveEvent> _pointerLeaveCallback;

    public HoverableModiferImpl(IMutableInteractionSource interactionSource)
    {
        _interactionSource = interactionSource;
        _pointerEnterCallback = OnPointerEnter;
        _pointerLeaveCallback = OnPointerLeave;
    }

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

    public override int GetHashCode()
    {
        return HashCode.Combine(_interactionSource);
    }

    protected override bool Equals(HoverableModiferImpl other)
    {
        return _interactionSource == other._interactionSource;
    }

    private void OnPointerEnter(PointerEnterEvent evt)
    {
        var visualElement = evt.VisualElement();
        var enterInteraction = new IHoverInteraction.Enter();
        visualElement.EnterInteractions().Add(enterInteraction);
        _interactionSource.Emit(enterInteraction);
    }

    private void OnPointerLeave(PointerLeaveEvent evt)
    {
        var visualElement = evt.VisualElement();
        var enterInteractions = visualElement.EnterInteractions();
        if (enterInteractions.IsEmpty())
            return;
        var enterInteraction = enterInteractions[enterInteractions.LastIndex];
        enterInteractions.RemoveAt(enterInteractions.LastIndex);
        _interactionSource.Emit(new IHoverInteraction.Exit(enterInteraction));
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