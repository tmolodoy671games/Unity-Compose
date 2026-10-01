// ReSharper disable CheckNamespace

using Compose.Net;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace UnityCompose;

public static class ModifierExtensions
{
    public static IModifier Scrollable(
        this IModifier modifier,
        bool enabled = true,
        bool reverseDirection = false,
        IMutableInteractionSource? interactionSource = null
    )
    {
        if (!enabled)
            return modifier;
        return modifier;
    }
}

internal class ScrollableModifierImpl : BaseModifier<ScrollableModifierImpl>
{
    private readonly bool _reverseDirection;
    private readonly IMutableInteractionSource? _interactionSource;
    private readonly EventCallback<WheelEvent> _callback;

    public ScrollableModifierImpl(bool reverseDirection, IMutableInteractionSource? interactionSource)
    {
        _reverseDirection = reverseDirection;
        _interactionSource = interactionSource;
        _callback = OnWheelEvent;
    }

    public override void Apply(IReusableComposeNode node)
    {
        node.VisualElement().RegisterCallback(_callback);
    }

    public override void Revert(IReusableComposeNode node)
    {
        node.VisualElement().UnregisterCallback(_callback);
    }

    protected override bool Equals(ScrollableModifierImpl other)
    {
        return _reverseDirection == other._reverseDirection &&
               _interactionSource == other._interactionSource &&
               _callback == other._callback;
    }

    private void OnWheelEvent(WheelEvent evt)
    {
    }
}