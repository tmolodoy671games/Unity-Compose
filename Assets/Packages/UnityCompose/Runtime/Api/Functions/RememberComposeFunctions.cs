using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable, Compiled]
    public static TValue Remember<TKey, TValue>(
        TKey key,
        Func<TValue> defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.HasRememberedValue<TKey, TValue>(key, filePath, lineNumber)
            ? CurrentComposer.RememberedValue<TKey, TValue>()
            : CurrentComposer.WriteValue<TKey, TValue>(defaultValueFactory);
    }

    [Composable, Compiled]
    public static TValue Remember<TValue>(
        Func<TValue> defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.HasRememberedValue<bool, TValue>(true, filePath, lineNumber)
            ? CurrentComposer.RememberedValue<bool, TValue>()
            : CurrentComposer.WriteValue<bool, TValue>(defaultValueFactory);
    }
}