using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static T __Remember<T>(Func<T> defaultValueFactory, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), -1337, defaultValueFactory);
    }

    [Composable]
    [Compiled]
    private static T __Remember<T>(T defaultValueFactory, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), -1337, () => defaultValueFactory);
    }

    [Composable]
    [Compiled]
    private static T __Remember<T>(object? key, Func<T> defaultValueFactory, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), key, defaultValueFactory);
    }

    [Composable]
    [Compiled]
    private static T __Remember<T>(object? key, T defaultValueFactory, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), key, () => defaultValueFactory);
    }

    [Composable]
    [Compiled]
    private static T __RememberComposable<T>(object? key, [Composable] T defaultValueFactory, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(new RememberId(filePath, lineNumber), key, () => defaultValueFactory);
    }
}