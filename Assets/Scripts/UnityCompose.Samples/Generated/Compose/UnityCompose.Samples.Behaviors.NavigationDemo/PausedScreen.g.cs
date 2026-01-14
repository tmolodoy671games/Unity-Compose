#nullable enable
using System;
using StableCollections;
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
        __composer.StartRestartGroup(1155514478);
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

        __composer.EndRestartGroup(1155514478, __isRestarted)?.UpdateScope(() => __Content(__modifier));
    }

    [Composable]
    private static void __Layout(PausedTab tab, IPausedCoordinator pausedCoordinator, Action<PausedTab> onClick, Action onTabContentClick, IModifier? modifier = null)
    {
        var(__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier) = (tab, pausedCoordinator, onClick, onTabContentClick, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1865055843);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier)))
        {
            var previousTab = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityCompose.Samples.Behaviors.NavigationDemo.PausedTab>>(IMutableStableProperty.Create(tab));
            Column(modifier: modifier.OrEmpty().FillMaxSize(), content: !__composer.ChangedAsStruct((tab, pausedCoordinator, onClick, onTabContentClick, previousTab)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
            {
                TabsRow(currentTab: tab, onClick: onClick, modifier: Modifier.Align(Alignment.CenterHorizontally));
                // Spacer(Modifier.FillMaxSize().OnClick(onTabContentClick));
                Navigation(modifier: Modifier.FillMaxSize().OnClick(onTabContentClick), transition: !__composer.ChangedAsStruct((tab, previousTab)) ? __composer.RememberedValue<System.Func<UnityCompose.ContentTransform>?>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.ContentTransform>?>(() => ResolveTransform(previousTab.Value, tab)), coordinator: pausedCoordinator);
            }));
            previousTab.Value = tab;
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1865055843, __isRestarted)?.UpdateScope(() => __Layout(__tab, __pausedCoordinator, __onClick, __onTabContentClick, __modifier));
    }

    [Composable]
    private static void __TabsRow(PausedTab currentTab, Action<PausedTab> onClick, IModifier? modifier = null)
    {
        var(__currentTab, __onClick, __modifier) = (currentTab, onClick, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-180068670);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__currentTab, __onClick, __modifier)))
        {
            Row(modifier: modifier.OrEmpty().Align(Alignment.CenterHorizontally), content: !__composer.ChangedAsStruct((currentTab, onClick)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
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

        __composer.EndRestartGroup(-180068670, __isRestarted)?.UpdateScope(() => __TabsRow(__currentTab, __onClick, __modifier));
    }

    [Composable]
    private static void __Tab(PausedTab tab, bool selected, Action<PausedTab> onClick)
    {
        var(__tab, __selected, __onClick) = (tab, selected, onClick);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(963642206);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__tab, __selected, __onClick)))
        {
            Text(text: tab.ToString(), color: Color.white, fontSize: 20, modifier: Modifier.Background(Color.gray).Padding(horizontal: AnimateFloatAsState(selected ? 120 : 40).Value.Px()).Padding(vertical: 12.Px()).OnClick(!__composer.ChangedAsStruct((tab, onClick)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => onClick(tab))));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(963642206, __isRestarted)?.UpdateScope(() => __Tab(__tab, __selected, __onClick));
    }
}