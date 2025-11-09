// ReSharper disable CheckNamespace
using System;
using System.Collections;
using StableCollections;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static IComposeCoroutineScope __RememberCoroutineScope()
    {
        return Remember(() => new ComposeCoroutineScopeImpl());
    }
}