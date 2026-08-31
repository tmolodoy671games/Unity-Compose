using System.Diagnostics.CodeAnalysis;

namespace UnityCompose.Packages.UnityCompose.Runtime.Api.Models;

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
}