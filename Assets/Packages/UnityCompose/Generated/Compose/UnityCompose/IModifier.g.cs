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

public abstract partial class BaseModifier<T>
    where T : BaseModifier<T>
{
    public virtual void __DrawBefore(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(425863812);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(425863812, __isRestarted)?.UpdateScope(() => __DrawBefore(__composer, 0));
    }

    public virtual void __DrawAfter(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(1621847148);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1621847148, __isRestarted)?.UpdateScope(() => __DrawAfter(__composer, 0));
    }
}

public abstract partial class BaseComposableModifier<T>
    where T : BaseComposableModifier<T>
{
    public void __DrawBefore(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(161664111);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(161664111, __isRestarted)?.UpdateScope(() => __DrawBefore(__composer, 0));
    }

    public virtual void __DrawAfter(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(914191611);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(914191611, __isRestarted)?.UpdateScope(() => __DrawAfter(__composer, 0));
    }
}

internal partial class CompositeModifierImpl
{
    public override void __DrawBefore(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(890049012);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
            _first.__DrawBefore(__composer: __composer, __changed: 0b_00);
            _second.__DrawBefore(__composer: __composer, __changed: 0b_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(890049012, __isRestarted)?.UpdateScope(() => __DrawBefore(__composer, 0));
    }

    public override void __DrawAfter(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(1960357154);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
            _first.__DrawAfter(__composer: __composer, __changed: 0b_00);
            _second.__DrawAfter(__composer: __composer, __changed: 0b_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1960357154, __isRestarted)?.UpdateScope(() => __DrawAfter(__composer, 0));
    }
}