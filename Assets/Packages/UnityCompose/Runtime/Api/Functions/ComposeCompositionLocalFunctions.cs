using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static ICompositionLocal<T> CompositionLocalOf<T>(Func<T> defaultValue)
    {
        return new CompositionLocalImpl<T>(defaultValue);
    }
        
    [Composable, Compiled]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider(
        IImmutableStableList<CompositionLocalProvides> provides,
        [Composable] Action content,
        [CallerLineNumber] int lineNumber = 0
    )
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
}