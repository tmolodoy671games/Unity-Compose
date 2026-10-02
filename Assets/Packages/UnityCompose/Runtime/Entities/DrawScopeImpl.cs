using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;
using Color = System.Drawing.Color;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class DrawScopeImpl : IDrawScope
{
    private readonly MeshGenerationContext _context;
    private FloatSize _size;

    public DrawScopeImpl(MeshGenerationContext context)
    {
        _context = context;
    }

    public object DrawContext => _context;
    public Offset Center => _context.visualElement.contentRect.center.ToOffset();
    public FloatSize Size => _context.visualElement.contentRect.size.ToFloatSize();

    public void DrawLine(
        Color color,
        Offset start,
        Offset end,
        float strokeWidth = 0.01f,
        StrokeCap strokeCap = StrokeCap.Butt,
        float alpha = 1
    )
    {
        var painter = _context.painter2D;

        painter.lineWidth = strokeWidth;
        painter.lineCap = strokeCap.ToUnityLineCap();

        var c = color.ToUnityColor();
        c.a *= alpha;
        painter.strokeColor = c;

        painter.BeginPath();
        painter.MoveTo(start.ToVector2());
        painter.LineTo(end.ToVector2());
        painter.Stroke();
    }

    public void DrawRect(
        Color color,
        Offset topLeft,
        Optional<FloatSize> size,
        float alpha,
        DrawStyle style = DrawStyle.Fill
    )
    {
        var painter = _context.painter2D;

        var c = color.ToUnityColor();
        c.a *= alpha;
        painter.fillColor = c;
        painter.strokeColor = c;
        var resolvedSize = size.GetOrDefault(Size);

        var rect = new Rect(
            topLeft.X,
            topLeft.Y,
            resolvedSize.Width,
            resolvedSize.Height
        );

        painter.BeginPath();
        painter.MoveTo(rect.min);
        painter.LineTo(new Vector2(rect.xMax, rect.yMin));
        painter.LineTo(rect.max);
        painter.LineTo(new Vector2(rect.xMin, rect.yMax));
        painter.ClosePath();

        if (style == DrawStyle.Fill)
            painter.Fill();
        else
            painter.Stroke();
    }

    public void DrawRoundRect(
        Color color,
        Offset topLeft = new(),
        Optional<FloatSize> size = new(),
        float cornerRadius = 0,
        float alpha = 1,
        DrawStyle style = DrawStyle.Fill
    )
    {
        var painter = _context.painter2D;

        var resolvedSize = size.GetOrDefault(Size);

        var x = topLeft.X;
        var y = topLeft.Y;
        var width = resolvedSize.Width;
        var height = resolvedSize.Height;

        var radius = Mathf.Clamp(
            cornerRadius,
            0f,
            Mathf.Min(width, height) / 2f
        );

        var c = color.ToUnityColor();
        c.a *= alpha;

        painter.BeginPath();

        // Top-left
        painter.MoveTo(new Vector2(x + radius, y));

        // Top-right
        painter.LineTo(new Vector2(x + width - radius, y));
        painter.ArcTo(
            new Vector2(x + width, y),
            new Vector2(x + width, y + radius),
            radius
        );

        // Bottom-right
        painter.LineTo(new Vector2(x + width, y + height - radius));
        painter.ArcTo(
            new Vector2(x + width, y + height),
            new Vector2(x + width - radius, y + height),
            radius
        );

        // Bottom-left
        painter.LineTo(new Vector2(x + radius, y + height));
        painter.ArcTo(
            new Vector2(x, y + height),
            new Vector2(x, y + height - radius),
            radius
        );

        // Top-left
        painter.LineTo(new Vector2(x, y + radius));
        painter.ArcTo(
            new Vector2(x, y),
            new Vector2(x + radius, y),
            radius
        );

        painter.ClosePath();

        switch (style)
        {
            case DrawStyle.Fill:
                painter.fillColor = c;
                painter.Fill();
                break;

            case DrawStyle.Stroke:
                painter.strokeColor = c;
                painter.Stroke();
                break;
        }
    }
}

public static class StrokeCapExtensions
{
    public static LineCap ToUnityLineCap(this StrokeCap cap) =>
        cap switch
        {
            StrokeCap.Butt => LineCap.Butt,
            StrokeCap.Round => LineCap.Round,
            _ => LineCap.Butt
        };
}