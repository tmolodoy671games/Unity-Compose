// ReSharper disable CheckNamespace

using System;
using Compose.Net;
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
            case Action<Texture> action:
                action(Texture);
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
            case Action<Texture2D> action:
                action(Texture);
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
            default:
                throw new InvalidOperationException($"Unsupported target type: {target.GetType().Name}!");
        }
    }
}