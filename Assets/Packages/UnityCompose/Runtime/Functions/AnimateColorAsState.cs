// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine;

namespace UnityCompose;

public static partial class UnityComposeFunctions
{
    [Composable]
    public static IState<Color> AnimateColorAsState(
        Color targetValue,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return AnimateValueAsState(
            targetValue: targetValue,
            interpolator: Color.LerpUnclamped,
            animationSpec: animationSpec
        );
    }
    
    [Composable]
    public static IState<Color> AnimateColorAsState<TKey>(
        TKey key,
        Func<Color> targetValueFactory,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return AnimateValueAsState(
            key: key,
            targetValueFactory: targetValueFactory,
            interpolator: Color.LerpUnclamped,
            animationSpec: animationSpec
        );
    }
}