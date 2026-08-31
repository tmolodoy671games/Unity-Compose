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

    public static RoundedCornerShape Lerp(
        RoundedCornerShape a,
        RoundedCornerShape b,
        float t
    )
    {
        return new RoundedCornerShape(
            TopLeft: LayoutLength.Lerp(a.TopLeft, b.TopLeft, t),
            TopRight: LayoutLength.Lerp(a.TopRight, b.TopRight, t),
            BottomLeft: LayoutLength.Lerp(a.BottomLeft, b.BottomLeft, t),
            BottomRight: LayoutLength.Lerp(a.BottomRight, b.BottomRight, t)
        );
    }

    public static RoundedCornerShape LerpUnclamped(
        RoundedCornerShape a,
        RoundedCornerShape b,
        float t
    )
    {
        return new RoundedCornerShape(
            TopLeft: LayoutLength.LerpUnclamped(a.TopLeft, b.TopLeft, t),
            TopRight: LayoutLength.LerpUnclamped(a.TopRight, b.TopRight, t),
            BottomLeft: LayoutLength.LerpUnclamped(a.BottomLeft, b.BottomLeft, t),
            BottomRight: LayoutLength.LerpUnclamped(a.BottomRight, b.BottomRight, t)
        );
    }
}