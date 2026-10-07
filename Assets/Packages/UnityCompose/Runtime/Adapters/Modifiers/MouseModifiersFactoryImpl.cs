using System;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class MouseModifiersFactoryImpl : IMouseModifiersFactory
{
    public IModifier OnMouseEnter(Action onMouseEnter) => new OnMouseEnterModifierImpl(null, onMouseEnter);
    public IModifier OnMouseEnter(Action<PointerMoveInfo> onMouseEnter) =>
        new OnMouseEnterModifierImpl(onMouseEnter, null);
    public IModifier OnMouseMove(Action onMouseMove) => new OnMouseMoveModifierImpl(null, onMouseMove);
    public IModifier OnMouseMove(Action<PointerMoveInfo> onMouseMove) =>
        new OnMouseMoveModifierImpl(onMouseMove, null);
    public IModifier OnMouseLeave(Action onMouseLeave) => new OnMouseLeaveModifierImpl(null, onMouseLeave);
    public IModifier OnMouseLeave(Action<PointerMoveInfo> onMouseLeave) =>
        new OnMouseLeaveModifierImpl(onMouseLeave, null);

    public IModifier OnMouseDown(Action onMouseDown, int button) => 
        new OnMouseDownModifierImpl(null, onMouseDown, button);
    public IModifier OnMouseDown(Action<PointerClickInfo> onMouseDown, int button) =>
        new OnMouseDownModifierImpl(onMouseDown, null, button);

    public IModifier OnMouseUp(Action onMouseUp, int button) => new OnMouseUpModifierImpl(null, onMouseUp, button);
    public IModifier OnMouseUp(Action<PointerClickInfo> onMouseUp, int button) =>
        new OnMouseUpModifierImpl(onMouseUp, null, button);

    public IModifier OnClick(Action<PointerClickInfo> onClick, int button) =>
        new OnClickModiferImpl(onClick, null, button);
    public IModifier OnClick(Action onClick, int button) =>
        new OnClickModiferImpl(null, onClick, button);

    public IModifier CaptureMouse() => CaptureMouseModifierImpl.Instance;
}