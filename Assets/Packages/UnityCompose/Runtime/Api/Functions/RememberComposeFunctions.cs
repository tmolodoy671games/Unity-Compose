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
        throw new InvalidOperationException("Should be replaced when compiled!");
    }

    [Composable, Compiled]
    public static TValue Remember<TValue>(
        Func<TValue> defaultValueFactory
    )
    {
        throw new InvalidOperationException("Should be replaced when compiled!");
    }
}