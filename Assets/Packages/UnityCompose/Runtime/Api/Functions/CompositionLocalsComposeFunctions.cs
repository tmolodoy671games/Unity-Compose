// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static readonly ICompositionLocal<Optional<Color>> LocalContentColor =
        CompositionLocalOf(Optional.Empty<Color>);

    public static readonly ICompositionLocal<Optional<TextStyle>> LocalTextStyle =
        CompositionLocalOf(Optional.Empty<TextStyle>);

    public static readonly ICompositionLocal<float> LocalTextScale = CompositionLocalOf(() => 1f);
}