using System;
using StableCollections;

namespace UnityCompose.Samples.Behaviors.NavigationDemo;

internal partial class PausedScreen : ComposeScreen
{
    [Composable]
    public override void Content(IModifier modifier)
    {
        var coordinator = FindCoordinator<ISampleCoordinator>();
        var pausedCoordinator = Remember(() => new PausedCoordinatorImpl());
        var currentTab = Remember(() => MutableStateOf(PausedTab.First));
        Layout(
            tab: currentTab.Value,
            pausedCoordinator: pausedCoordinator,
            onClick: it =>
            {
                currentTab.Value = it;
                pausedCoordinator.ShowTab(it);
            },
            onTabContentClick: () => coordinator.ShowResumedScreen(),
            modifier: modifier
        );
    }

    [Composable]
    private static void Layout(
        PausedTab tab,
        IPausedCoordinator pausedCoordinator,
        Action<PausedTab> onClick,
        Action onTabContentClick,
        IModifier? modifier = null
    )
    {
        var previousTab = Remember(() => IMutableStableProperty.Create(tab));
        Column(
            modifier: modifier.OrEmpty()
                .FillMaxSize(),
            content: () =>
            {
                TabsRow(
                    currentTab: tab,
                    onClick: onClick,
                    modifier: Modifier.Align(Alignment.CenterHorizontally)
                );
                // Spacer(Modifier.FillMaxSize().OnClick(onTabContentClick));
                Navigation(
                    modifier: Modifier.FillMaxSize()
                        .OnClick(onTabContentClick),
                    transition: () => ResolveTransform(previousTab.Value, tab),
                    coordinator: pausedCoordinator
                );
            }
        );
        previousTab.Value = tab;
    }

    private static ContentTransform ResolveTransform(PausedTab previousTab, PausedTab nextTab)
    {
        var multiplier = nextTab > previousTab ? 1 : -1;
        return SlideInHorizontally(it => multiplier * it)
            .TogetherWith(SlideOutHorizontally(it => -multiplier * it));
    }

    [Composable]
    private static void TabsRow(
        PausedTab currentTab,
        Action<PausedTab> onClick,
        IModifier? modifier = null
    )
    {
        Row(
            modifier: modifier.OrEmpty()
                .Align(Alignment.CenterHorizontally),
            content: () =>
            {
                Tab(PausedTab.First, currentTab == PausedTab.First, onClick);
                Tab(PausedTab.Second, currentTab == PausedTab.Second, onClick);
                Tab(PausedTab.Third, currentTab == PausedTab.Third, onClick);
                Tab(PausedTab.Fourth, currentTab == PausedTab.Fourth, onClick);
            }
        );
    }

    [Composable]
    private static void Tab(
        PausedTab tab,
        bool selected,
        Action<PausedTab> onClick
    )
    {
        Text(
            text: tab.ToString(),
            color: Color.white,
            fontSize: 20,
            modifier: Modifier
                .Background(Color.gray)
                .Padding(horizontal: AnimateFloatAsState(selected ? 120 : 40).Value.Px())
                .Padding(vertical: 12.Px())
                .OnClick(() => onClick(tab))
        );
    }
}