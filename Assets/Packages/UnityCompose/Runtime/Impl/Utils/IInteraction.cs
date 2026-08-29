// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public interface IInteraction
{
}

public interface IPressInteraction : IInteraction
{
    public sealed record Press(Vector2 PressPosition) : IPressInteraction;
    public sealed record Release(Press Press) : IPressInteraction;
    public sealed record Cancel(Press Press) : IPressInteraction;
}

public interface IFocusInteraction : IInteraction
{
    public sealed record Focus : IFocusInteraction;
    public sealed record Unfocus(Focus Focus) : IFocusInteraction;
}

public interface IHoverInteraction : IInteraction
{
    public sealed record Enter : IFocusInteraction;
    public sealed record Exit(Enter Enter) : IFocusInteraction;
}
