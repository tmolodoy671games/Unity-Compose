using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using Color = System.Drawing.Color;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class DrawScopeImpl : IDrawScope
{
    private readonly MeshGenerationContext _context;
    private Size _size;

    public DrawScopeImpl(MeshGenerationContext context)
    {
        _context = context;
    }

    public object DrawContext => _context;
    public Offset Center => _context.visualElement.contentRect.center.ToOffset();
    public Size Size => _context.visualElement.contentRect.size.ToSize();

    public void DrawLine(
        Color color,
        Offset start,
        Offset end,
        float strokeWidth,
        StrokeCap strokeCap,
        float alpha
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
        Optional<Size> size,
        float alpha,
        DrawStyle style
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
        Offset topLeft,
        Optional<Size> size,
        float cornerRadius,
        float alpha,
        DrawStyle style
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

    public void DrawCircle(
        Color color,
        Optional<float> radius,
        Optional<Offset> center,
        float alpha,
        DrawStyle style
    )
    {
        var painter = _context.painter2D;

        var c = color.ToUnityColor();
        c.a *= alpha;

        painter.BeginPath();

        painter.Arc(
            center.GetOrDefault(Center).ToVector2(),
            radius.GetOrDefault(Size.MinDimension / 2),
            0f,
            360f
        );

        painter.ClosePath();

        if (style == DrawStyle.Fill)
        {
            painter.fillColor = c;
            painter.Fill();
        }
        else
        {
            painter.strokeColor = c;
            painter.Stroke();
        }
    }

    public void DrawOval(
        Color color,
        Offset topLeft,
        Optional<Size> size,
        float alpha,
        DrawStyle style
    )
    {
        var painter = _context.painter2D;
        var resolvedSize = size.GetOrDefault(
            new Size(
                Size.Width - topLeft.X,
                Size.Height - topLeft.Y
            )
        );

        var c = color.ToUnityColor();
        c.a *= alpha;

        var rect = new Rect(
            topLeft.X,
            topLeft.Y,
            resolvedSize.Width,
            resolvedSize.Height
        );

        painter.BeginPath();

        painter.Arc(
            rect.center,
            Mathf.Min(rect.width, rect.height) / 2f,
            0f,
            360f
        );

        painter.ClosePath();

        if (style == DrawStyle.Fill)
        {
            painter.fillColor = c;
            painter.Fill();
        }
        else
        {
            painter.strokeColor = c;
            painter.Stroke();
        }
    }

    public void DrawArc(
        Color color,
        float startAngle,
        float sweepAngle,
        bool useCenter,
        Offset topLeft,
        Optional<Size> size,
        float alpha,
        DrawStyle style
    )
    {
        var painter = _context.painter2D;

        var resolvedSize = size.GetOrDefault(
            new Size(
                Size.Width - topLeft.X,
                Size.Height - topLeft.Y
            )
        );

        var c = color.ToUnityColor();
        c.a *= alpha;

        var rect = new Rect(
            topLeft.X,
            topLeft.Y,
            resolvedSize.Width,
            resolvedSize.Height
        );

        var center = rect.center;
        var radius = Mathf.Min(rect.width, rect.height) / 2f;

        painter.BeginPath();

        if (useCenter)
            painter.MoveTo(center);

        painter.Arc(
            center,
            radius,
            startAngle,
            startAngle + sweepAngle
        );

        if (useCenter)
            painter.ClosePath();

        if (style == DrawStyle.Fill)
        {
            painter.fillColor = c;
            painter.Fill();
        }
        else
        {
            painter.strokeColor = c;
            painter.Stroke();
        }
    }

    public void DrawPath(IPath path, Color color, float alpha, DrawStyle style)
    {
        var painter = _context.painter2D;

        var c = color.ToUnityColor();
        c.a *= alpha;

        var adapter = new PathDrawerImpl(painter);

        path.Apply(adapter);

        if (style == DrawStyle.Fill)
        {
            painter.fillColor = c;
            painter.Fill();
        }
        else
        {
            painter.strokeColor = c;
            painter.Stroke();
        }
    }

    public void DrawPoints(
        IStableList<Offset> points,
        PointMode pointMode,
        Color color,
        float strokeWidth,
        StrokeCap strokeCap,
        float alpha
    )
    {
        if (points.Count == 0)
            return;

        var painter = _context.painter2D;

        var c = color.ToUnityColor();
        c.a *= alpha;

        painter.strokeColor = c;
        painter.lineWidth = strokeWidth;
        painter.lineCap = strokeCap.ToUnityLineCap();

        switch (pointMode)
        {
            case PointMode.Individual:
                DrawPoints(painter, points);
                break;

            case PointMode.Lines:
                DrawPointLines(painter, points);
                break;

            case PointMode.Polygon:
                DrawPointPolygon(painter, points);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(pointMode), pointMode, null);
        }
    }

    private static void DrawPoints(
        Painter2D painter,
        IStableList<Offset> points
    )
    {
        var radius = painter.lineWidth / 2f;

        foreach (var point in points)
        {
            var p = point.ToVector2();

            painter.BeginPath();
            painter.Arc(p, radius, 0f, 360f);
            painter.ClosePath();
            painter.Fill();
        }
    }

    private static void DrawPointLines(
        Painter2D painter,
        IStableList<Offset> points
    )
    {
        for (var i = 0; i + 1 < points.Count; i += 2)
        {
            painter.BeginPath();
            painter.MoveTo(points[i].ToVector2());
            painter.LineTo(points[i + 1].ToVector2());
            painter.Stroke();
        }
    }

    private static void DrawPointPolygon(
        Painter2D painter,
        IStableList<Offset> points
    )
    {
        if (points.Count < 2)
            return;

        painter.BeginPath();
        painter.MoveTo(points[0].ToVector2());

        for (var i = 1; i < points.Count; i++)
            painter.LineTo(points[i].ToVector2());

        painter.Stroke();
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