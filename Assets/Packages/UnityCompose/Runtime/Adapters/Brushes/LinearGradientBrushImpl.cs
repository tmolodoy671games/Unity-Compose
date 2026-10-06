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

internal record LinearGradientBrushImpl(
    IStableList<Color> Colors,
    IStableList<float>? Stops,
    Offset Start,
    Offset End,
    TileMode TileMode
) : IBrush
{
    public void Apply(object target, Rect contentRect, float alpha, DrawStyle style)
    {
        switch (target)
        {
            case MeshGenerationContext context:
                ApplyToContext(context, contentRect, alpha, style);
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    public void Revert(object target, Rect contentRect, float alpha, DrawStyle style)
    {
        throw new InvalidOperationException();
    }

    private void ApplyToContext(MeshGenerationContext context, Rect contentRect, float alpha, DrawStyle style)
    {
        switch (style)
        {
            case DrawStyle.FillStyle:
                context.painter2D.fillGradient = Create(contentRect, alpha);
                break;
            case DrawStyle.StrokeStyle strokeStyle:
                context.painter2D.lineWidth = strokeStyle.Width;
                context.painter2D.strokeFillGradient = Create(contentRect, alpha);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(style));
        }
    }

    private FillGradient Create(Rect contentRect, float alpha)
    {
        var start = Start + contentRect.Offset;
        var end = new Offset(
            X: float.IsPositiveInfinity(End.X) ? contentRect.Size.Width : End.X,
            Y: float.IsPositiveInfinity(End.Y) ? contentRect.Size.Width : End.Y
        ) + contentRect.Offset;
        var gradient = new Gradient
        {
            colorKeys = Colors
                .Select((color, index) => new GradientColorKey(
                    color.ToUnityColor(),
                    Stops != null
                        ? Stops[index]
                        : (float)index / (Colors.Count - 1)
                ))
                .ToArray(),

            alphaKeys = Colors
                .Select((color, index) => new GradientAlphaKey(
                    color.A * alpha / 255f,
                    Stops != null
                        ? Stops[index]
                        : (float)index / (Colors.Count - 1)
                ))
                .ToArray()
        };
        return new FillGradient
        {
            gradient = gradient,
            start = start.ToVector2(),
            end = end.ToVector2(),
            addressMode = TileMode switch
            {
                TileMode.Clamp => AddressMode.Clamp,
                TileMode.Repeated => AddressMode.Wrap,
                TileMode.Mirror => AddressMode.Mirror,
                _ => throw new ArgumentOutOfRangeException()
            },
            gradientType = GradientType.Linear
        };
        ;
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        var scope = context.DrawScope();
        scope.DrawRoundRect(
            brush: this
        );
    }
}