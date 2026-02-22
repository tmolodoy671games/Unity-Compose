// ReSharper disable CheckNamespace

using System;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    // public static IModifier DrawBefore(this IModifier modifier, Action<MeshGenerationContext> draw)
    // {
    //     return modifier + new DrawWithContentModifierImpl(draw, DrawMode.Before);
    // }

    public static IModifier DrawAfter(this IModifier modifier, Action<MeshGenerationContext> draw)
    {
        return modifier + new DrawWithContentModifierImpl(draw, DrawMode.After);
    }
}

internal class DrawWithContentModifierImpl : BaseModifier<DrawWithContentModifierImpl>
{
    private readonly Action<MeshGenerationContext> _generateVisualContent;
    private readonly DrawMode _drawMode;

    public DrawWithContentModifierImpl(Action<MeshGenerationContext> callback, DrawMode drawMode)
    {
        _generateVisualContent = callback;
        _drawMode = drawMode;
    }

    public override void Apply(VisualElement element)
    {
        switch (_drawMode)
        {
            case DrawMode.Before:
                element.ComposeGenerateVisualContent().AddBefore(_generateVisualContent);
                break;
            case DrawMode.After:
                element.ComposeGenerateVisualContent().AddAfter(_generateVisualContent);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        element.MarkDirtyRepaint();
    }

    public override void Revert(VisualElement element)
    {
        element.ComposeGenerateVisualContent().Remove(_generateVisualContent);
    }

    protected override bool Equals(DrawWithContentModifierImpl other)
    {
        return _generateVisualContent == other._generateVisualContent && _drawMode == other._drawMode;
    }
}

internal enum DrawMode
{
    Before,
    After,
}