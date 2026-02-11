#nullable enable
// ReSharper disable CheckNamespace

using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    private static void __KeyImpl(ComposableContent content, global::UnityCompose.Composer __composer = null !)
    {
        var __content = (content);
        __composer.StartRestartGroup(2008433022);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__content))
        {
            content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(2008433022, __isRestarted)?.UpdateScope(() => __KeyImpl(__content));
    }

    private static void __KeyImpl(ComposableContent content)
    {
        __KeyImpl(content, CurrentComposer);
    }
}