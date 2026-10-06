using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Brushes;
using UnityEngine;
using UnityEngine.UIElements;
using Color = System.Drawing.Color;
using Rect = Compose.Net.Rect;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class DrawScopeImpl : IDrawScope<MeshGenerationContext>
{
    private readonly MeshGenerationContext _context;
    private readonly IPathDrawer _drawer;
    private readonly Action<Texture2D> _consumeTexture2D;
    private Size _size;

    public DrawScopeImpl(MeshGenerationContext context)
    {
        _context = context;
        _drawer = new PathDrawerImpl(_context.painter2D);
        _consumeTexture2D = it => _context.painter2D.fillTexture = it;
    }

    public MeshGenerationContext Context => _context;


    public Offset Center => _context.visualElement.layout.center.ToOffset();
    public Size Size => _context.visualElement.layout.size.ToSize();

    #region DrawLine

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
        var c = color.ToUnityColor();
        c.a *= alpha;
        painter.strokeColor = c;
        DrawLineImpl(
            start: start,
            end: end,
            strokeWidth: strokeWidth,
            strokeCap: strokeCap
        );
    }

    public void DrawLine(
        IBrush brush,
        Offset start,
        Offset end,
        float strokeWidth = 1,
        StrokeCap strokeCap = StrokeCap.Butt,
        float alpha = 1
    )
    {
        var min = new Offset(
            X: Math.Min(start.X, end.X),
            Y: Math.Min(start.Y, end.Y)
        );
        var max = new Offset(
            X: Math.Max(start.X, end.X),
            Y: Math.Max(start.Y, end.Y)
        );
        var contentRect = new Rect(
            Offset: min,
            Size: (max - min).ToSize()
        );
        brush.Apply(_context, contentRect, alpha, DrawStyle.Stroke(strokeWidth));
        DrawLineImpl(
            start: start,
            end: end,
            strokeWidth: strokeWidth,
            strokeCap: strokeCap
        );
    }

    private void DrawLineImpl(
        Offset start,
        Offset end,
        float strokeWidth,
        StrokeCap strokeCap
    )
    {
        var painter = _context.painter2D;

        painter.lineWidth = strokeWidth;
        painter.lineCap = strokeCap.ToUnityLineCap();
        painter.BeginPath();
        painter.MoveTo(start.ToVector2());
        painter.LineTo(end.ToVector2());
        painter.Stroke();
    }

    #endregion

    #region DrawRect

    public void DrawRect(
        Color color,
        Offset topLeft,
        Optional<Size> size,
        float alpha,
        DrawStyle? style
    )
    {
        ApplyColor(color, alpha, style ?? DrawStyle.Fill);
        DrawRectImpl(
            topLeft: topLeft,
            size: size,
            style: style
        );
    }

    public void DrawRect(
        IBrush brush,
        Offset topLeft = new(),
        Optional<Size> size = new(),
        float alpha = 1,
        DrawStyle? style = null
    )
    {
        var contentRect = new Rect(
            Offset: topLeft,
            Size: size.GetOrDefault(Size)
        );
        brush.Apply(_context, contentRect, alpha, style ?? DrawStyle.Fill);
        DrawRectImpl(
            topLeft: topLeft,
            size: size,
            style: style
        );
    }

    private void DrawRectImpl(
        Offset topLeft = new(),
        Optional<Size> size = new(),
        DrawStyle? style = null
    )
    {
        var painter = _context.painter2D;
        var resolvedSize = size.GetOrDefault(Size);
        var rect = new UnityEngine.Rect(
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

        Draw(style ?? DrawStyle.Fill);
    }

    #endregion

    #region DrawRoundRect

    public void DrawRoundRect(
        Color color,
        Offset topLeft,
        Optional<Size> size,
        float cornerRadius,
        float alpha,
        DrawStyle? style
    )
    {
        ApplyColor(color, alpha, style ?? DrawStyle.Fill);
        DrawRoundRectImpl(
            topLeft: topLeft,
            size: size,
            cornerRadius: cornerRadius,
            style: style
        );
    }

    public void DrawRoundRect(
        IBrush brush,
        Offset topLeft,
        Optional<Size> size,
        float cornerRadius,
        float alpha,
        DrawStyle? style
    )
    {
        var contentRect = new Rect(
            Offset: topLeft,
            Size: size.GetOrDefault(Size)
        );
        brush.Apply(_context, contentRect, alpha, style ?? DrawStyle.Fill);
        DrawRoundRectImpl(
            topLeft: topLeft,
            size: size,
            cornerRadius: cornerRadius,
            style: style
        );
    }

    private void DrawRoundRectImpl(
        Offset topLeft,
        Optional<Size> size,
        float cornerRadius,
        DrawStyle? style
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
        Draw(style ?? DrawStyle.Fill);
    }

    #endregion

    #region DrawCircle

    public void DrawCircle(
        Color color,
        Optional<float> radius,
        Optional<Offset> center,
        float alpha,
        DrawStyle? style
    )
    {
        ApplyColor(color, alpha, style);
        DrawCircleImpl(radius, center, style);
    }

    public void DrawCircle(
        IBrush brush,
        Optional<float> radius,
        Optional<Offset> center,
        float alpha,
        DrawStyle? style
    )
    {
        var resolvedRadius = radius.GetOrDefault(Size.MinDimension / 2);
        var resolvedCenter = center.GetOrDefault(Center);
        var contentRect = new Rect(
            Offset: resolvedCenter - new Offset(resolvedRadius, resolvedRadius),
            Size: new Size(resolvedRadius, resolvedRadius)
        );
        brush.Apply(_context, contentRect, alpha, style ?? DrawStyle.Fill);
        DrawCircleImpl(radius, center, style);
    }

    private void DrawCircleImpl(
        Optional<float> radius,
        Optional<Offset> center,
        DrawStyle? style
    )
    {
        var painter = _context.painter2D;
        painter.BeginPath();
        painter.Arc(
            center.GetOrDefault(Center).ToVector2(),
            radius.GetOrDefault(Size.MinDimension / 2),
            0f,
            360f
        );
        painter.ClosePath();

        Draw(style ?? DrawStyle.Fill);
    }

    #endregion

    #region DrawOval

    public void DrawOval(
        Color color,
        Offset topLeft,
        Optional<Size> size,
        float alpha,
        DrawStyle? style
    )
    {
        ApplyColor(color, alpha, style);
        DrawOvalImpl(
            topLeft: topLeft,
            size: size,
            style: style
        );
    }

    public void DrawOval(
        IBrush brush,
        Offset topLeft,
        Optional<Size> size,
        float alpha,
        DrawStyle? style
    )
    {
        var contentRect = new Rect(
            Offset: topLeft,
            Size: size.GetOrDefault(Size)
        );
        brush.Apply(_context, contentRect, alpha, style ?? DrawStyle.Fill);
        DrawOvalImpl(
            topLeft: topLeft,
            size: size,
            style: style
        );
    }

    private void DrawOvalImpl(
        Offset topLeft,
        Optional<Size> size,
        DrawStyle? style
    )
    {
        var painter = _context.painter2D;
        var resolvedSize = size.GetOrDefault(
            new Size(
                Size.Width - topLeft.X,
                Size.Height - topLeft.Y
            )
        );

        var rect = new UnityEngine.Rect(
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

        Draw(style);
    }

    #endregion

    #region DrawArc

    public void DrawArc(
        Color color,
        float startAngle,
        float sweepAngle,
        bool useCenter,
        Offset topLeft,
        Optional<Size> size,
        float alpha,
        DrawStyle? style
    )
    {
        ApplyColor(color, alpha, style);
        DrawArcImpl(
            startAngle: startAngle,
            sweepAngle: sweepAngle,
            useCenter: useCenter,
            topLeft: topLeft,
            size: size,
            style: style
        );
    }

    public void DrawArc(
        IBrush brush,
        float startAngle,
        float sweepAngle,
        bool useCenter,
        Offset topLeft,
        Optional<Size> size,
        float alpha,
        DrawStyle? style
    )
    {
        var contentRect = new Rect(
            Offset: topLeft,
            Size: size.GetOrDefault(Size)
        );
        brush.Apply(_context, contentRect, alpha, style ?? DrawStyle.Fill);
        DrawArcImpl(
            startAngle: startAngle,
            sweepAngle: sweepAngle,
            useCenter: useCenter,
            topLeft: topLeft,
            size: size,
            style: style
        );
    }

    private void DrawArcImpl(
        float startAngle,
        float sweepAngle,
        bool useCenter,
        Offset topLeft,
        Optional<Size> size,
        DrawStyle? style
    )
    {
        var painter = _context.painter2D;

        var resolvedSize = size.GetOrDefault(
            new Size(
                Size.Width - topLeft.X,
                Size.Height - topLeft.Y
            )
        );

        var rect = new UnityEngine.Rect(
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

        Draw(style);
    }

    #endregion

    #region DrawPath

    public void DrawPath(IPath path, Color color, float alpha, DrawStyle? style)
    {
        ApplyColor(color, alpha, style);
        DrawPathImpl(path, style);
    }

    public void DrawPath(
        IPath path,
        IBrush brush,
        float alpha = 1,
        DrawStyle? style = null
    )
    {
        brush.Apply(_context, new Rect(), alpha, style ?? DrawStyle.Fill);
        DrawPathImpl(path, style);
    }

    private void DrawPathImpl(
        IPath path,
        DrawStyle? style
    )
    {
        path.Apply(_drawer);
        Draw(style);
    }

    #endregion

    #region DrawPoints

    public void DrawPoints(
        IStableList<Offset> points,
        PointMode pointMode,
        Color color,
        float strokeWidth,
        StrokeCap strokeCap,
        float alpha
    )
    {
        var style = pointMode switch
        {
            PointMode.Individual => DrawStyle.Stroke(strokeWidth),
            PointMode.Lines => DrawStyle.Stroke(strokeWidth),
            PointMode.Polygon => DrawStyle.Fill,
            _ => throw new ArgumentOutOfRangeException(nameof(pointMode), pointMode, null)
        };
        ApplyColor(color, alpha, style);
        DrawPointsImpl(points, pointMode, strokeWidth, strokeCap);
    }

    public void DrawPoints(
        IStableList<Offset> points,
        PointMode pointMode,
        IBrush brush,
        float strokeWidth,
        StrokeCap strokeCap,
        float alpha
    )
    {
        var style = pointMode switch
        {
            PointMode.Individual => DrawStyle.Stroke(strokeWidth),
            PointMode.Lines => DrawStyle.Stroke(strokeWidth),
            PointMode.Polygon => DrawStyle.Fill,
            _ => throw new ArgumentOutOfRangeException(nameof(pointMode), pointMode, null)
        };
        brush.Apply(_context, new Rect(), alpha, style);
        DrawPointsImpl(points, pointMode, strokeWidth, strokeCap);
    }

    private void DrawPointsImpl(
        IStableList<Offset> points,
        PointMode pointMode,
        float strokeWidth,
        StrokeCap strokeCap
    )
    {
        if (points.Count == 0)
            return;

        var painter = _context.painter2D;
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

    #endregion

    #region DrawPointLines

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

    #endregion

    #region DrawImage

    public void DrawImage(
        IImageBitmap image,
        Offset topLeft = new(),
        float alpha = 1,
        Optional<Size> size = new(),
        DrawStyle? style = null
    )
    {
        var drawSize = size.GetOrDefault(image.Size);
        image.Apply(_consumeTexture2D);
        DrawRectImpl(
            topLeft: topLeft,
            size: drawSize,
            style: style
        );
    }

    #endregion

    private void Draw(DrawStyle? style)
    {
        var painter = _context.painter2D;
        switch (style ?? DrawStyle.Fill)
        {
            case DrawStyle.FillStyle:
                painter.Fill();
                break;
            case DrawStyle.StrokeStyle stroke:
                painter.lineWidth = stroke.Width;
                painter.Stroke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(style));
        }
    }

    private void ApplyColor(Color color, float alpha, DrawStyle? style)
    {
        color = color.WithAlpha(alpha);
        var painter = _context.painter2D;
        switch (style ?? DrawStyle.Fill)
        {
            case DrawStyle.FillStyle:
                painter.fillColor = color.ToUnityColor();
                break;
            case DrawStyle.StrokeStyle stroke:
                painter.lineWidth = stroke.Width;
                painter.fillColor = color.ToUnityColor();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(style));
        }
    }
}

public static partial class VisualElementExtensions
{
    internal static IDrawScope DrawScope(this MeshGenerationContext context)
    {
        const string key = "UnityCompose_DrawScope";
        var visualElement = context.visualElement;
        if (visualElement.UserData().TryGet(key, out var cached))
            return (IDrawScope)cached.NotNull();
        var scope = new DrawScopeImpl(context);
        visualElement.UserData()[key] = scope;
        return scope;
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