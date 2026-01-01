using System;
using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
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
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider<T1>(
        CompositionLocalProvides<T1> provides1,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, Unit, Unit, Unit, Unit, Unit, Unit, Unit, Unit, Unit>()
        );
        provides.Update(provides1);
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, Unit, Unit, Unit, Unit, Unit, Unit, Unit, Unit>()
        );
        provides.Update(provides1, provides2);
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, T3, Unit, Unit, Unit, Unit, Unit, Unit, Unit>()
        );
        provides.Update(provides1, provides2, provides3);
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3, T4>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        CompositionLocalProvides<T4> provides4,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, T3, T4, Unit, Unit, Unit, Unit, Unit, Unit>()
        );
        provides.Update(provides1, provides2, provides3, provides4);
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3, T4, T5>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        CompositionLocalProvides<T4> provides4,
        CompositionLocalProvides<T5> provides5,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, T3, T4, T5, Unit, Unit, Unit, Unit, Unit>()
        );
        provides.Update(provides1, provides2, provides3, provides4, provides5);
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3, T4, T5, T6>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        CompositionLocalProvides<T4> provides4,
        CompositionLocalProvides<T5> provides5,
        CompositionLocalProvides<T6> provides6,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, Unit, Unit, Unit, Unit>()
        );
        provides.Update(provides1, provides2, provides3, provides4, provides5, provides6);
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        CompositionLocalProvides<T4> provides4,
        CompositionLocalProvides<T5> provides5,
        CompositionLocalProvides<T6> provides6,
        CompositionLocalProvides<T7> provides7,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, Unit, Unit, Unit>()
        );
        provides.Update(provides1, provides2, provides3, provides4, provides5, provides6, provides7);
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        CompositionLocalProvides<T4> provides4,
        CompositionLocalProvides<T5> provides5,
        CompositionLocalProvides<T6> provides6,
        CompositionLocalProvides<T7> provides7,
        CompositionLocalProvides<T8> provides8,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, Unit, Unit>()
        );
        provides.Update(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8);
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        CompositionLocalProvides<T4> provides4,
        CompositionLocalProvides<T5> provides5,
        CompositionLocalProvides<T6> provides6,
        CompositionLocalProvides<T7> provides7,
        CompositionLocalProvides<T8> provides8,
        CompositionLocalProvides<T9> provides9,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, Unit>()
        );
        provides.Update(
            provides1,
            provides2,
            provides3,
            provides4,
            provides5,
            provides6,
            provides7,
            provides8,
            provides9
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        CompositionLocalProvides<T4> provides4,
        CompositionLocalProvides<T5> provides5,
        CompositionLocalProvides<T6> provides6,
        CompositionLocalProvides<T7> provides7,
        CompositionLocalProvides<T8> provides8,
        CompositionLocalProvides<T9> provides9,
        CompositionLocalProvides<T10> provides10,
        ComposableContent content
    )
    {
        var provides = Remember(() =>
            new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        );
        provides.Update(
            provides1,
            provides2,
            provides3,
            provides4,
            provides5,
            provides6,
            provides7,
            provides8,
            provides9,
            provides10
        );
        CompositionLocalProviderImpl(provides, content);
    }

    [Composable, Compiled]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void CompositionLocalProviderImpl<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> provides,
        ComposableContent content
    )
    {
        CurrentComposer.StartLocalGroup(123);
        CurrentComposer.UpdateCompositionLocal(provides);
        content();
        CurrentComposer.EndLocalGroup(123);
    }
}