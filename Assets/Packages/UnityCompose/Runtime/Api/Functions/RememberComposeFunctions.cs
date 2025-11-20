using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable]
    public static TValue Remember<TKey, TValue>(
        TKey key,
        Func<TKey, TValue> defaultValueFactory
    )
    {
        return CurrentComposer.Remember(key, defaultValueFactory);
    }

    [Composable]
    public static TValue Remember<TKey, TValue>(
        TKey key,
        Func<TValue> defaultValueFactory
    )
    {
        return CurrentComposer.Remember(
            key,
            defaultValueFactory
        );
    }

    [Composable]
    public static TValue Remember<TValue>(
        Func<TValue> defaultValueFactory
    )
    {
        return CurrentComposer.Remember(
            ComposeRememberKey.None,
            defaultValueFactory
        );
    }
}