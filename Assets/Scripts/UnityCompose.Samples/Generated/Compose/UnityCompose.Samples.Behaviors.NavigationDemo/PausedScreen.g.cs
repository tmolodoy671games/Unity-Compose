#nullable enable
using System;
using System.Collections;
using SharpExtensions;
using StableCollections;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo;
internal partial class PausedScreen
{
    [Composable]
    private void __Content(IModifier modifier)
    {
        var __modifier = (modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1606057606);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            var coordinator = FindCoordinator<ISampleCoordinator>();
            var pausedCoordinator = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Samples.Behaviors.NavigationDemo.PausedCoordinatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.Samples.Behaviors.NavigationDemo.PausedCoordinatorImpl>(new PausedCoordinatorImpl());
            var currentTab = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(MutableStateOf(PausedTab.First));
            Layout(tab: currentTab.Value, pausedCoordinator: pausedCoordinator, onClick: !__composer.ChangedAsStruct((pausedCoordinator, currentTab)) ? __composer.RememberedValue<System.Action<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(it =>
            {
                currentTab.Value = it;
                pausedCoordinator.ShowTab(it);
            }), onTabContentClick: !__composer.Changed(coordinator) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => coordinator.ShowResumedScreen()), modifier: modifier);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1606057606, __isRestarted)?.UpdateScope(() => __Content(__modifier));
    }

    [Composable]
    private static void __Layout(PausedTab tab, IPausedCoordinator pausedCoordinator, Action<PausedTab> onClick, Action onTabContentClick, IModifier modifier)
    {
        var(__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier) = (tab, pausedCoordinator, onClick, onTabContentClick, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(471408317);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier)))
        {
            ReusableComposeView<PausedScreenView>(modifier.FillMaxSize().Background(Color.red).OnClick(onTabContentClick));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(471408317, __isRestarted)?.UpdateScope(() => __Layout(__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier));
    }

    [Composable]
    private static void __TabsRow(PausedTab currentTab, Action<PausedTab> onClick, IModifier? modifier = null)
    {
        var(__currentTab, __onClick, __modifier) = (currentTab, onClick, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(869151335);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__currentTab, __onClick, __modifier)))
        {
            Row(modifier: modifier.OrEmpty().Align(Alignment.CenterHorizontally).Margin(top: 32.Px()), content: !__composer.ChangedAsStruct((currentTab, onClick)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                Tab(PausedTab.First, currentTab == PausedTab.First, onClick);
                Tab(PausedTab.Second, currentTab == PausedTab.Second, onClick);
                Tab(PausedTab.Third, currentTab == PausedTab.Third, onClick);
                Tab(PausedTab.Fourth, currentTab == PausedTab.Fourth, onClick);
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(869151335, __isRestarted)?.UpdateScope(() => __TabsRow(__currentTab, __onClick, __modifier));
    }

    [Composable]
    private static void __Tab(PausedTab tab, bool selected, Action<PausedTab> onClick)
    {
        var(__tab, __selected, __onClick) = (tab, selected, onClick);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-198400003);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__tab, __selected, __onClick)))
        {
            ClickIndication(modifier: Modifier.Background(Color.gray).Padding(horizontal: AnimateFloatAsState(selected ? 120 : 40, animationSpec: Tween(1)).Value.Px()).Padding(vertical: 12.Px()).Margin(horizontal: 2.Px()).Border(16.Px()).OnClick(!__composer.ChangedAsStruct((tab, onClick)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => onClick(tab))), content: !__composer.ChangedAsStruct(tab) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                Text(text: tab.ToString(), color: Color.white, fontSize: 20);
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-198400003, __isRestarted)?.UpdateScope(() => __Tab(__tab, __selected, __onClick));
    }
}