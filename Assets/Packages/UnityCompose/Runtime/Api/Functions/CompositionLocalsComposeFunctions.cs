// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    // private static readonly ICompositionLocal<(IModifier? Before, IModifier? After)> LocalModifier =
    //     CompositionLocalOf<(IModifier? Before, IModifier? After)>(static () => (null, null));

    // private static CompositionLocalProvides Provides(
    //     this ICompositionLocal<(IModifier? Before, IModifier? After)> localModifier,
    //     IModifier? before = null,
    //     IModifier? after = null
    // )
    // {
    //     return localModifier.Provides((before, after));
    // }

    public static readonly ICompositionLocal<Optional<Color>> LocalContentColor =
        CompositionLocalOf(Optional.Empty<Color>);

    public static readonly ICompositionLocal<Optional<TextStyle>> LocalTextStyle =
        CompositionLocalOf(Optional.Empty<TextStyle>);
}