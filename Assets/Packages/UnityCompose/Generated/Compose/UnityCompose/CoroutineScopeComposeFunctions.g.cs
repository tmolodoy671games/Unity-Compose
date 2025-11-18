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
        return Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>>(__ => () => new ComposeCoroutineScopeImpl()));
    }
}