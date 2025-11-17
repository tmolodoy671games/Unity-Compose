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
    public static void __LaunchedEffect<TKey>(TKey key, Func<IEnumerator> coroutine, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(FileName: filePath, MemberName: memberName, LineNumber: lineNumber), compareKey: key, defaultValueFactory: CurrentComposer.WithState(coroutine).Remember<Func>(__ => _ => ComposeInvalidator.StartCoroutineAsDisposable(coroutine())));
    }

    [Composable, DontGenerateComposeGroups]
    public static void __LaunchedEffect<TKey>(TKey key, Action block, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(FileName: filePath, MemberName: memberName, LineNumber: lineNumber), compareKey: key, defaultValueFactory: CurrentComposer.WithState(block).Remember<Func>(__ => _ =>
        {
            block();
            return string.Empty;
        }));
    }

    [Composable, DontGenerateComposeGroups]
    public static void __LaunchedEffect<TKey>(TKey key, TimeSpan delay, Action block, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(FileName: filePath, MemberName: memberName, LineNumber: lineNumber), compareKey: key, defaultValueFactory: CurrentComposer.WithState((delay, block)).Remember<Func>(__ => _ => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(delay, block))));
    }

    [Composable, DontGenerateComposeGroups]
    public static void __LaunchedEffect<TKey>(TKey key, float delay, Action block, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(FileName: filePath, MemberName: memberName, LineNumber: lineNumber), compareKey: key, defaultValueFactory: CurrentComposer.WithState((delay, block)).Remember<Func>(__ => _ => ComposeInvalidator.StartCoroutineAsDisposable(RunDelayed(TimeSpan.FromSeconds(delay), block))));
    }

    [Composable, DontGenerateComposeGroups]
    public static void __DisposableEffect<TKey>(TKey key, Func<IDisposableEffectScope, IDisposable> effect, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        CurrentComposer.Remember(key: new ComposeKey(filePath, memberName, lineNumber), compareKey: key, defaultValueFactory: CurrentComposer.WithState(effect).Remember<Func>(__ => _ => effect(DisposableEffectScopeImpl.Instance)));
    }
}