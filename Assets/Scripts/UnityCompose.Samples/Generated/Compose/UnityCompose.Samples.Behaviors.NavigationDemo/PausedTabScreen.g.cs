#nullable enable
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo;
internal partial class PausedTabScreen
{
    [Composable]
    private void __Content(IModifier modifier)
    {
        var __modifier = (modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(2061450876);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            Box(modifier: modifier.FillMaxSize().Padding(16.Px()), content: !__composer.Changed(this) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                Text(text: _tab.ToString(), color: Color.white, fontSize: 32, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Background(_backgroundColor).Border(32.Px()).FillMaxSize());
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(2061450876, __isRestarted)?.UpdateScope(() => __Content(__modifier));
    }
}