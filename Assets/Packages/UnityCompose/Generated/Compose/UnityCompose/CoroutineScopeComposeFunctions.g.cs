#nullable enable
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using StableCollections;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static IComposeCoroutineScope __RememberCoroutineScope()
    {
        var __composer = CurrentComposer;
        return !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>() : __composer.UpdateRememberedValue<UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>(new ComposeCoroutineScopeImpl());
    }
}