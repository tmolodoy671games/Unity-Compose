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
                ApplyToVisualElement(node.VisualElement(), style);
                break;
        }
    }

    public void Revert(object target, Rect contentRect, float alpha, DrawStyle style)
    {
        switch (target)
        {
            case UnityReusableComposeNode node:
                RevertToVisualElement(node.VisualElement(), style);
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

    private void ApplyToVisualElement(VisualElement element, DrawStyle style)
    {
        if (style == DrawStyle.Fill)
            element.style.backgroundColor = Color.ToUnityColor();
        else if (style is DrawStyle.FillStyle)
        {
            element.style.borderTopColor = Color.ToUnityColor();
            element.style.borderBottomColor = Color.ToUnityColor();
            element.style.borderLeftColor = Color.ToUnityColor();
            element.style.borderRightColor = Color.ToUnityColor();
        }
    }

    private void RevertToVisualElement(VisualElement element, DrawStyle style)
    {
        
        if (style == DrawStyle.Fill)
            element.style.backgroundColor = StyleKeyword.Null;
        else if (style is DrawStyle.FillStyle)
        {
            element.style.borderTopColor = StyleKeyword.Null;
            element.style.borderBottomColor = StyleKeyword.Null;
            element.style.borderLeftColor = StyleKeyword.Null;
            element.style.borderRightColor = StyleKeyword.Null;
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