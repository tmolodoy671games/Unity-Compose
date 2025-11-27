using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable, Compiled]
    public static TValue Remember<TKey, TValue>(
        TKey key,
        Func<TValue> defaultValueFactory
    )
    {
        return CurrentComposer.HasRememberedValue<TKey, TValue>(-1337, key)
            ? CurrentComposer.RememberedValue<TKey, TValue>()
            : CurrentComposer.WriteValue<TKey, TValue>(defaultValueFactory);
    }

    [Composable, Compiled]
    public static TValue Remember<TValue>(
        Func<TValue> defaultValueFactory
    )
    {
        return CurrentComposer.HasRememberedValue<bool, TValue>(-1337, true)
            ? CurrentComposer.RememberedValue<bool, TValue>()
            : CurrentComposer.WriteValue<bool, TValue>(defaultValueFactory);
    }
}