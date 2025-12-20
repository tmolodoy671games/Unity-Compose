using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;
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
        __composer.StartRestartGroup(2060217662);
        var __isRestarted = __composer.IsRestarted();
        if (__composer.ShouldExecute())
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(2060217662, __isRestarted)?.UpdateScope(() => __Preview());
    }
}