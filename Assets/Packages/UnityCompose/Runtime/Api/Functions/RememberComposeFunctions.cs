using System;
using System.Runtime.CompilerServices;
using SharpExtensions;

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
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TValue>(
        TKey0 key0,
        TKey1 key1,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TKey2, TValue>(
        TKey0 key0,
        TKey1 key1,
        TKey2 key2,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TKey2, TKey3, TValue>(
        TKey0 key0,
        TKey1 key1,
        TKey2 key2,
        TKey3 key3,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TKey2, TKey3, TKey4, TValue>(
        TKey0 key0,
        TKey1 key1,
        TKey2 key2,
        TKey3 key3,
        TKey4 key4,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TKey2, TKey3, TKey4, TKey5, TValue>(
        TKey0 key0,
        TKey1 key1,
        TKey2 key2,
        TKey3 key3,
        TKey4 key4,
        TKey5 key5,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>(
        TKey0 key0,
        TKey1 key1,
        TKey2 key2,
        TKey3 key3,
        TKey4 key4,
        TKey5 key5,
        TKey6 key6,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>(
        TKey0 key0,
        TKey1 key1,
        TKey2 key2,
        TKey3 key3,
        TKey4 key4,
        TKey5 key5,
        TKey6 key6,
        TKey7 key7,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TValue>(
        TKey0 key0,
        TKey1 key1,
        TKey2 key2,
        TKey3 key3,
        TKey4 key4,
        TKey5 key5,
        TKey6 key6,
        TKey7 key7,
        TKey8 key8,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }
    
    [Composable, Compiled]
    public static TValue Remember<TKey0, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TValue>(
        TKey0 key0,
        TKey1 key1,
        TKey2 key2,
        TKey3 key3,
        TKey4 key4,
        TKey5 key5,
        TKey6 key6,
        TKey7 key7,
        TKey8 key8,
        TKey9 key9,
        Func<TValue> defaultValueFactory
    )
    {
        throw new IllegalStateException("Should be recompiled!");
    }

    [Composable, Compiled]
    public static TValue Remember<TValue>(
        Func<TValue> defaultValueFactory
    )
    {
        var composer = CurrentComposer;
        return !composer.Changed()
            ? composer.RememberedValue<TValue>()
            : composer.UpdateRememberedValue(defaultValueFactory);
    }
}