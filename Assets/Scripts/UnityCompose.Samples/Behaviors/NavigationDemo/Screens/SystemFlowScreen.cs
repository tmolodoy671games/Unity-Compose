using StableCollections;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

internal partial class SystemFlowScreen : ComposeScreen
{
    [Composable]
    public override void Content(IModifier modifier)
    {
        var coordinator = Remember(() => new SystemFlowCoordinatorImpl());
        Navigation(
            coordinator: coordinator,
            modifier: modifier
                .FillMaxSize()
        );
    }
}

internal class SystemFlowCoordinatorImpl : BaseComposeCoordinator
{
    public override IImmutableStableList<ComposeScreen> InitialScreens()
    {
        return IImmutableStableList.Create<ComposeScreen>(new SystemScreen());
    }
}