using System;
using System.Diagnostics.CodeAnalysis;

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
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_1);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        content();
        composer.EndLocalGroup(123_1);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        ComposableContent content
    )
    {
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_2);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        content();
        composer.EndLocalGroup(123_2);
    }

    [Composable]
    public static void CompositionLocalProvider<T1, T2, T3>(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2,
        CompositionLocalProvides<T3> provides3,
        ComposableContent content
    )
    {
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_3);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        map.Set(provides3);
        content();
        composer.EndLocalGroup(123_3);
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
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_4);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        map.Set(provides3);
        map.Set(provides4);
        content();
        composer.EndLocalGroup(123_4);
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
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_5);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        map.Set(provides3);
        map.Set(provides4);
        map.Set(provides5);
        content();
        composer.EndLocalGroup(123_5);
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
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_6);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        map.Set(provides3);
        map.Set(provides4);
        map.Set(provides5);
        map.Set(provides6);
        content();
        composer.EndLocalGroup(123_6);
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
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_7);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        map.Set(provides3);
        map.Set(provides4);
        map.Set(provides5);
        map.Set(provides6);
        map.Set(provides7);
        content();
        composer.EndLocalGroup(123_7);
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
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_8);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        map.Set(provides3);
        map.Set(provides4);
        map.Set(provides5);
        map.Set(provides6);
        map.Set(provides7);
        map.Set(provides8);
        content();
        composer.EndLocalGroup(123_8);
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
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_9);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        map.Set(provides3);
        map.Set(provides4);
        map.Set(provides5);
        map.Set(provides6);
        map.Set(provides7);
        map.Set(provides8);
        map.Set(provides9);
        content();
        composer.EndLocalGroup(123_9);
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
        var composer = CurrentComposer;
        composer.StartLocalGroup(123_10);
        var map = composer.RequireCompositionLocalMap();
        map.Set(provides1);
        map.Set(provides2);
        map.Set(provides3);
        map.Set(provides4);
        map.Set(provides5);
        map.Set(provides6);
        map.Set(provides7);
        map.Set(provides8);
        map.Set(provides9);
        map.Set(provides10);
        content();
        composer.EndLocalGroup(123_10);
    }
}