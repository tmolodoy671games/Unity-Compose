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
        var composer = CurrentComposer;
        return composer.RememberedKeyChanged(0, key) ? composer.RememberedValue<TValue>() : composer.UpdateRememberedValue(defaultValueFactory);
    }

    [Composable, Compiled]
    public static TValue Remember<TValue>(
        Func<TValue> defaultValueFactory
    )
    {
        var composer = CurrentComposer;
        return composer.RememberedKeyChanged(0, 0) ? composer.RememberedValue<TValue>() : composer.UpdateRememberedValue(defaultValueFactory);
    }
}