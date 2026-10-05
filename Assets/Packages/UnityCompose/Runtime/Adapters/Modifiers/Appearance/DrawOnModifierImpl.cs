// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class DrawOnModifierImpl : DrawOnUnityModifier<DrawOnModifierImpl>, IAppearanceModifier
{
    private readonly Action<IDrawScope> _onDraw;
    private readonly Action<MeshGenerationContext> _generateVisualContent;

    public DrawOnModifierImpl(Action<IDrawScope> onDraw)
    {
        _onDraw = onDraw;
        _generateVisualContent = GenerateVisualContent;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawOn,
        IStableList<IModifier> newModifiers
    )
    {
        drawOn.generateVisualContent += _generateVisualContent;
        drawOn.MarkDirtyRepaint();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawOn,
        IStableList<IModifier> newModifiers
    )
    {
        drawOn.generateVisualContent -= _generateVisualContent;
        drawOn.MarkDirtyRepaint();
    }

    protected override bool Equals(DrawOnModifierImpl other)
    {
        return _onDraw == other._onDraw;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onDraw);
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        IDrawScope scope = new DrawScopeImpl(context);
        _onDraw(scope);
    }
}

public static class DrawScopeExtensions
{
    public static void DrawImageScreen(
        this IDrawScope scope,
        ILayoutCoordinates coordinates,
        IImageBitmap image,
        float alpha = 1f
    )
    {
        var topLeft = coordinates.RootToLocal(Offset.Zero);

        var bottomRight = coordinates.RootToLocal(
            new Offset(Screen.width, Screen.height)
        );

        var size = new Size(
            bottomRight.X - topLeft.X,
            bottomRight.Y - topLeft.Y
        );

        scope.DrawImage(
            image,
            topLeft: topLeft,
            alpha: alpha,
            size: size
        );
    }
}