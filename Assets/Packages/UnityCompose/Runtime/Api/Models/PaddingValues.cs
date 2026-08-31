using System.Diagnostics.CodeAnalysis;
using UnityEngine;
// ReSharper disable CheckNamespace

namespace UnityCompose;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public readonly record struct PaddingValues(
    Dp Top = default,
    Dp Bottom = default,
    Dp Left = default,
    Dp Right = default
)
{
    public PaddingValues(Dp All) : this(All, All, All, All)
    {
    }

    public PaddingValues(
        Dp Horizontal = default,
        Dp Vertical = default
    ) : this(Vertical, Vertical, Horizontal, Horizontal)
    {
    }

    public static PaddingValues LerpUnclamped(
        PaddingValues a,
        PaddingValues b,
        float t
    )
    {
        return new PaddingValues(
            Top: Dp.LerpUnclamped(a.Top, b.Top, t),
            Bottom: Dp.LerpUnclamped(a.Bottom, b.Bottom, t),
            Left: Dp.LerpUnclamped(a.Left, b.Left, t),
            Right: Dp.LerpUnclamped(a.Right, b.Right, t)
        );
    }
    
    public static PaddingValues Lerp(
        PaddingValues a,
        PaddingValues b,
        float t
    )
    {
        return new PaddingValues(
            Top: Dp.Lerp(a.Top, b.Top, t),
            Bottom: Dp.Lerp(a.Bottom, b.Bottom, t),
            Left: Dp.Lerp(a.Left, b.Left, t),
            Right: Dp.Lerp(a.Right, b.Right, t)
        );
    }
}