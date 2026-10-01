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
            typeConverter: Color.LerpUnclamped,
            animationSpec: animationSpec
        );
    }
}