using System;
using System.Drawing;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Brushes;

internal record SolidColorBrushImpl(Color Color) : IBrush
{
    public void Apply(IDrawScope drawScope, Rect rect, float alpha, DrawStyle style)
    {
        var context = drawScope.Context();
        switch (style)
        {
            case DrawStyle.Fill:
                context.painter2D.fillColor = Color.ToUnityColor();
                break;
            case DrawStyle.Stroke strokeStyle:
                context.painter2D.strokeColor = Color.ToUnityColor();
                context.painter2D.lineWidth = strokeStyle.Width;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(style));
        }
    }
}

public static partial class SystemColorExtensions
{
    public static Color WithAlpha(this Color color, float alpha)
    {
        return Color.FromArgb(
            (int)(color.A * alpha),
            color.R,
            color.G,
            color.B
        );
    }
}