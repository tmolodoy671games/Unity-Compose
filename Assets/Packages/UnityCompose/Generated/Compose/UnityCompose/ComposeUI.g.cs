#nullable enable
using System;
using System.IO;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public abstract partial class ComposeUI
{
    protected abstract void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1);
    protected virtual void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(832656256);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(832656256, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
    }
}