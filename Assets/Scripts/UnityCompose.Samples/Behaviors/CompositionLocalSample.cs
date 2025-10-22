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
                style: ComposeStyle.Empty
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
                        style: ComposeStyle.Empty
                            .BackgroundColor(Color.blue)
                            .Padding(32)
                            .BorderRadius(16)
                            .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            .MarginTop(80)
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
                        style: ComposeStyle.Empty
                            .BackgroundColor(
                                LocalIsSwitched.Current ? Color.green : Color.red, Transition()
                            )
                            .Padding(100)
                    );
                });
            });
        }
    }
}