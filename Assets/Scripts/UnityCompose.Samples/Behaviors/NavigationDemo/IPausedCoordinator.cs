using StableCollections;

namespace UnityCompose.Samples.Behaviors.NavigationDemo;

internal interface IPausedCoordinator : IComposeCoordinator
{
    void ShowTab(PausedTab tab);
}

internal class PausedCoordinatorImpl : BaseComposeCoordinator, IPausedCoordinator
{
    public override IImmutableStableList<ComposeScreen> InitialScreens()
    {
        return IImmutableStableList.Create<ComposeScreen>(new PausedTabScreen(PausedTab.First));
    }

    public void ShowTab(PausedTab tab)
    {
        Router.ReplaceScreen(new PausedTabScreen(tab));
    }
}