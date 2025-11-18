using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(IImmutableStableList<CompositionLocalProvides> provides, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember(provides1, CurrentComposer.WithState(provides1).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2), CurrentComposer.WithState((provides1, provides2)).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1, provides2)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3), CurrentComposer.WithState((provides1, provides2, provides3)).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1, provides2, provides3)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3, provides4), CurrentComposer.WithState((provides1, provides2, provides3, provides4)).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1, provides2, provides3, provides4)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5), CurrentComposer.WithState((provides1, provides2, provides3, provides4, provides5)).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6), CurrentComposer.WithState((provides1, provides2, provides3, provides4, provides5, provides6)).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6, provides7), CurrentComposer.WithState((provides1, provides2, provides3, provides4, provides5, provides6, provides7)).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8), CurrentComposer.WithState((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8)).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, CompositionLocalProvides provides9, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerFilePath] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        var provides = Remember((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9), CurrentComposer.WithState((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9)).Remember<System.Func<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>>(__ => () => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9)));
        CompositionLocalProviderImpl(provides, content, filePath, memberName, lineNumber);
    }

    [Composable, DontGenerateComposeGroups]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void __CompositionLocalProviderImpl(IImmutableStableList<CompositionLocalProvides> provides, [Composable] Action content, string filePath, string memberName, int lineNumber)
    {
        ICompositionLocalProvider compositionLocal = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.Packages.UnityCompose.Runtime.Impl.CompositionLocalProvider>>(__ => () => new CompositionLocalProvider()), filePath, memberName, lineNumber);
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