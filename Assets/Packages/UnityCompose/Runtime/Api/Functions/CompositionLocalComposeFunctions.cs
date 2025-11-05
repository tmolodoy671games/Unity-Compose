using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using SharpExtensions;
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

    [Composable, Compiled]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        [Composable] Action content,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var compositionLocal = Remember(() => new CompositionLocal(), lineNumber);
        var provides = Remember(provides1, () => IImmutableStableList.Create(provides1));
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

    [Composable, Compiled]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        [Composable] Action content,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var compositionLocal = Remember(() => new CompositionLocal(), lineNumber);
        var provides = Remember((provides1, provides2), () => IImmutableStableList.Create(provides1, provides2));
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

    [Composable, Compiled]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        [Composable] Action content,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var compositionLocal = Remember(() => new CompositionLocal(), lineNumber);
        var provides = Remember((provides1, provides2, provides3), () =>
            IImmutableStableList.Create(provides1, provides2, provides3)
        );
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

    [Composable, Compiled]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        [Composable] Action content,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var compositionLocal = Remember(() => new CompositionLocal(), lineNumber);
        var provides = Remember((provides1, provides2, provides3, provides4), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4)
        );
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

    [Composable, Compiled]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        [Composable] Action content,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var compositionLocal = Remember(() => new CompositionLocal(), lineNumber);
        var provides = Remember((provides1, provides2, provides3, provides4, provides5), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5)
        );
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