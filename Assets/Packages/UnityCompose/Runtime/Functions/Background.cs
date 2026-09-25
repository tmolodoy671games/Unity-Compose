// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine;

namespace UnityCompose;

public static partial class UnityComposeFunctions
{
    public static IModifier Background(
        this IModifier modifier,
        Color color,
        Optional<RoundedCornerShape> shape = default
    )
    {
        return new BackgroundColorModifierImpl(color, shape);
    }
}