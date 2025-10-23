using StableCollections;
using UnityEngine.UIElements;

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
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: Modifier
                    .Name("composition-local-sample")
                    .FlexGrow(1),
                content: () =>
                {
                    var isSwitched = Remember(() => MutableStateOf(false));

                    CompositionLocalProvider(
                        provides: IImmutableStableList.Create(LocalIsSwitched.Provides(isSwitched.Value)),
                        content: SampleReader
                    );

                    Label(
                        text: "Switch",
                        textColor: Color.white,
                        fontSize: 32,
                        style: Modifier
                            .Background(Color.blue)
                            .NewPadding(all: 32)
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
                        style: Modifier
                            .Background(
                                LocalIsSwitched.Current ? Color.green : Color.red, Transition()
                            )
                            .NewPadding(all: 100)
                    );
                });
            });
        }
    }
}