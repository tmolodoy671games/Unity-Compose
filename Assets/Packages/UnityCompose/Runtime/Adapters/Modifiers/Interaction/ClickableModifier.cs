// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class ClickableModifierImpl : BaseUnityModifier<ClickableModifierImpl>
{
    private readonly IMutableInteractionSource _interactionSource;
    private readonly EventCallback<PointerDownEvent> _pointerDownCallback;
    private readonly EventCallback<PointerUpEvent> _pointerUpCallback;
    private readonly EventCallback<PointerCancelEvent> _pointerCancelCallback;
    private readonly EventCallback<PointerLeaveEvent> _pointerLeaveCallback;

    public ClickableModifierImpl(IMutableInteractionSource interactionSource)
    {
        _interactionSource = interactionSource;
        _pointerDownCallback = OnPointerDown;
        _pointerUpCallback = OnPointerUp;
        _pointerCancelCallback = OnPointerCancel;
        _pointerLeaveCallback = OnPointerLeave;
    }

    public override void Apply(VisualElement element)
    {
        element.PickingMode().Increment();
        element.RegisterCallback(_pointerDownCallback);
        element.RegisterCallback(_pointerUpCallback);
        element.RegisterCallback(_pointerCancelCallback);
        element.RegisterCallback(_pointerLeaveCallback);
    }

    public override void Revert(VisualElement element)
    {
        element.PickingMode().Decrement();
        element.UnregisterCallback(_pointerDownCallback);
        element.UnregisterCallback(_pointerUpCallback);
        element.UnregisterCallback(_pointerCancelCallback);
        element.UnregisterCallback(_pointerLeaveCallback);
    }

    protected override bool Equals(ClickableModifierImpl other)
    {
        return _interactionSource == other._interactionSource;
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (evt.button != 0)
            return;
        var pressInteraction = new IPressInteraction.Press(evt.localPosition.ToOffset());
        evt.VisualElement().PressInteractions().Add(pressInteraction);
        _interactionSource.Emit(pressInteraction);
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (evt.button != 0)
            return;
        var pressInteractions = evt.VisualElement().PressInteractions();
        if (pressInteractions.IsEmpty())
            return;
        var pressInteraction = pressInteractions[0];
        pressInteractions.RemoveAt(0);
        _interactionSource.Emit(new IPressInteraction.Release(pressInteraction));
    }

    private void OnPointerCancel(PointerCancelEvent evt)
    {
        var pressInteractions = evt.VisualElement().PressInteractions();
        if (pressInteractions.IsEmpty())
            return;
        var pressInteraction = pressInteractions[0];
        pressInteractions.RemoveAt(0);
        _interactionSource.Emit(new IPressInteraction.Cancel(pressInteraction));
    }

    private void OnPointerLeave(PointerLeaveEvent evt)
    {
        var pressInteractions = evt.VisualElement().PressInteractions();
        if (pressInteractions.IsEmpty())
            return;
        var pressInteraction = pressInteractions[0];
        pressInteractions.RemoveAt(0);
        _interactionSource.Emit(new IPressInteraction.Cancel(pressInteraction));
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