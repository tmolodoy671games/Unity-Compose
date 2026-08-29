#nullable enable
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class InventoryTabScreen
{
    public override void __Content(IModifier modifier, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __modifier = (modifier);
        var __isCreated = __composer.StartRestartGroup(1906860964);
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10 : 0b_01;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            __Box(alignment: Alignment.Center, modifier: modifier.OrEmpty().FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __Column(modifier: Modifier.Background(Color.black).Padding(8.Dp()).Border(16.Dp()), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Row((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __InventoryItem(__composer: __composer, __changed: 0b_00);
                        __InventoryItem(__composer: __composer, __changed: 0b_00);
                        __InventoryItem(__composer: __composer, __changed: 0b_00);
                        __InventoryItem(__composer: __composer, __changed: 0b_00);
                    })), __composer: __composer, __changed: 0b_01_01_01_00);
                    __Row((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __InventoryItem(__composer: __composer, __changed: 0b_00);
                        __InventoryItem(__composer: __composer, __changed: 0b_00);
                        __InventoryItem(__composer: __composer, __changed: 0b_00);
                        __InventoryItem(__composer: __composer, __changed: 0b_00);
                    })), __composer: __composer, __changed: 0b_01_01_01_00);
                })), __composer: __composer, __changed: 0b_01_01_00_00);
            })), __composer: __composer, __changed: 0b_00_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(1906860964, __isRestarted)?.UpdateScope(() => __Content(__modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    private static void __InventoryItem(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __isCreated = __composer.StartRestartGroup(794104995);
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __changed != 0b_00)
        {
            __Spacer(Modifier.Size(100.Dp()).Border(16.Dp()).Background(Color.grey).Margin(2.Dp()), __composer: __composer, __changed: 0b_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(794104995, __isRestarted)?.UpdateScope(() => __InventoryItem(__composer, 0));
    }
}