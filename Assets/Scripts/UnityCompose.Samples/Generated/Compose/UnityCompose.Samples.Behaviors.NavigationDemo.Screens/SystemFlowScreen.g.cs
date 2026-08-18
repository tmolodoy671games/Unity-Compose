#nullable enable
using StableCollections;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class SystemFlowScreen
{
    public override void __Content(IModifier modifier, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __modifier = (modifier);
        var __isCreated = __composer.StartRestartGroup(1203927761);
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10 : 0b_01;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            var coordinator = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.Screens.SystemFlowCoordinatorImpl>() : __composer.UpdateRememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.Screens.SystemFlowCoordinatorImpl>(new SystemFlowCoordinatorImpl()));
            __Navigation(coordinator: coordinator, modifier: modifier.FillMaxSize(), __composer: __composer, __changed: 0b_01_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(1203927761, __isRestarted)?.UpdateScope(() => __Content(__modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}