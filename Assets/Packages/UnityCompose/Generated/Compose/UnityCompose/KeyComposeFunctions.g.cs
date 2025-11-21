using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    private static void __Key(object key, [Composable] Action content, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        // BRUH
        if (CurrentComposer.BeginComposeGroup(0))
            return;
        try
        {
            content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((key, content)).Remember<System.Action>(__ => () => Key(key, content)));
        }
    }
}