using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __Key(object key, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        if (CurrentComposer.BeginComposeGroup((key, content), key: new RememberId(filePath, lineNumber, AdditionalKey: key)))
            return;
        try
        {
            content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(Remember<global::System.Action>((key, content), () => Key(key, content)));
        }
    }
}