#nullable enable
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class PausedTabScreen
{
    private void __Content(IModifier modifier, global::UnityCompose.Composer __composer = null !)
    {
        var __modifier = (modifier);
        __composer.StartRestartGroup(-352325677);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            Box(modifier: modifier.OrEmpty().FillMaxSize().Padding(16.Px()), content: (!__composer.Changed(this) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                Text(text: _tab.ToString(), color: Color.white, fontWeight: FontWeight.Bold, fontSize: 32, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxSize().Background(_background).Border(16.Px()));
            })));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-352325677, __isRestarted)?.UpdateScope(() => __Content(__modifier));
    }

    private void __Content(IModifier modifier)
    {
        __Content(modifier, CurrentComposer);
    }
}