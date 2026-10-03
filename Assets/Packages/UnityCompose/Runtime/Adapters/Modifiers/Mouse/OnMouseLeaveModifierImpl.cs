using System;
using Compose.Net;
using NUnit.Framework;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class OnMouseLeaveModifierImpl : UnityModifier<OnMouseLeaveModifierImpl>
{
    private readonly Action<PointerMoveInfo>? _onMouseLeave;
    private readonly Action? _parameterlessOnMouseLeave;
    private readonly EventCallback<MouseLeaveEvent> _callback;

    public OnMouseLeaveModifierImpl(Action<PointerMoveInfo> onMouseLeave)
    {
        _onMouseLeave = onMouseLeave;
        _callback = OnMouseLeave;
    }

    public OnMouseLeaveModifierImpl(Action onMouseLeave)
    {
        _parameterlessOnMouseLeave = onMouseLeave;
        _callback = OnMouseLeave;
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

    protected override bool Equals(OnMouseLeaveModifierImpl other)
    {
        return _onMouseLeave == other._onMouseLeave &&
               _parameterlessOnMouseLeave == other._parameterlessOnMouseLeave;
    }

    public override int GetHashCode() => HashCode.Combine(_onMouseLeave, _parameterlessOnMouseLeave);

    private void OnMouseLeave(MouseLeaveEvent evt)
    {
        _onMouseLeave?.Invoke(
            new PointerMoveInfo(
                Position: evt.mousePosition.ToOffset(),
                LocalPosition: evt.localMousePosition.ToOffset()
            )
        );
        _parameterlessOnMouseLeave?.Invoke();
        evt.StopPropagation();
    }
}