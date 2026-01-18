#nullable enable
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public abstract partial class ComposeUI
{
    [Composable]
    private void __Preview()
    {
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-885475722);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute())
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-885475722, __isRestarted)?.UpdateScope(() => __Preview());
    }
}