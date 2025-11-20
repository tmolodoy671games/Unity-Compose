using System;
using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(IImmutableStableList<CompositionLocalProvides> provides, [Composable] Action content)
    {
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, [Composable] Action content)
    {
        var provides = Remember(provides1, () => IImmutableStableList.Create(provides1));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, [Composable] Action content)
    {
        var provides = Remember((provides1, provides2), () => IImmutableStableList.Create(provides1, provides2));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, [Composable] Action content)
    {
        var provides = Remember((provides1, provides2, provides3), () => IImmutableStableList.Create(provides1, provides2, provides3));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, [Composable] Action content)
    {
        var provides = Remember((provides1, provides2, provides3, provides4), () => IImmutableStableList.Create(provides1, provides2, provides3, provides4));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, [Composable] Action content)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5), () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, [Composable] Action content)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6), () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, [Composable] Action content)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6, provides7), () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, [Composable] Action content)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8), () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, CompositionLocalProvides provides9, [Composable] Action content)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9), () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void __CompositionLocalProviderImpl(IImmutableStableList<CompositionLocalProvides> provides, [Composable] Action content)
    {
        var(__provides, __content) = (provides, content);
        if (CurrentComposer.BeginComposeGroup((__provides, __content)))
            return;
        try
        {
            // ICompositionLocalProvider compositionLocal = Remember(() => new CompositionLocalProvider());
            CurrentComposer.BeginCompositionLocal(provides);
            content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__provides, __content)).Remember<Action>(__ => () => __CompositionLocalProviderImpl(__.__provides, __.__content)));
        }
    }
}