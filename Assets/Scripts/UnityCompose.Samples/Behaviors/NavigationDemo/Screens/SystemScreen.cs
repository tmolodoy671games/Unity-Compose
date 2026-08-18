namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

internal partial class SystemScreen : ComposeScreen
{
    [Composable]
    public override void Content(IModifier modifier)
    {
        var showModals = Remember(() => MutableStateOf(false));
        if (showModals.Value)
        {
            ModalMenu(() =>
            {
                Spacer(
                    Modifier
                        .Size(100.Px())
                        .Background(Color.red)
                        .OnClick(() => showModals.Value = false)
                );
            });
        }

        Box(
            alignment: Alignment.Center,
            modifier: modifier.FillMaxSize(),
            content: () =>
            {
                Column(() =>
                {
                    DsClickIndication(it =>
                    {
                        Text(
                            "Bla",
                            modifier: Modifier
                                .OnClick(() => showModals.Value = true)
                        );
                    });
                    DsClickIndication(it =>
                    {
                        Text(
                            "Bla",
                            modifier: Modifier
                        );
                    });
                });
            }
        );
    }
}