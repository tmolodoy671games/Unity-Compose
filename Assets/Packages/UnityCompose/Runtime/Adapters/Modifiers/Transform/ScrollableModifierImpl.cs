// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine;
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
        return modifier + new ScrollableModifierImpl(reverseDirection, interactionSource);
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
        var root = new Scrollable();
        root.Init(true, true);
        var element = node.VisualElement();
        element.RegisterCallback(_callback, TrickleDown.TrickleDown);
        element.PickingMode().Increment();
        element.style.overflow = Overflow.Hidden;
        element.UserData()[this] = true;
        element.RegisterCallback<ClickEvent>(it => Debug.Log("click"));
        node.CastTo<UnityReusableComposeNode>().SetContentContainer(root);
    }

    public override void Revert(IReusableComposeNode node)
    {
        var element = node.VisualElement();
        element.UnregisterCallback(_callback, TrickleDown.TrickleDown);
        element.PickingMode().Decrement();
        element.UserData().Remove(this);
        element.style.overflow = Overflow.Visible;
        node.CastTo<UnityReusableComposeNode>().RemoveContentContainer();
    }

    protected override bool Equals(ScrollableModifierImpl other)
    {
        return _reverseDirection == other._reverseDirection &&
               _interactionSource == other._interactionSource &&
               _callback == other._callback;
    }

    private void OnWheelEvent(WheelEvent evt)
    {
        var element = evt.VisualElement();
        if (!element.UserData().ContainsKey(this))
            return;
        var offset = new Offset(evt.delta.x, evt.delta.y);
        if (element.childCount <= 0)
            return;
        var container = element[0];
        var multiplier = _reverseDirection ? 1 : -1;
        var currentTranslate =
            new Vector2(container.style.translate.value.x.value, container.style.translate.value.y.value);
        currentTranslate *= multiplier;
        container.style.translate = currentTranslate + offset.ToVector2() * 40;
    }
}

internal class Scrollable : VisualElement
{
    private bool _scrollHorizontally;
    private bool _scrollVertically;

    public Scrollable()
    {
        RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        pickingMode = PickingMode.Ignore;
    }

    public void Init(bool scrollHorizontally, bool scrollVertically)
    {
        _scrollHorizontally = scrollHorizontally;
        _scrollVertically = scrollVertically;
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        style.position = Position.Absolute;
        if (!_scrollHorizontally)
            this[0].style.width = resolvedStyle.width;
        if (!_scrollVertically)
            this[0].style.height = resolvedStyle.height;
    }
}