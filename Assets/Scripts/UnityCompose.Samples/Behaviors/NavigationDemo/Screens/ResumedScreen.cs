namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

internal partial class ResumedScreen : ComposeScreen
{
    [Composable]
    public override void Content(IModifier modifier)
    {
        var coordinator = FindCoordinator<ISampleCoordinator>();
        Box(
            alignment: Alignment.Center,
            modifier: modifier
                .FillMaxSize()
                .Background(Color.green)
                .OnClick(() => coordinator.ShowPausedScreen()),
            content: () =>
            {
                Spacer(
                    modifier: Modifier
                        .Size(100.Px())
                        .Background(Color.blue)
                        .Scale(1 + 2 * LocalTransitionProgress.Current)
                );
            }
        );
    }
}