// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace UnityCompose;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public readonly record struct RoundedCornerShape(
    LayoutLength TopLeft = default,
    LayoutLength TopRight = default,
    LayoutLength BottomLeft = default,
    LayoutLength BottomRight = default
)
{
    public RoundedCornerShape(
        LayoutLength All
    ) : this(All, All, All, All)
    {
    }
}