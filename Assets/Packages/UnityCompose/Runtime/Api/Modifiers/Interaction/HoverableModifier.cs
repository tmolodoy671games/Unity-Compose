// ReSharper disable CheckNamespace

using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Hoverable(
        this IModifier modifier,
        IMutableInteractionSource interactionSource,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new HoverableModiferImpl(interactionSource);
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

internal class HoverableModiferImpl : BaseModifier<HoverableModiferImpl>
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

    public override void Apply(VisualElement element)
    {
        element.RegisterCallback(_pointerEnterCallback);
        element.RegisterCallback(_pointerLeaveCallback);
    }

    public override void Revert(VisualElement element)
    {
        element.UnregisterCallback(_pointerEnterCallback);
        element.UnregisterCallback(_pointerLeaveCallback);
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