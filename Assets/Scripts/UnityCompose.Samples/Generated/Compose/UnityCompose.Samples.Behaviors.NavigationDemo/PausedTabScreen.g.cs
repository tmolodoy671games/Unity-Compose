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
        __composer.StartRestartGroup(-1801877681);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            Text(text: _tab.ToString(), color: Color.white, fontSize: 32, textAlign: TextAlign.MiddleCenter, modifier: modifier.FillMaxSize());
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1801877681, __isRestarted)?.UpdateScope(() => __Content(__modifier));
    }
}