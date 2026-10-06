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
        VisualElement drawBehind,
        IStableList<IModifier> newModifiers
    )
    {
        var index = 0;
        for (var i = 0; i < newModifiers.Count; i++)
        {
            var newModifier = newModifiers[i];
            if (ReferenceEquals(newModifier, this))
                break;
            if (newModifier is DrawBehindModifierImpl)
                index++;
        }
        element.GenerateVisualContent().Insert(index, _generateVisualContent);
        element.MarkDirtyRepaint();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawBehind,
        IStableList<IModifier> newModifiers
    )
    {
        element.GenerateVisualContent().Remove(_generateVisualContent);
        element.MarkDirtyRepaint();
    }

    protected override bool Equals(DrawBehindModifierImpl other)
    {
        return _onDraw == other._onDraw;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_onDraw);
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        IDrawScope scope = context.DrawScope();
        _onDraw(scope);
    }
}