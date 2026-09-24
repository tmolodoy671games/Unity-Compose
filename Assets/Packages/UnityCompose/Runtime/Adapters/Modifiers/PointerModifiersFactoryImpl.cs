using System;
using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class PointerModifiersFactoryImpl : IPointerModifiersFactory
{
    public IModifier OnPointerEnter(Action onPointerEnter, int pointerId)
    {
        return new OnPointerEnterModifierImpl(onPointerEnter, pointerId);
    }

    public IModifier OnPointerEnter(Action<PointerMoveInfo> onPointerEnter, int pointerId)
    {
        return new OnPointerEnterModifierImpl(onPointerEnter, pointerId);
    }

    public IModifier OnPointerMove(Action onPointerMove, int pointerId)
    {
        return new OnPointerMoveModifierImpl(onPointerMove, pointerId);
    }

    public IModifier OnPointerMove(Action<PointerMoveInfo> onPointerMove, int pointerId)
    {
        return new OnPointerMoveModifierImpl(onPointerMove, pointerId);
    }

    public IModifier OnPointerLeave(Action onPointerLeave, int pointerId)
    {
        return new OnPointerLeaveModifierImpl(onPointerLeave, pointerId);
    }

    public IModifier OnPointerLeave(Action<PointerMoveInfo> onPointerLeave, int pointerId)
    {
        return new OnPointerLeaveModifierImpl(onPointerLeave, pointerId);
    }

    public IModifier OnPointerDown(Action onPointerDown, int pointerId)
    {
        return new OnPointerDownModifierImpl(onPointerDown, pointerId);
    }

    public IModifier OnPointerDown(Action<PointerClickInfo> onPointerDown, int pointerId)
    {
        return new OnPointerDownModifierImpl(onPointerDown, pointerId);
    }

    public IModifier OnPointerUp(Action onPointerUp, int pointerId)
    {
        return new OnPointerUpModifierImpl(onPointerUp, pointerId);
    }

    public IModifier OnPointerUp(Action<PointerClickInfo> onPointerUp, int pointerId)
    {
        return new OnPointerUpModifierImpl(onPointerUp, pointerId);
    }

    public IModifier OnPointerCancel(Action onPointerCancel, int pointerId)
    {
        return new OnPointerCancelModifierImpl(onPointerCancel, pointerId);
    }

    public IModifier OnPointerCancel(Action<PointerClickInfo> onPointerCancel, int pointerId)
    {
        return new OnPointerCancelModifierImpl(onPointerCancel, pointerId);
    }

    public IModifier CapturePointer(int pointerId) => new CapturePointerModifierImpl(pointerId);
}