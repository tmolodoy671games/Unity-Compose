// ReSharper disable CheckNamespace

namespace UnityCompose;

public readonly record struct RoundedCornerShape(
    Dp TopLeft = default,
    Dp TopRight = default,
    Dp BottomLeft = default,
    Dp BottomRight = default
);