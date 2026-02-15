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
        var __isCreated = __composer.StartRestartGroup(542308588);
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
                var showMenu = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                __DropdownMenu(expanded: showMenu.Value, onDismissRequest: (!__composer.Changed(showMenu) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => showMenu.Value = false)), __composer: __composer, __changed: 0);
                __Spacer(modifier: Modifier.Size(300.Px()).Background(Color.blue).OnClick((!__composer.Changed(showMenu) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => showMenu.Value = true))).Scale(1 - 0.5f * (1 - LocalTransitionProgress.Current)), __composer: __composer, __changed: 0);
            })), __composer: __composer, __changed: 0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(542308588, __isRestarted)?.UpdateScope(() => __Content(__modifier, __composer, __dirtyRestart));
    }

    private static void __DropdownMenu(bool expanded, Action onDismissRequest, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__expanded, __onDismissRequest) = (expanded, onDismissRequest);
        var __isCreated = __composer.StartRestartGroup(228257823);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_11) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(expanded) ? 0b_00_10 : 0b_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_01;
        }

        if ((__changed & 0b_11_00) == 0)
        {
            __dirty |= __composer.Changed(onDismissRequest) ? 0b_10_00 : 0b_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01)
        {
            __composer.StartReplaceGroup(1587820450);
            if (expanded)
            {
                ModalMenu((!__composer.Changed(onDismissRequest) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize().Background(Color.black.With(a: 0.9f)), content: (!__composer.Changed(onDismissRequest) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        __Spacer(Modifier.Size(100.Px()).Background(Color.yellow).OnClick(onDismissRequest), __composer: __composer, __changed: 0);
                    })), __composer: __composer, __changed: 0);
                })));
            }

            __composer.EndReplaceGroup(1587820450);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(228257823, __isRestarted)?.UpdateScope(() => __DropdownMenu(__expanded, __onDismissRequest, __composer, __dirtyRestart));
    }
}