using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable]
    public static T Remember<T>(
        Func<T> defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), -1337, defaultValueFactory);
    }

    [Composable]
    public static T Remember<T>(
        T defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), -1337, () => defaultValueFactory);
    }

    [Composable]
    public static T Remember<T>(
        object? key,
        Func<T> defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), key, defaultValueFactory);
    }

    [Composable]
    public static T Remember<T>(
        object? key,
        T defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), key, () => defaultValueFactory);
    }

    [Composable]
    public static T RememberComposable<T>(
        object? key,
        [Composable] T defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), key, () => defaultValueFactory);
    }
}