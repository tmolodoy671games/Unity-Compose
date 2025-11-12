// ReSharper disable CheckNamespace

using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public readonly record struct LayoutCoordinates(
    // Size:
    float Width,
    float Height,

    // Paddings:
    float PaddingTop,
    float PaddingBottom,
    float PaddingLeft,
    float PaddingRight,

    // Margins:
    float MarginTop,
    float MarginBottom,
    float MarginLeft,
    float MarginRight,

    // Local:
    float LocalTop,
    float LocalBottom,
    float LocalLeft,
    float LocalRight,

    // Global:
    float GlobalTop,
    float GlobalBottom,
    float GlobalLeft,
    float GlobalRight
)
{
    public static LayoutCoordinates Create(VisualElement element)
    {
        var resolvedStyle = element.resolvedStyle;
        var worldBound = element.worldBound;
        return new LayoutCoordinates(
            // Size:
            Width: resolvedStyle.width - resolvedStyle.paddingLeft - resolvedStyle.paddingRight,
            Height: resolvedStyle.height - resolvedStyle.paddingTop - resolvedStyle.paddingBottom,

            // Paddings:
            PaddingTop: resolvedStyle.paddingTop,
            PaddingBottom: resolvedStyle.paddingBottom,
            PaddingLeft: resolvedStyle.paddingLeft,
            PaddingRight: resolvedStyle.paddingRight,

            // Margins:
            MarginTop: resolvedStyle.marginTop,
            MarginBottom: resolvedStyle.marginBottom,
            MarginLeft: resolvedStyle.marginLeft,
            MarginRight: resolvedStyle.marginRight,

            // Local:
            LocalTop: resolvedStyle.top,
            LocalBottom: resolvedStyle.bottom,
            LocalLeft: resolvedStyle.left,
            LocalRight: resolvedStyle.right,

            // Global:
            GlobalTop: worldBound.yMin,
            GlobalBottom: worldBound.yMax,
            GlobalLeft: worldBound.xMin,
            GlobalRight: worldBound.xMax
        );
    }

    public Vector2 LocalPosition => new(LocalLeft, LocalRight);
    public Vector2 GlobalPosition => new(GlobalLeft, GlobalRight);

    public Vector2 LocalCenter => new(
        x: (LocalLeft + LocalRight) / 2,
        y: (LocalTop + LocalBottom) / 2
    );

    public Vector2 GlobalCenter => new(
        x: (GlobalLeft + GlobalRight) / 2,
        y: (GlobalTop + GlobalBottom) / 2
    );

    public Vector2 Size => new(Width, Height);
    public Vector2 SizeWithPaddings => Size + Paddings;

    public Vector2 Margins => new(
        x: MarginLeft + MarginRight,
        y: MarginTop + MarginBottom
    );

    public Vector2 Paddings => new(
        x: PaddingLeft + PaddingRight,
        y: PaddingTop + PaddingBottom
    );

    public Vector2 LocalToGlobal(Vector2 localPosition)
    {
        var globalX = GlobalLeft + (localPosition.x - LocalLeft);
        var globalY = GlobalTop + (localPosition.y - LocalTop);
        return new Vector2(globalX, globalY);
    }

    public Vector2 GlobalToLocal(Vector2 globalPosition)
    {
        var localX = LocalLeft + (globalPosition.x - GlobalLeft);
        var localY = LocalTop + (globalPosition.y - GlobalTop);
        return new Vector2(localX, localY);
    }
}