using System;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class MouseModifiersFactoryImpl : IMouseModifiersFactory
{
    public IModifier OnMouseEnter(Action onMouseEnter) => new OnMouseEnterModifierImpl(onMouseEnter);
    public IModifier OnMouseEnter(Action<PointerMoveInfo> onMouseEnter) => new OnMouseEnterModifierImpl(onMouseEnter);
    public IModifier OnMouseMove(Action onMouseMove) => new OnMouseMoveModifierImpl(onMouseMove);
    public IModifier OnMouseMove(Action<PointerMoveInfo> onMouseMove) => new OnMouseMoveModifierImpl(onMouseMove);
    public IModifier OnMouseLeave(Action onMouseLeave) => new OnMouseLeaveModifierImpl(onMouseLeave);
    public IModifier OnMouseLeave(Action<PointerMoveInfo> onMouseLeave) => new OnMouseLeaveModifierImpl(onMouseLeave);
    public IModifier OnMouseDown(Action onMouseDown, int button) => new OnMouseDownModifierImpl(onMouseDown, button);

    public IModifier OnMouseDown(Action<PointerClickInfo> onMouseDown, int button) =>
        new OnMouseDownModifierImpl(onMouseDown, button);

    public IModifier OnMouseUp(Action onMouseUp, int button) => new OnMouseUpModifierImpl(onMouseUp, button);

    public IModifier OnMouseUp(Action<PointerClickInfo> onMouseUp, int button) =>
        new OnMouseUpModifierImpl(onMouseUp, button);
}