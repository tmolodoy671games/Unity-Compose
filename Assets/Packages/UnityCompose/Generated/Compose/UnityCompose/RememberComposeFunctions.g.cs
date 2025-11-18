using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static TValue __Remember<TKey, TValue>(TKey key, Func<TKey, TValue> defaultValueFactory, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(new ComposeKey(filePath, memberName, lineNumber), key, defaultValueFactory);
    }

    [Composable]
    private static TValue __Remember<TKey, TValue>(TKey key, Func<TValue> defaultValueFactory, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(new ComposeKey(filePath, memberName, lineNumber), key, CurrentComposer.WithState(defaultValueFactory).Remember<System.Func<TKey, TValue>>(__ => _ => defaultValueFactory()));
    }

    [Composable]
    private static TValue __Remember<TValue>(Func<TValue> defaultValueFactory, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember<int, TValue>(new ComposeKey(filePath, memberName, lineNumber), 0, CurrentComposer.WithState(defaultValueFactory).Remember<System.Func<int, TValue>>(__ => _ => defaultValueFactory()));
    }
}