#nullable enable
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class PausedTabScreen
{
    public override void __Content(IModifier modifier, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __modifier = (modifier);
        var __isCreated = __composer.StartRestartGroup(352325677);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10 : 0b_01;
        else
            __dirtyRestart |= 0b_01;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            __Box(modifier: modifier.OrEmpty().FillMaxSize().Padding(16.Px()), content: (!__composer.Changed<global::UnityCompose.Samples.Behaviors.NavigationDemo.Screens.PausedTabScreen>(this !) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __Text(text: _tab.ToString(), color: Color.white, fontWeight: FontWeight.Bold, fontSize: 32, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxSize().Background(_background).Border(16.Px()), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
            })), __composer: __composer, __changed: 0b_01_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(352325677, __isRestarted)?.UpdateScope(() => __Content(__modifier, __composer, __dirtyRestart));
    }
}