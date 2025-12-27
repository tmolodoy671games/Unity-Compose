namespace UnityCompose.Samples.Behaviors
{
    internal partial class ModifiersSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            Layout();
        }

        [Composable]
        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            Column(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .Name("composition-local-sample")
                    .FillMaxSize(),
                content: () =>
                {
                    var isSwitched = Remember(() => MutableStateOf(false));

                    WithModifiers(
                        after: Modifier.Background(
                            AnimateColorAsState(isSwitched.Value ? Color.green : Color.red).Value
                        ),
                        content: () => SampleReader()
                    );

                    Text(
                        text: "Switch",
                        color: Color.white,
                        fontSize: 32,
                        modifier: Modifier
                            .Background(Color.blue)
                            .Padding(all: 32)
                            .Border(radius: 16)
                            .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            .Margin(top: 80)
                    );
                }
            );
        }

        [Composable]
        private static void SampleReader()
        {
            Spacer(
                modifier: Modifier
                    .Padding(all: 100)
            );
        }
    }
}