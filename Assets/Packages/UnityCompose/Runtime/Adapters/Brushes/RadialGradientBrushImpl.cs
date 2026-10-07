using System;
using System.Linq;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using Color = System.Drawing.Color;
using Rect = Compose.Net.Rect;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Brushes;

internal record RadialGradientBrushImpl(
    IStableList<Color> Colors,
    IStableList<float>? Stops,
    Optional<Offset> Center,
    float Radius,
    TileMode TileMode
) : IBrush
{
    public void Apply(IDrawScope drawScope, Rect rect, float alpha, DrawStyle style)
    {
        var context = drawScope.Context();
        switch (style)
        {
            case DrawStyle.Fill:
                context.painter2D.fillGradient = Create(rect, alpha);
                break;
            case DrawStyle.Stroke strokeStyle:
                context.painter2D.lineWidth = strokeStyle.Width;
                context.painter2D.strokeFillGradient = Create(rect, alpha);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(style));
        }
    }

    private FillGradient Create(Rect contentRect, float alpha)
    {
        var containerSize = contentRect.Size.ToVector2();
        var center = (Center.GetOrDefault(containerSize.ToOffset() / 2) + contentRect.Offset).ToVector2();

        var defaultRadius = Mathf.Max(
            Vector2.Distance(center, new Vector2(0, 0)),
            Vector2.Distance(center, new Vector2(containerSize.x, 0)),
            Vector2.Distance(center, new Vector2(0, containerSize.y)),
            Vector2.Distance(center, new Vector2(containerSize.x, containerSize.y))
        );
        return FillGradient.MakeRadialGradient(
            gradient: new Gradient
            {
                colorKeys = Colors
                    .Select((color, index) => new GradientColorKey(
                            color.ToUnityColor(),
                            Stops != null ? Stops[0] : index * 1f / Colors.Count
                        )
                    )
                    .ToArray(),
                alphaKeys = Colors
                    .Select((color, index) => new GradientAlphaKey(
                            color.A * alpha / 255f,
                            Stops != null ? Stops[0] : index * 1f / Colors.Count
                        )
                    )
                    .ToArray()
            },
            center: center,
            focus: center,
            radius: float.IsPositiveInfinity(Radius) ? defaultRadius : Radius,
            addressMode: TileMode switch
            {
                TileMode.Clamp => AddressMode.Clamp,
                TileMode.Repeated => AddressMode.Wrap,
                TileMode.Mirror => AddressMode.Mirror,
                _ => throw new ArgumentOutOfRangeException()
            }
        );
    }
}