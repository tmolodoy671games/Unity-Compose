using System;
using SharpExtensions;
using StableCollections;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

internal partial class PausedScreen : ComposeScreen
{
    [Composable]
    public override void Content(IModifier modifier)
    {
        var coordinator = FindCoordinator<ISampleCoordinator>();
        var pausedCoordinator = Remember(() => new PausedCoordinatorImpl());
        var currentTab = Remember(() => MutableStateOf(PausedTab.Inventory));
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
                Spacer(Modifier.FillMaxSize().OnClick(onTabContentClick));
                // Navigation(
                //     modifier: Modifier.FillMaxSize()
                //         .OnClick(onTabContentClick),
                //     transition: Remember((previousTab.Value, tab), () => ResolveTransform(previousTab.Value, tab)),
                //     coordinator: pausedCoordinator
                // );
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
                .Align(Alignment.CenterHorizontally)
                .Margin(top: 16.Px()),
            content: () =>
            {
                Tab(PausedTab.Inventory, currentTab == PausedTab.Inventory, onClick);
                // Tab(PausedTab.Map, currentTab == PausedTab.Map, onClick);
                // Tab(PausedTab.Journal, currentTab == PausedTab.Journal, onClick);
                // Tab(PausedTab.System, currentTab == PausedTab.System, onClick);
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
        DsClickIndication(
            modifier: Modifier
                .Background(Color.gray)
                .Padding(horizontal: AnimateFloatAsState(selected ? 120 : 40).Value.Px())
                .Padding(vertical: 12.Px())
                .Border(8.Px())
                .Margin(horizontal: 4.Px())
                .OnClick(() => onClick(tab)),
            content: () =>
            {
                Text(
                    text: tab.ToString(),
                    color: Color.white,
                    fontSize: 20,
                    fontWeight: FontWeight.Bold
                    // modifier: Modifier
                    //     .Scale(1 - AnimateFloatAsState(it.IsPressed.ToInt()).Value * 0.25f)
                );
            }
        );
    }
}