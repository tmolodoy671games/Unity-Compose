using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable]
    public static void Key<T>(
        T key,
        [Composable] Action content
    )
    {
        // BRUH
        content();
    }
}