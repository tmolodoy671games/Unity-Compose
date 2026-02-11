#nullable enable
using Sirenix.OdinInspector;
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
    private void __Preview(global::UnityCompose.Composer __composer = null !)
    {
        __composer.StartRestartGroup(-1401336462);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute())
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1401336462, __isRestarted)?.UpdateScope(() => __Preview());
    }

    private void __Preview()
    {
        __Preview(CurrentComposer);
    }
}