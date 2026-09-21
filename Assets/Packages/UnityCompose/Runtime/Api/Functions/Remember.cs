// ReSharper disable CheckNamespace

using System;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable, Compiled]
    public static T Remember<T>(Func<T> defaultValueFactory)
    {
        throw new InvalidOperationException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static T Remember<TKey, T>(TKey key, Func<T> defaultValueFactory)
    {
        throw new InvalidOperationException("Should be recompiled!");
    }
}