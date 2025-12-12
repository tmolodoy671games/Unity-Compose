using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __Key<T>(T key, [Composable] Action content)
    {
        var(__key, __content) = (key, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-664152188);
        if (__composer.ShouldExecuteAsStruct((__key, __content)))
        {
            content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-664152188)?.UpdateScope(() => __Key(__key, __content));
    }
}