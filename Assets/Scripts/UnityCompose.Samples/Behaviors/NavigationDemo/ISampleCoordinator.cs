using StableCollections;

namespace UnityCompose.Samples.Behaviors.NavigationDemo;

internal interface ISampleCoordinator
{
    void ShowPausedScreen();
    void ShowResumedScreen();
}

internal class SampleCoordinatorImpl : BaseComposeCoordinator, ISampleCoordinator
{
    public override IImmutableStableList<ComposeScreen> InitialScreens() =>
        IImmutableStableList.Create<ComposeScreen>(new Screens.ResumedScreen());

    public void ShowPausedScreen()
    {
        Router.ReplaceScreen(new Screens.PausedScreen());
    }

    public void ShowResumedScreen()
    {
        Router.Exit();
    }
}