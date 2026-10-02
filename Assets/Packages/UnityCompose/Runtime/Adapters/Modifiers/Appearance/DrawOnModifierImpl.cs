// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class DrawOnModifierImpl : BaseModifier<DrawOnModifierImpl>
{
    private readonly Action<IDrawScope> _onDraw;
    private readonly Action<MeshGenerationContext> _generateVisualContent;

    public DrawOnModifierImpl(Action<IDrawScope> onDraw)
    {
        _onDraw = onDraw;
        _generateVisualContent = GenerateVisualContent;
    }

    public override void Apply(IReusableComposeNode node)
    {
        var unityNode = node.CastTo<UnityReusableComposeNode>();
        unityNode.SetupDrawOn();
        unityNode.DrawOn.generateVisualContent += _generateVisualContent;
    }

    public override void Revert(IReusableComposeNode node)
    {
        var unityNode = node.CastTo<UnityReusableComposeNode>();
        unityNode.RemoveDrawOn();
        unityNode.DrawOn.generateVisualContent -= _generateVisualContent;
    }

    protected override bool Equals(DrawOnModifierImpl other)
    {
        return _onDraw.Equals(other._onDraw) && 
               _generateVisualContent.Equals(other._generateVisualContent);
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        IDrawScope scope = new DrawScopeImpl(context);
        _onDraw(scope);
    }
}