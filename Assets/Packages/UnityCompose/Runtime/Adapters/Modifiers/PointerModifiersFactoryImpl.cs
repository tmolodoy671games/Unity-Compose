using System;
using System.Threading.Tasks;
using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class PointerModifiersFactoryImpl : IPointerModifiersFactory
{
    public IModifier OnPointerEnter(Action onPointerEnter, int pointerId)
    {
        return new OnPointerEnterModifierImpl(null, onPointerEnter, pointerId);
    }

    public IModifier OnPointerEnter(Action<PointerMoveInfo> onPointerEnter, int pointerId)
    {
        return new OnPointerEnterModifierImpl(onPointerEnter, null, pointerId);
    }

    public IModifier OnPointerMove(Action onPointerMove, int pointerId)
    {
        return new OnPointerMoveModifierImpl(null, onPointerMove, pointerId);
    }

    public IModifier OnPointerMove(Action<PointerMoveInfo> onPointerMove, int pointerId)
    {
        return new OnPointerMoveModifierImpl(onPointerMove, null, pointerId);
    }

    public IModifier OnPointerLeave(Action onPointerLeave, int pointerId)
    {
        return new OnPointerLeaveModifierImpl(null, onPointerLeave, pointerId);
    }

    public IModifier OnPointerLeave(Action<PointerMoveInfo> onPointerLeave, int pointerId)
    {
        return new OnPointerLeaveModifierImpl(onPointerLeave, null, pointerId);
    }

    public IModifier OnPointerDown(Action onPointerDown, int pointerId)
    {
        return new OnPointerDownModifierImpl(null, onPointerDown, pointerId);
    }

    public IModifier OnPointerDown(Action<PointerClickInfo> onPointerDown, int pointerId)
    {
        return new OnPointerDownModifierImpl(onPointerDown, null, pointerId);
    }

    public IModifier OnPointerUp(Action onPointerUp, int pointerId)
    {
        return new OnPointerUpModifierImpl(null, onPointerUp, pointerId);
    }

    public IModifier OnPointerUp(Action<PointerClickInfo> onPointerUp, int pointerId)
    {
        return new OnPointerUpModifierImpl(onPointerUp, null, pointerId);
    }

    public IModifier OnPointerCancel(Action onPointerCancel, int pointerId)
    {
        return new OnPointerCancelModifierImpl(null, onPointerCancel, pointerId);
    }

    public IModifier OnPointerCancel(Action<PointerClickInfo> onPointerCancel, int pointerId)
    {
        return new OnPointerCancelModifierImpl(onPointerCancel, null, pointerId);
    }

    public IModifier CapturePointer(int pointerId) => new CapturePointerModifierImpl(pointerId);

    public IModifier PointerInput<T>(T key, Func<IPointerInputScope, Task> body)
    {
        return new PointerInputModifierImpl<T>(key, body);
    }
}