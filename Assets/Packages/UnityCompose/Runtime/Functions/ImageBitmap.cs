// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine;

namespace UnityCompose;

public static class ImageBitmap
{
    public static IImageBitmap ImageResource(Texture texture) => new TextureImageBitmapImpl(texture);
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
            case UnityEngine.UIElements.Image image:
                image.image = Texture;
                break;
            case Action<Texture> action:
                action(Texture);
                break;
        }
    }
}