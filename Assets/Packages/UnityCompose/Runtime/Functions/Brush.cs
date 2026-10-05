// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine.UIElements;
using Color = System.Drawing.Color;

namespace UnityCompose;

public static class Brush
{
    public static IBrush SolidColor(Color color) => new SolidColorBrushImpl(color);
    public static IBrush LinearGradient(Color color) => new SolidColorBrushImpl(color);
}

internal record SolidColorBrushImpl(Color Color) : IBrush
{
    public void Apply(object target, float alpha, DrawStyle style)
    {
        var color = Color.WithAlpha(alpha);
        switch (target)
        {
            case Painter2D painter2D:
                ApplyToPainter(painter2D, style);
                break;
            case UnityReusableComposeNode node:
                node.VisualElement.style.backgroundColor = color.ToUnityColor();
                break;
        }
    }

    public void Revert(object target, float alpha, DrawStyle style)
    {
        switch (target)
        {
            case UnityReusableComposeNode node:
                node.VisualElement.style.backgroundColor = StyleKeyword.Null;
                break;
        }
    }

    private void ApplyToPainter(Painter2D painter2D, DrawStyle style)
    {
        switch (style)
        {
            case DrawStyle.FillStyle:
                painter2D.fillColor = Color.ToUnityColor();
                break;
            case DrawStyle.StrokeStyle strokeStyle:
                painter2D.strokeColor = Color.ToUnityColor();
                painter2D.lineWidth = strokeStyle.Width;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(style));
        }
    }
}

internal static partial class SystemColorExtensions
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