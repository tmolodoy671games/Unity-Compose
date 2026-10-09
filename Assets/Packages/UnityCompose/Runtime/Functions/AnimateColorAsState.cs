// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine;

namespace UnityCompose;

public static partial class UnityComposeFunctions
{
    [Composable]
    public static IState<UnityEngine.Color> AnimateColorAsState(
        UnityEngine.Color targetValue,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return AnimateValueAsState(
            targetValue: targetValue,
            typeConverter: UnityEngine.Color.LerpUnclamped,
            animationSpec: animationSpec
        );
    }
}