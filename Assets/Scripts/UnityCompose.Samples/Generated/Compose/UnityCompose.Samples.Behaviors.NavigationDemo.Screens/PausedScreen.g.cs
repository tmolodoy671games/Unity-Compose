#nullable enable
using System;
using SharpExtensions;
using StableCollections;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class PausedScreen
{
    public override void __Content(IModifier modifier, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __modifier = (modifier);
        var __isCreated = __composer.StartRestartGroup(1131697469);
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
            var pausedCoordinator = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedCoordinatorImpl>() : __composer.UpdateRememberedValue<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedCoordinatorImpl>(new PausedCoordinatorImpl()));
            var currentTab = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(MutableStateOf(PausedTab.Inventory)));
            __Layout(tab: currentTab.Value, pausedCoordinator: pausedCoordinator, onClick: (!__composer.BuildChanged().Changed<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedCoordinatorImpl>(pausedCoordinator!).Changed<global::UnityCompose.IMutableState<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(currentTab!).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(it =>
            {
                currentTab.Value = it;
                pausedCoordinator.ShowTab(it);
            })), onTabContentClick: (!__composer.Changed<global::UnityCompose.Samples.Behaviors.NavigationDemo.ISampleCoordinator>(coordinator!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => coordinator.ShowResumedScreen())), modifier: modifier, __composer: __composer, __changed: ((__dirty & 0b_00_00_00_00_11) << 8));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(1131697469, __isRestarted)?.UpdateScope(() => __Content(__modifier, __composer, __dirtyRestart));
    }

    private static void __Layout(IPausedCoordinator pausedCoordinator, PausedTab tab, Action<PausedTab> onClick, Action onTabContentClick, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__pausedCoordinator, __tab, __onClick, __onTabContentClick, __modifier) = (pausedCoordinator, tab, onClick, onTabContentClick, modifier);
        var __isCreated = __composer.StartRestartGroup(177925555);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(pausedCoordinator) ? 0b_00_00_00_00_10 : 0b_00_00_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_01;
        }

        if ((__changed & 0b_00_00_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(tab) ? 0b_00_00_00_10_00 : 0b_00_00_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_01_00;
        }

        if ((__changed & 0b_00_00_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(onClick) ? 0b_00_00_10_00_00 : 0b_00_00_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01_00_00;
        }

        if ((__changed & 0b_00_11_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(onTabContentClick) ? 0b_00_10_00_00_00 : 0b_00_01_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00_00_00;
        }

        if ((__changed & 0b_11_00_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00 : 0b_01_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01)
        {
            var previousTab = (!__composer.Changed() ? __composer.RememberedValue<global::StableCollections.IMutableStableProperty<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>() : __composer.UpdateRememberedValue<global::StableCollections.IMutableStableProperty<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(IMutableStableProperty.Create(tab)));
            __Column(modifier: modifier.OrEmpty().FillMaxSize(), content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_11) == 0b_00_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_00_11_00) == 0b_00_00_00_10_00).ChangedAsFlag((__dirty & 0b_00_00_11_00_00) == 0b_00_00_10_00_00).ChangedAsFlag((__dirty & 0b_00_11_00_00_00) == 0b_00_10_00_00_00).Changed<global::StableCollections.IMutableStableProperty<global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(previousTab!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __TabsRow(currentTab: tab, onClick: onClick, modifier: Modifier.Align(Alignment.CenterHorizontally).Offset(y: -100 * (1 - 1).Px()), __composer: __composer, __changed: ((__dirty & 0b_00_11_00) >> 2) | ((__dirty & 0b_11_00_00) >> 2));
                __Navigation(modifier: Modifier.FillMaxSize().OnClick(onTabContentClick), transition: (!__composer.Changed<(global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab Value, global::UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab tab)>((previousTab.Value, tab)!) ? __composer.RememberedValue<global::UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<global::UnityCompose.ContentTransform>(ResolveTransform(previousTab.Value, tab))), coordinator: pausedCoordinator, __composer: __composer, __changed: (__dirty & 0b_00_00_11));
            })), __composer: __composer, __changed: 0b_01_01_00_00);
            previousTab.Value = tab;
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01;
        __composer.EndRestartGroup(177925555, __isRestarted)?.UpdateScope(() => __Layout(__pausedCoordinator, __tab, __onClick, __onTabContentClick, __modifier, __composer, __dirtyRestart));
    }

    private static void __TabsRow(PausedTab currentTab, Action<PausedTab> onClick, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__currentTab, __onClick, __modifier) = (currentTab, onClick, modifier);
        var __isCreated = __composer.StartRestartGroup(447550961);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(currentTab) ? 0b_00_00_10 : 0b_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01;
        }

        if ((__changed & 0b_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(onClick) ? 0b_00_10_00 : 0b_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00;
        }

        if ((__changed & 0b_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00 : 0b_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            __Row(modifier: modifier.OrEmpty().Align(Alignment.CenterHorizontally).Margin(top: 16.Px()), content: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).ChangedAsFlag((__dirty & 0b_00_11_00) == 0b_00_10_00).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __Tab(PausedTab.Inventory, currentTab == PausedTab.Inventory, onClick, __composer: __composer, __changed: 0b_00_00_01 | ((__dirty & 0b_00_11_00) << 2));
                __Tab(PausedTab.Map, currentTab == PausedTab.Map, onClick, __composer: __composer, __changed: 0b_00_00_01 | ((__dirty & 0b_00_11_00) << 2));
                __Tab(PausedTab.Journal, currentTab == PausedTab.Journal, onClick, __composer: __composer, __changed: 0b_00_00_01 | ((__dirty & 0b_00_11_00) << 2));
                __Tab(PausedTab.System, currentTab == PausedTab.System, onClick, __composer: __composer, __changed: 0b_00_00_01 | ((__dirty & 0b_00_11_00) << 2));
            })), __composer: __composer, __changed: 0b_01_01_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(447550961, __isRestarted)?.UpdateScope(() => __TabsRow(__currentTab, __onClick, __modifier, __composer, __dirtyRestart));
    }

    private static void __Tab(PausedTab tab, bool selected, Action<PausedTab> onClick, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__tab, __selected, __onClick) = (tab, selected, onClick);
        var __isCreated = __composer.StartRestartGroup(2090021764);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(tab) ? 0b_00_00_10 : 0b_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01;
        }

        if ((__changed & 0b_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(selected) ? 0b_00_10_00 : 0b_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00;
        }

        if ((__changed & 0b_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(onClick) ? 0b_10_00_00 : 0b_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            __DsClickIndication(modifier: Modifier.Background(Color.gray).Padding(horizontal: __AnimateFloatAsState(selected ? 120 : 40, __composer: __composer, __changed: 0b_01_01_00).Value.Px()).Padding(vertical: 12.Px()).Border(8.Px()).Margin(horizontal: 4.Px()).OnClick((!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).ChangedAsFlag((__dirty & 0b_11_00_00) == 0b_10_00_00).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => onClick(tab)))), content: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent<global::UI.DesignSystem.Compose.DsClickIndicationScope>>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent<global::UI.DesignSystem.Compose.DsClickIndicationScope>>(it =>
            {
                __Text(text: tab.ToString(), color: Color.white, fontSize: 20, fontWeight: FontWeight.Bold, modifier: Modifier.Scale(1.5f - __AnimateFloatAsState(it.IsPressed.ToInt(), __composer: __composer, __changed: 0b_01_01_00).Value * 0.25f), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
            })), __composer: __composer, __changed: 0b_01_01_01_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(2090021764, __isRestarted)?.UpdateScope(() => __Tab(__tab, __selected, __onClick, __composer, __dirtyRestart));
    }
}