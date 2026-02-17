using System;
using StableCollections;
using UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

namespace UnityCompose.Samples.Behaviors.NavigationDemo;

internal interface IPausedCoordinator : IComposeCoordinator
{
    void ShowTab(PausedTab tab);
}

internal class PausedCoordinatorImpl : BaseComposeCoordinator, IPausedCoordinator
{
    public override IImmutableStableList<ComposeScreen> InitialScreens()
    {
        return IImmutableStableList.Create<ComposeScreen>(
            new InventoryTabScreen()
        );
    }

    public void ShowTab(PausedTab tab)
    {
        ComposeScreen screen = tab switch
        {
            PausedTab.Inventory => new InventoryTabScreen(),
            _ => new PausedTabScreen(tab, ResolveBackgroundColor(tab))
        };
        Debug.Log("ReplaceScreen()");
        Router.ReplaceScreen(screen);
    }

    private static Color ResolveBackgroundColor(PausedTab tab)
    {
        return tab switch
        {
            PausedTab.Inventory => Color.red,
            PausedTab.Map => Color.green,
            PausedTab.Journal => Color.blue,
            PausedTab.System => Color.yellow,
            _ => throw new ArgumentOutOfRangeException(nameof(tab), tab, null)
        };
    }
}