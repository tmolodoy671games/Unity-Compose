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
        return Remember(() => new ComposeCoroutineScopeImpl());
    }
}