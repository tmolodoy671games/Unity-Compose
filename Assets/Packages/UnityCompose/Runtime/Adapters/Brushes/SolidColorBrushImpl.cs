using System;
using System.Drawing;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Brushes;

internal record SolidColorBrushImpl(Color Color) : IBrush
{
    public void Apply(object target, Rect contentRect, float alpha, DrawStyle style)
    {
        var color = Color.WithAlpha(alpha);
        switch (target)
        {
            case MeshGenerationContext painter2D:
                ApplyToPainter(painter2D, style);
                break;
            case UnityReusableComposeNode node:
                node.VisualElement.style.backgroundColor = color.ToUnityColor();
                break;
        }
    }

    public void Revert(object target, Rect contentRect, float alpha, DrawStyle style)
    {
        switch (target)
        {
            case UnityReusableComposeNode node:
                node.VisualElement.style.backgroundColor = StyleKeyword.Null;
                break;
        }
    }

    private void ApplyToPainter(MeshGenerationContext context, DrawStyle style)
    {
        switch (style)
        {
            case DrawStyle.FillStyle:
                context.painter2D.fillColor = Color.ToUnityColor();
                break;
            case DrawStyle.StrokeStyle strokeStyle:
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