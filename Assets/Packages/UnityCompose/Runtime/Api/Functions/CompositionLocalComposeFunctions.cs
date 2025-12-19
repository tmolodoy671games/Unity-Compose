using System;
using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static ICompositionLocal<T> CompositionLocalOf<T>(Func<T> defaultValue)
    {
        return new CompositionLocalImpl<T>(null, defaultValue);
    }
    
    public static ICompositionLocal<T> CompositionLocalOf<T>(string name, Func<T> defaultValue)
    {
        return new CompositionLocalImpl<T>(name, defaultValue);
    }

    [Composable]
    public static void CompositionLocalProvider(
        IImmutableStableList<CompositionLocalProvides> provides,
        ComposableContent content
    )
    {
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        ComposableContent content
    )
    {
        var provides = Remember(provides1, () => IImmutableStableList.Create(provides1));
        CompositionLocalProviderImpl(provides, content);
    }
    
    [Composable]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void LoggableCompositionLocalProvider(
        CompositionLocalProvides provides1,
        ComposableContent content
    )
    {
        var provides = Remember(provides1, () => IImmutableStableList.Create(provides1));
        LoggableCompositionLocalProviderImpl(provides, content);
    }
    
    [Composable]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void LoggableCompositionLocalProviderImpl(
        IImmutableStableList<CompositionLocalProvides> provides,
        ComposableContent content
    )
    {
        // Debug.Log($"StartLocalGroup({provides})");
        CurrentComposer.StartLocalGroup(321);
        CurrentComposer.UpdateCompositionLocal(provides, true);
        content();
        CurrentComposer.EndLocalGroup(321);
        // Debug.Log("EndLocalGroup()");
    }

    [Composable]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        ComposableContent content
    )
    {
        var provides = Remember((provides1, provides2), () => IImmutableStableList.Create(provides1, provides2));
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        ComposableContent content
    )
    {
        var provides = Remember((provides1, provides2, provides3), () =>
            IImmutableStableList.Create(provides1, provides2, provides3)
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        ComposableContent content
    )
    {
        var provides = Remember((provides1, provides2, provides3, provides4), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4)
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        ComposableContent content
    )
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5)
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        CompositionLocalProvides provides6,
        ComposableContent content
    )
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6)
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        CompositionLocalProvides provides6,
        CompositionLocalProvides provides7,
        ComposableContent content
    )
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6, provides7), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7)
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        CompositionLocalProvides provides6,
        CompositionLocalProvides provides7,
        CompositionLocalProvides provides8,
        ComposableContent content
    )
    {
        var provides = Remember(
            (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8),
            () => IImmutableStableList.Create(
                provides1,
                provides2,
                provides3,
                provides4,
                provides5,
                provides6,
                provides7,
                provides8
            )
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        CompositionLocalProvides provides6,
        CompositionLocalProvides provides7,
        CompositionLocalProvides provides8,
        CompositionLocalProvides provides9,
        ComposableContent content
    )
    {
        var provides = Remember(
            (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9),
            () => IImmutableStableList.Create(
                provides1,
                provides2,
                provides3,
                provides4,
                provides5,
                provides6,
                provides7,
                provides8,
                provides9
            )
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void CompositionLocalProviderImpl(
        IImmutableStableList<CompositionLocalProvides> provides,
        ComposableContent content
    )
    {
        CurrentComposer.StartLocalGroup(123);
        CurrentComposer.UpdateCompositionLocal(provides);
        content();
        CurrentComposer.EndLocalGroup(123);
    }
}