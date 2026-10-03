using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class OnMouseDownModifierImpl : UnityModifier<OnMouseDownModifierImpl>
{
    private readonly Action? _parameterlessOnMouseDown;
    private readonly Action<PointerClickInfo>? _onMouseDown;
    private readonly EventCallback<MouseDownEvent> _callback;
    private readonly int _button;

    public OnMouseDownModifierImpl(Action<PointerClickInfo> onMouseDown, int button)
    {
        _onMouseDown = onMouseDown;
        _button = button;
        _callback = OnMouseDown;
    }

    public OnMouseDownModifierImpl(Action onMouseDown, int button)
    {
        _parameterlessOnMouseDown = onMouseDown;
        _button = button;
        _callback = OnMouseDown;
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

    protected override bool Equals(OnMouseDownModifierImpl other)
    {
        return _onMouseDown == other._onMouseDown &&
               _parameterlessOnMouseDown == other._parameterlessOnMouseDown &&
               _button == other._button;
    }

    public override int GetHashCode() => HashCode.Combine(_onMouseDown, _parameterlessOnMouseDown, _button);

    private void OnMouseDown(MouseDownEvent evt)
    {
        if (_button >= 0 && _button != evt.button)
            return;
        _onMouseDown?.Invoke(
            new PointerClickInfo(
                Button: evt.button,
                Position: evt.mousePosition.ToOffset(),
                LocalPosition: evt.localMousePosition.ToOffset()
            )
        );
        _parameterlessOnMouseDown?.Invoke();
        evt.StopPropagation();
    }
}