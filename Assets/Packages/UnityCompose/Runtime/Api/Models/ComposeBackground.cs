// ReSharper disable CheckNamespace

using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public readonly struct ComposeImage
{
    internal readonly Sprite? Sprite;
    internal readonly VectorImage? VectorImage;
    internal readonly Texture? Texture;

    private ComposeImage(Sprite sprite) : this()
    {
        Sprite = sprite;
    }

    private ComposeImage(VectorImage vectorImage) : this()
    {
        VectorImage = vectorImage;
    }

    private ComposeImage(Texture texture) : this()
    {
        Texture = texture;
    }

    public static implicit operator ComposeImage(Sprite sprite)
    {
        return new ComposeImage(sprite);
    }

    public static implicit operator ComposeImage(VectorImage vectorImage)
    {
        return new ComposeImage(vectorImage);
    }

    public static implicit operator ComposeImage(Texture texture)
    {
        return new ComposeImage(texture);
    }

    public static implicit operator ComposeImage(RenderTexture texture)
    {
        return new ComposeImage(texture);
    }
}