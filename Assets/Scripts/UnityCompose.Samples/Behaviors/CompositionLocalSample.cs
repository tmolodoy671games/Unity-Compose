using StableCollections;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample : ComposeUI
    {
        private static readonly ICompositionLocal<bool> LocalIsSwitched = CompositionLocalOf(() => false);

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

                    CompositionLocalProvider(
                        provides: IImmutableStableList.Create(LocalIsSwitched.Provides(isSwitched.Value)),
                        content: SampleReader
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
            Box(() =>
            {
                Box(() =>
                {
                    Spacer(
                        modifier: Modifier
                            .Background(
                                LocalIsSwitched.Current ? Color.green : Color.red, Transition()
                            )
                            .Padding(all: 100)
                    );
                });
            });
        }
    }
}