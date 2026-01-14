namespace UnityCompose.Samples.Behaviors.NavigationDemo;

internal partial class PausedTabScreen : ComposeScreen
{
    private readonly PausedTab _tab;

    public PausedTabScreen(PausedTab tab)
    {
        _tab = tab;
    }

    public override string ScreenKey => "PausedTab" + _tab;

    [Composable]
    public override void Content(IModifier modifier)
    {
        Text(
            text: _tab.ToString(),
            color: Color.white,
            fontSize: 32,
            textAlign: TextAlign.MiddleCenter,
            modifier: modifier
                .FillMaxSize()
        );
    }
}