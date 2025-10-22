using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable, Compiled]
    public static T Remember<T>(
        Func<T> defaultValueFactory,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(lineNumber, -1337, defaultValueFactory);
    }

    [Composable, Compiled]
    public static T Remember<T>(
        T defaultValueFactory,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(lineNumber, -1337, () => defaultValueFactory);
    }

    [Composable, Compiled]
    public static T Remember<T>(
        object? key,
        Func<T> defaultValueFactory,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(lineNumber, key, defaultValueFactory);
    }

    [Composable, Compiled]
    public static T Remember<T>(
        object? key,
        T defaultValueFactory,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(lineNumber, key, () => defaultValueFactory);
    }

    [Composable, Compiled]
    public static T RememberComposable<T>(
        object? key,
        [Composable] T defaultValueFactory,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(lineNumber, key, () => defaultValueFactory);
    }
}