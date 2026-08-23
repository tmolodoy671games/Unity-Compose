#nullable enable
using System.Diagnostics.CodeAnalysis;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public abstract partial class ComposePreview
{
    protected abstract void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1);
    private static void __EmptyPreview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(396282524);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(396282524, __isRestarted)?.UpdateScope(() => __EmptyPreview(__composer, 0));
    }
}