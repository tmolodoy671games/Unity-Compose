using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Compiled]
    private static void __CompositionLocalProvider(IImmutableStableList<CompositionLocalProvides> provides, [Composable] Action content, [CallerLineNumber] int lineNumber = 0)
    {
        var compositionLocal = Remember(() => new CompositionLocal(), lineNumber);
        CurrentComposer.BeginCompositionLocal(compositionLocal, provides);
        try
        {
            content();
        }
        finally
        {
            CurrentComposer.EndCompositionLocal();
        }
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Compiled]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, [Composable] Action content, [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember(provides1, () => IImmutableStableList.Create(provides1));
        CompositionLocalProvider(provides, content, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Compiled]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, [Composable] Action content, [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2), () => IImmutableStableList.Create(provides1, provides2));
        CompositionLocalProvider(provides, content, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Compiled]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, [Composable] Action content, [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3), () => IImmutableStableList.Create(provides1, provides2, provides3));
        CompositionLocalProvider(provides, content, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Compiled]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, [Composable] Action content, [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3, provides4), () => IImmutableStableList.Create(provides1, provides2, provides3, provides4));
        CompositionLocalProvider(provides, content, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Compiled]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, [Composable] Action content, [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5), () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5));
        CompositionLocalProvider(provides, content, lineNumber);
    }
}