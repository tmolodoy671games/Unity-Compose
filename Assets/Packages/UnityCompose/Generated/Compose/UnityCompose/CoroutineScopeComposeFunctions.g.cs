using System;
using System.Collections;
using StableCollections;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    public static IComposeCoroutineScope __RememberCoroutineScope()
    {
        return Remember(static () => new ComposeCoroutineScopeImpl());
    }
}