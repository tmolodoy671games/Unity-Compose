using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable, Compiled]
    public static void Key(
        object key,
        [Composable] Action content
    )
    {
        // BRUH
        content();
    }
}