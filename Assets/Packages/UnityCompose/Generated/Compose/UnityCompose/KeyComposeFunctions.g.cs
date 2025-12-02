using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, Compiled]
    private static void __Key(object key, [Composable] Action content)
    {
        var __composer = CurrentComposer;
        // BRUH
        content();
    }
}