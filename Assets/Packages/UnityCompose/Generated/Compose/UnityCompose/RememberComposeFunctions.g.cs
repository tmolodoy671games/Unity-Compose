using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static TValue __Remember<TKey, TValue>(TKey key, Func<TKey, TValue> defaultValueFactory)
    {
        return CurrentComposer.Remember(key, defaultValueFactory);
    }

    [Composable]
    private static TValue __Remember<TKey, TValue>(TKey key, Func<TValue> defaultValueFactory)
    {
        return CurrentComposer.Remember(key, defaultValueFactory);
    }

    [Composable]
    private static TValue __Remember<TValue>(Func<TValue> defaultValueFactory)
    {
        return CurrentComposer.Remember(ComposeRememberKey.None, defaultValueFactory);
    }
}