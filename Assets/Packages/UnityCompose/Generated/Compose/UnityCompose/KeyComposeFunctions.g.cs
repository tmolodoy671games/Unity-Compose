using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __Key<T>(T key, ComposableContent content)
    {
        var(__key, __content) = (key, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-735268864);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__key, __content)))
        {
            content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-735268864, __isRestarted)?.UpdateScope(() => __Key(__key, __content));
    }
}