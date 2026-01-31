#nullable enable
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class InventoryTabScreen
{
    [Composable]
    private void __Content(IModifier modifier)
    {
        var __modifier = (modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1906860964);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            Spacer(modifier.FillMaxSize());
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1906860964, __isRestarted)?.UpdateScope(() => __Content(__modifier));
    }

    [Composable]
    private static void __InventoryItem()
    {
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1874757568);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute())
        {
            Spacer(Modifier.Size(100.Px()).Border(16.Px()).Background(Color.grey).Margin(2.Px()));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1874757568, __isRestarted)?.UpdateScope(() => __InventoryItem());
    }
}