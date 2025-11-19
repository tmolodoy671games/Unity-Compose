using System;
using System.Collections;
using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, Func<IEnumerator> coroutine, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember<TKey, IDisposable>(key: new ComposeKey(FileName: filePath, MemberName: memberName, LineNumber: lineNumber), compareKey: key, defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(coroutine()));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, Action block, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(FileName: filePath, MemberName: memberName, LineNumber: lineNumber), compareKey: key, defaultValueFactory: _ =>
        {
            block();
            return string.Empty;
        });
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(FileName: filePath, MemberName: memberName, LineNumber: lineNumber), compareKey: key, defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block)));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __LaunchedEffect<TKey>(TKey key, float delay, Action block, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(FileName: filePath, MemberName: memberName, LineNumber: lineNumber), compareKey: key, defaultValueFactory: _ => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block)));
    }

    [Composable, DontGenerateComposeGroups]
    private static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposable> effect, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(filePath, memberName, lineNumber), compareKey: key, defaultValueFactory: _ => effect(DisposableEffectScopeImpl.Instance));
    }
}