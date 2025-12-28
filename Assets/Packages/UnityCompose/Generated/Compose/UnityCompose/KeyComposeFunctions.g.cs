using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __KeyImpl(ComposableContent content)
    {
        var __content = (content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(272469570);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__content))
        {
            content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(272469570, __isRestarted)?.UpdateScope(() => __KeyImpl(__content));
    }
}