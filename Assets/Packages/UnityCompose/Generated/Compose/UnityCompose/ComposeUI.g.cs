#nullable enable
using Sirenix.OdinInspector;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public abstract partial class ComposeUI
{
    protected void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(1401336462);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1401336462, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
    }

    private void __Preview()
    {
        __Preview(CurrentComposer, 0b_10);
    }
}