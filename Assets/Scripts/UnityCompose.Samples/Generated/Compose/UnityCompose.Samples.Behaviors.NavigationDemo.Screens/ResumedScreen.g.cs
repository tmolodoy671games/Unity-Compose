#nullable enable
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class ResumedScreen
{
    private void __Content(IModifier modifier, global::UnityCompose.Composer __composer = null !)
    {
        var __modifier = (modifier);
        __composer.StartRestartGroup(733657196);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            var coordinator = FindCoordinator<ISampleCoordinator>();
            Box(alignment: Alignment.Center, modifier: modifier.FillMaxSize().Background(Color.green).OnClick((!__composer.Changed(coordinator) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => coordinator.ShowPausedScreen()))), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                Spacer(modifier: Modifier.Size(100.Px()).Background(Color.blue).Scale(1 + 2 * LocalTransitionProgress.Current));
            })));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(733657196, __isRestarted)?.UpdateScope(() => __Content(__modifier));
    }

    private void __Content(IModifier modifier)
    {
        __Content(modifier, CurrentComposer);
    }
}