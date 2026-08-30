// ReSharper disable CheckNamespace

using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Clickable(
        this IModifier modifier,
        IMutableInteractionSource interactionSource,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new ClickableModifierImpl(interactionSource);
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

internal class ClickableModifierImpl : BaseModifier<ClickableModifierImpl>
{
    private readonly IMutableInteractionSource _interactionSource;
    private readonly EventCallback<PointerDownEvent> _pointerDownCallback;
    private readonly EventCallback<PointerUpEvent> _pointerUpCallback;

    public ClickableModifierImpl(IMutableInteractionSource interactionSource)
    {
        _interactionSource = interactionSource;
        _pointerDownCallback = OnPointerDown;
        _pointerUpCallback = OnPointerUp;
    }

    public override void Apply(VisualElement element)
    {
        element.ComposePickingMode().Increment();
        element.RegisterCallback(_pointerDownCallback);
        element.RegisterCallback(_pointerUpCallback);
    }

    public override void Revert(VisualElement element)
    {
        element.ComposePickingMode().Decrement();
        element.UnregisterCallback(_pointerDownCallback);
        element.UnregisterCallback(_pointerUpCallback);
    }

    protected override bool Equals(ClickableModifierImpl other)
    {
        return _interactionSource == other._interactionSource;
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (evt.button != 0)
            return;
        var pressInteraction = new IPressInteraction.Press(evt.localPosition);
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
}