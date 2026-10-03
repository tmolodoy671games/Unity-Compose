using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;
using ScaleMode = Compose.Net.ScaleMode;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class ImageNodeFactoryImpl : IImageNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new Image());
    }

    public void Apply(IReusableComposeNode node, object image, ScaleMode scaleMode)
    {
        var element = node.VisualElement().CastTo<Image>();
        switch (image)
        {
            case Sprite sprite:
                element.sprite = sprite;
                element.image = null;
                break;
            case Texture texture:
                element.sprite = null;
                element.image = texture; 
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(image), image, "Passing unknown type to Image");
        }

        element.scaleMode = scaleMode.ToUnityScaleMode();
    }
}

internal static class ScaleModeExtensions
{
    public static UnityEngine.ScaleMode ToUnityScaleMode(this ScaleMode scaleMode)
    {
        return scaleMode switch
        {
            ScaleMode.FillBounds => UnityEngine.ScaleMode.StretchToFill,
            ScaleMode.Crop => UnityEngine.ScaleMode.ScaleAndCrop,
            ScaleMode.Fit => UnityEngine.ScaleMode.ScaleToFit,
            _ => throw new ArgumentOutOfRangeException(nameof(scaleMode), scaleMode, null)
        };
    }
}