// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static class ImageBitmap
{
    public static IImageBitmap ImageResource(Texture texture) => new TextureImageBitmapImpl(texture);
    public static IImageBitmap ImageResource(Texture2D texture) => new Texture2DImageBitmapImpl(texture);
    public static IImageBitmap ImageResource(Sprite sprite) => new SpriteImageBitmapImpl(sprite);
    public static IImageBitmap ImageResource(VectorImage vectorImage) => new VectorImageBitmapImpl(vectorImage);
}

internal record TextureImageBitmapImpl(
    Texture Texture
) : IImageBitmap
{
    public Size Size => new(Texture.width, Texture.height);

    public void Apply(object target)
    {
        switch (target)
        {
            case Image image:
                image.image = Texture;
                break;
            case MeshGenerationContext context:
                if (Texture is Texture2D texture2D)
                    context.painter2D.fillTexture = texture2D;
                break;
            default:
                throw new InvalidOperationException($"Unsupported target type: {target.GetType().Name}!");
        }
    }
}

internal record Texture2DImageBitmapImpl(
    Texture2D Texture
) : IImageBitmap
{
    public Size Size => new(Texture.width, Texture.height);

    public void Apply(object target)
    {
        switch (target)
        {
            case Image image:
                image.image = Texture;
                break;
            case MeshGenerationContext context:
                context.painter2D.fillTexture = Texture;
                break;
            default:
                throw new InvalidOperationException($"Unsupported target type: {target.GetType().Name}!");
        }
    }
}

internal record SpriteImageBitmapImpl(
    Sprite Sprite
) : IImageBitmap
{
    public Size Size => new(Sprite.rect.width, Sprite.rect.height);

    public void Apply(object target)
    {
        switch (target)
        {
            case Image image:
                image.sprite = Sprite;
                break;
            case MeshGenerationContext context:
                context.painter2D.fillTexture = Sprite.texture;
                break;
            default:
                throw new InvalidOperationException($"Unsupported target type: {target.GetType().Name}!");
        }
    }
}

internal record VectorImageBitmapImpl(
    VectorImage VectorImage
) : IImageBitmap
{
    public Size Size => new(VectorImage.width, VectorImage.height);

    public void Apply(object target)
    {
        switch (target)
        {
            case Image image:
                image.vectorImage = VectorImage;
                break;
            case MeshGenerationContext:
                throw new InvalidOperationException($"Unsupported target type: {target.GetType().Name}!");
            default:
                throw new InvalidOperationException($"Unsupported target type: {target.GetType().Name}!");
        }
    }
}