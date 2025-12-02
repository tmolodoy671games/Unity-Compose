// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public readonly record struct LayoutInfo(
    Vector2 Position,
    Vector2 Size,
    Vector2 Center,
    Vector2 Min,
    Vector2 Max,
    float PaddingTop,
    float PaddingBottom,
    float PaddingLeft,
    float PaddingRight,
    float MarginTop,
    float MarginBottom,
    float MarginLeft,
    float MarginRight
)
{
    public float Height => Size.y;
    public float Width => Size.x;

    public static LayoutInfo From(VisualElement element)
    {
        var layout = element.layout;
        var resolvedStyle = element.resolvedStyle;
        return new LayoutInfo(
            Position: layout.position,
            Size: layout.size,
            Center: layout.center,
            Min: layout.min,
            Max: layout.max,
            PaddingTop: resolvedStyle.paddingTop,
            PaddingBottom: resolvedStyle.paddingBottom,
            PaddingLeft: resolvedStyle.paddingLeft,
            PaddingRight: resolvedStyle.paddingRight,
            MarginTop: resolvedStyle.marginTop,
            MarginBottom: resolvedStyle.marginBottom,
            MarginLeft: resolvedStyle.marginLeft,
            MarginRight: resolvedStyle.marginRight
        );
    }
}