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
        return CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>(-1975442791, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>() : CurrentComposer.WriteValue<bool, UnityCompose.ComposeFunctions.ComposeCoroutineScopeImpl>(() => new ComposeCoroutineScopeImpl());
    }
}