using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class OnMouseMoveModifierImpl : UnityModifier<OnMouseMoveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onMouseMove;
    private readonly Action? _parameterlessOnMouseMove;
    private readonly EventCallback<MouseMoveEvent> _callback;

    public OnMouseMoveModifierImpl(Action<PointerMoveInfo> onMouseMove)
    {
        _onMouseMove = onMouseMove;
        _callback = OnMouseMove;
    }

    public OnMouseMoveModifierImpl(Action onMouseMove)
    {
        _parameterlessOnMouseMove = onMouseMove;
        _callback = OnMouseMove;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Increment();
        element.RegisterCallback(_callback);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.PickingMode().Decrement();
        element.UnregisterCallback(_callback);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onMouseMove, _parameterlessOnMouseMove);
    }

    protected override bool Equals(OnMouseMoveModifierImpl other)
    {
        return _onMouseMove == other._onMouseMove &&
               _parameterlessOnMouseMove == other._parameterlessOnMouseMove;
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        _onMouseMove?.Invoke(
            new PointerMoveInfo(
                Position: evt.mousePosition.ToOffset(),
                LocalPosition: evt.localMousePosition.ToOffset()
            )
        );
        _parameterlessOnMouseMove?.Invoke();
        evt.StopPropagation();
    }
}