#nullable enable
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class ResumedScreen
{
    public override void __Content(IModifier modifier, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __modifier = (modifier);
        var __isCreated = __composer.StartRestartGroup(733657196);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_11) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10 : 0b_01;
        }
        else
        {
            __dirtyRestart |= 0b_01;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            var coordinator = FindCoordinator<ISampleCoordinator>();
            __Box(alignment: Alignment.Center, modifier: modifier.FillMaxSize().Background(Color.green).OnClick((!__composer.Changed(coordinator) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => coordinator.ShowPausedScreen()))), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                __Spacer(modifier: Modifier.Size(100.Px()).Background(Color.blue), __composer: __composer, __changed: 0 // .Scale(1 + 2 * LocalTransitionProgress.Current)
                );
            })), __composer: __composer, __changed: 0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(733657196, __isRestarted)?.UpdateScope(() => __Content(__modifier, __composer, __dirtyRestart));
    }
}