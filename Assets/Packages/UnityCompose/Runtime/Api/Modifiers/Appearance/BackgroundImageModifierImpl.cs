// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityBackground = UnityEngine.UIElements.Background;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Background(this IModifier modifier, Sprite image)
    {
        return modifier + new BackgroundImageModifierImpl(UnityBackground.FromSprite(image));
    }
    
    public static IModifier Background(this IModifier modifier, Texture2D image)
    {
        return modifier + new BackgroundImageModifierImpl(UnityBackground.FromTexture2D(image));
    }
    
    public static IModifier Background(this IModifier modifier, VectorImage image)
    {
        return modifier + new BackgroundImageModifierImpl(UnityBackground.FromVectorImage(image));
    }
    
    public static IModifier Background(this IModifier modifier, RenderTexture image)
    {
        return modifier + new BackgroundImageModifierImpl(UnityBackground.FromRenderTexture(image));
    }
}

internal class BackgroundImageModifierImpl : BaseModifier<BackgroundImageModifierImpl>
{
    private readonly UnityBackground _background;

    public BackgroundImageModifierImpl(UnityBackground background)
    {
        _background = background;
    }

    public override void Apply(VisualElement element)
    {
        element.style.backgroundImage = _background;
    }

    public override void Revert(VisualElement element)
    {
        element.style.backgroundImage = StyleKeyword.Null;
    }

    protected override bool Equals(BackgroundImageModifierImpl other)
    {
        return _background.Equals(other._background);
    }
}