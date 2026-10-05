// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class DrawBehindModifierImpl : DrawBehindUnityModifier<DrawBehindModifierImpl>, IAppearanceModifier
{
    private readonly Action<IDrawScope> _onDraw;
    private readonly Action<MeshGenerationContext> _generateVisualContent;

    public DrawBehindModifierImpl(Action<IDrawScope> onDraw)
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

    protected override bool Equals(DrawBehindModifierImpl other)
    {
        return _onDraw.Equals(other._onDraw) &&
               _generateVisualContent.Equals(other._generateVisualContent);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onDraw, _generateVisualContent);
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        IDrawScope scope = new DrawScopeImpl(context);
        _onDraw(scope);
    }
}