// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
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

    public override void Apply(IReusableComposeNode node, IStableList<IModifier> newModifiers)
    {
        var unityNode = node.CastTo<UnityReusableComposeNode>();
        unityNode.SetupDrawOn().generateVisualContent += _generateVisualContent;
    }

    public override void Revert(IReusableComposeNode node, IStableList<IModifier> newModifiers)
    {
        var unityNode = node.CastTo<UnityReusableComposeNode>();
        unityNode.SetupDrawOn().generateVisualContent -= _generateVisualContent;
        foreach (var newModifier in newModifiers)
        {
            if (newModifier is DrawOnModifierImpl)
                return;
        }

        unityNode.RemoveDrawOn();
    }

    protected override bool Equals(DrawOnModifierImpl other)
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