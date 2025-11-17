using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static ICompositionLocal<T> CompositionLocalOf<T>(Func<T> defaultValue)
    {
        return new CompositionLocalImpl<T>(defaultValue);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        IImmutableStableList<CompositionLocalProvides> provides,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember(provides1, () => IImmutableStableList.Create(provides1));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember((provides1, provides2), () => IImmutableStableList.Create(provides1, provides2));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember((provides1, provides2, provides3), () =>
            IImmutableStableList.Create(provides1, provides2, provides3)
        );
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember((provides1, provides2, provides3, provides4), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4)
        );
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5)
        );
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        CompositionLocalProvides provides6,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6)
        );
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        CompositionLocalProvides provides6,
        CompositionLocalProvides provides7,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6, provides7), () =>
            IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7)
        );
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        CompositionLocalProvides provides6,
        CompositionLocalProvides provides7,
        CompositionLocalProvides provides8,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember(
            (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8),
            () => IImmutableStableList.Create(
                provides1,
                provides2,
                provides3,
                provides4,
                provides5,
                provides6,
                provides7,
                provides8
            )
        );
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    public static void CompositionLocalProvider(
        CompositionLocalProvides provides1,
        CompositionLocalProvides provides2,
        CompositionLocalProvides provides3,
        CompositionLocalProvides provides4,
        CompositionLocalProvides provides5,
        CompositionLocalProvides provides6,
        CompositionLocalProvides provides7,
        CompositionLocalProvides provides8,
        CompositionLocalProvides provides9,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerFilePath] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var provides = Remember(
            (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9),
            () => IImmutableStableList.Create(
                provides1,
                provides2,
                provides3,
                provides4,
                provides5,
                provides6,
                provides7,
                provides8,
                provides9
            )
        );
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void CompositionLocalProviderImpl(
        IImmutableStableList<CompositionLocalProvides> provides,
        [Composable] Action content,
        string filePath,
        string memberName,
        int lineNumber
    )
    {
        ICompositionLocalProvider compositionLocal =
            Remember(() => new CompositionLocalProvider(), filePath, memberName, lineNumber);
        CurrentComposer.BeginCompositionLocal(compositionLocal, provides);
        try
        {
            content();
        }
        finally
        {
            CurrentComposer.EndCompositionLocal();
        }
    }
}