#nullable enable
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using StableCollections;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class ComposeFunctions
{
    public static IComposeCoroutineScope __RememberCoroutineScope(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        return (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>(new ComposeCoroutineScopeImpl()));
    }
}