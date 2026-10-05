// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class DrawModifierImpl : UnityModifier<DrawModifierImpl>, IAppearanceModifier
{
    private readonly Action<IDrawScope> _onDraw;
    private readonly Action<MeshGenerationContext> _generateVisualContent;

    public DrawModifierImpl(Action<IDrawScope> onDraw)
    {
        _onDraw = onDraw;
        _generateVisualContent = GenerateVisualContent;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var index = 0;
        for (var i = 0; i < newModifiers.Count; i++)
        {
            var newModifier = newModifiers[i];
            if (ReferenceEquals(newModifier, this))
                break;
            if (newModifier is DrawModifierImpl)
                index++;
        }
        element.GenerateVisualContent().Insert(index, _generateVisualContent);
        element.MarkDirtyRepaint();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.GenerateVisualContent().Remove(_generateVisualContent);
        element.MarkDirtyRepaint();
    }

    protected override bool Equals(DrawModifierImpl other)
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