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
        [Composable] Action content
    )
    {
        // BRUH
        if (CurrentComposer.BeginComposeGroup(1337, 0, key)) return;
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