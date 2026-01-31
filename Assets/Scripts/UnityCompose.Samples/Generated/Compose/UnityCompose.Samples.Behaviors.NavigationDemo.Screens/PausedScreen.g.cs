#nullable enable
using System;
using SharpExtensions;
using StableCollections;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class PausedScreen
{
    [Composable]
    private void __Content(IModifier modifier)
    {
        var __modifier = (modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1131697469);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            var coordinator = FindCoordinator<ISampleCoordinator>();
            var pausedCoordinator = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Samples.Behaviors.NavigationDemo.PausedCoordinatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.Samples.Behaviors.NavigationDemo.PausedCoordinatorImpl>(new PausedCoordinatorImpl());
            var currentTab = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(MutableStateOf(PausedTab.Inventory));
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

        __composer.EndRestartGroup(1131697469, __isRestarted)?.UpdateScope(() => __Content(__modifier));
    }

    [Composable]
    private static void __Layout(PausedTab tab, IPausedCoordinator pausedCoordinator, Action<PausedTab> onClick, Action onTabContentClick, IModifier? modifier = null)
    {
        var(__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier) = (tab, pausedCoordinator, onClick, onTabContentClick, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-177925555);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier)))
        {
            var previousTab = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(IMutableStableProperty.Create(tab));
            Column(modifier: modifier.OrEmpty().FillMaxSize(), content: !__composer.ChangedAsStruct((tab, pausedCoordinator, onClick, onTabContentClick, previousTab)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                TabsRow(currentTab: tab, onClick: onClick, modifier: Modifier.Align(Alignment.CenterHorizontally));
                // Spacer(Modifier.FillMaxSize().OnClick(onTabContentClick));
                Navigation(modifier: Modifier.FillMaxSize().OnClick(onTabContentClick), transition: !__composer.ChangedAsStruct((previousTab.Value, tab)) ? __composer.RememberedValueAsStruct<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValueAsStruct<UnityCompose.ContentTransform>(ResolveTransform(previousTab.Value, tab)), coordinator: pausedCoordinator);
            }));
            previousTab.Value = tab;
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-177925555, __isRestarted)?.UpdateScope(() => __Layout(__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier));
    }

    [Composable]
    private static void __TabsRow(PausedTab currentTab, Action<PausedTab> onClick, IModifier? modifier = null)
    {
        var(__currentTab, __onClick, __modifier) = (currentTab, onClick, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-447550961);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__currentTab, __onClick, __modifier)))
        {
            Row(modifier: modifier.OrEmpty().Align(Alignment.CenterHorizontally).Margin(top: 16.Px()), content: !__composer.ChangedAsStruct((currentTab, onClick)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                Tab(PausedTab.Inventory, currentTab == PausedTab.Inventory, onClick);
                Tab(PausedTab.Map, currentTab == PausedTab.Map, onClick);
                Tab(PausedTab.Journal, currentTab == PausedTab.Journal, onClick);
                Tab(PausedTab.System, currentTab == PausedTab.System, onClick);
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-447550961, __isRestarted)?.UpdateScope(() => __TabsRow(__currentTab, __onClick, __modifier));
    }

    [Composable]
    private static void __Tab(PausedTab tab, bool selected, Action<PausedTab> onClick)
    {
        var(__tab, __selected, __onClick) = (tab, selected, onClick);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-2090021764);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__tab, __selected, __onClick)))
        {
            DsClickIndication(modifier: Modifier.Background(Color.gray).Padding(horizontal: AnimateFloatAsState(selected ? 120 : 40).Value.Px()).Padding(vertical: 12.Px()).Border(8.Px()).Margin(horizontal: 4.Px()).OnClick(!__composer.ChangedAsStruct((tab, onClick)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => onClick(tab))), content: !__composer.ChangedAsStruct(tab) ? __composer.RememberedValue<UnityCompose.ComposableContent<UI.DesignSystem.Compose.DsClickIndicationScope>>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent<UI.DesignSystem.Compose.DsClickIndicationScope>>(it =>
            {
                Text(text: tab.ToString(), color: Color.white, fontSize: 20, fontWeight: FontWeight.Bold, modifier: Modifier.Scale(1.5f - AnimateFloatAsState(it.IsPressed.ToInt()).Value * 0.25f));
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-2090021764, __isRestarted)?.UpdateScope(() => __Tab(__tab, __selected, __onClick));
    }
}