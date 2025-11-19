using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    public static void Key(
        object key,
        [Composable] Action content,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        if (CurrentComposer.BeginComposeGroup((key, content),
                key: new ComposeKey(filePath, memberName, lineNumber, AdditionalKey: key))) return;
        try
        {
            content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => Key(key, content));
        }
    }
}