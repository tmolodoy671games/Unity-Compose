#nullable enable
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using StableCollections;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public partial interface IModifier
{
    IModifier __Compose(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        return this;
    }

    void __DrawBefore(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(1531383956);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1531383956, __isRestarted)?.UpdateScope(() => __DrawBefore(__composer, 0));
    }

    void __DrawAfter(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(1975236316);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1975236316, __isRestarted)?.UpdateScope(() => __DrawAfter(__composer, 0));
    }
}

public abstract partial class BaseComposableModifier<T>
    where T : BaseComposableModifier<T>
{
    public void __DrawBefore(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(723220195);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(723220195, __isRestarted)?.UpdateScope(() => __DrawBefore(__composer, 0));
    }

    public virtual void __DrawAfter(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(1178099079);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1178099079, __isRestarted)?.UpdateScope(() => __DrawAfter(__composer, 0));
    }
}