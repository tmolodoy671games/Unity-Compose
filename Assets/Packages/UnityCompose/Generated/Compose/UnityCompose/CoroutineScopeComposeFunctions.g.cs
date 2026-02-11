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
    private static IComposeCoroutineScope __RememberCoroutineScope(global::UnityCompose.Composer __composer = null !)
    {
        return (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>() : __composer.UpdateRememberedValue<UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>(new ComposeCoroutineScopeImpl()));
    }

    private static IComposeCoroutineScope __RememberCoroutineScope()
    {
        return __RememberCoroutineScope(CurrentComposer);
    }
}