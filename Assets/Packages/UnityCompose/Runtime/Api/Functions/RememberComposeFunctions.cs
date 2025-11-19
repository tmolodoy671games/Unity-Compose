using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable]
    public static TValue Remember<TKey, TValue>(
        TKey key,
        Func<TKey, TValue> defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(new ComposeKey(filePath, memberName, lineNumber), key, defaultValueFactory);
    }

    [Composable]
    public static TValue Remember<TKey, TValue>(
        TKey key,
        Func<TValue> defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(
            new ComposeKey(filePath, memberName, lineNumber),
            key,
            _ => defaultValueFactory()
        );
    }
    
    [Composable]
    public static TValue Remember<TValue>(
        Func<TValue> defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember<int, TValue>(
            new ComposeKey(filePath, memberName, lineNumber),
            0,
            _ => defaultValueFactory()
        );
    }
}