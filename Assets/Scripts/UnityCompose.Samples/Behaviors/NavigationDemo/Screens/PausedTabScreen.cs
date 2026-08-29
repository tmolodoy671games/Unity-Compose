namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

internal partial class PausedTabScreen : ComposeScreen
{
    private readonly PausedTab _tab;
    private readonly Color _background;

    public PausedTabScreen(PausedTab tab, Color background)
    {
        _tab = tab;
        _background = background;
    }

    public override string ScreenKey => "PausedTab" + _tab;

    [Composable]
    public override void Content(IModifier modifier)
    {
        Box(
            modifier: modifier.OrEmpty()
                .FillMaxSize()
                .Padding(16.Dp()),
            content: () =>
            {
                Text(
                    text: _tab.ToString(),
                    color: Color.white,
                    fontWeight: FontWeight.Bold,
                    fontSize: 32.Sp(),
                    textAlign: TextAlign.MiddleCenter,
                    modifier: Modifier
                        .FillMaxSize()
                        .Background(_background)
                        .Border(16.Dp())
                );
            }
        );
    }
}