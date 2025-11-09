using System;
using System.Runtime.CompilerServices;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static T __Remember<T>(Func<T> defaultValueFactory, [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(lineNumber, -1337, defaultValueFactory);
    }

    [Composable]
    [Compiled]
    private static T __Remember<T>(T defaultValueFactory, [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(lineNumber, -1337, () => defaultValueFactory);
    }

    [Composable]
    [Compiled]
    private static T __Remember<T>(object? key, Func<T> defaultValueFactory, [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(lineNumber, key, defaultValueFactory);
    }

    [Composable]
    [Compiled]
    private static T __Remember<T>(object? key, T defaultValueFactory, [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(lineNumber, key, () => defaultValueFactory);
    }

    [Composable]
    [Compiled]
    private static T __RememberComposable<T>(object? key, [Composable] T defaultValueFactory, [CallerLineNumber] int lineNumber = 0)
    {
        return CurrentComposer.Remember(lineNumber, key, () => defaultValueFactory);
    }
}