// ReSharper disable CheckNamespace

namespace UnityCompose;

public readonly record struct RoundedCornerShape(
    LayoutLength TopLeft = default,
    LayoutLength TopRight = default,
    LayoutLength BottomLeft = default,
    LayoutLength BottomRight = default
);