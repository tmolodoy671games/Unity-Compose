using Compose.Net;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Stability
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
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .TestTag("composition-local-sample")
                    .FillMaxSize(),
                content: () =>
                {
                    var isSwitched = Remember(() => MutableStateOf(false));

                    CompositionLocalProvider(
                        LocalIsSwitched.Provides(isSwitched.Value),
                        content: SampleReader
                    );

                    Text(
                        text: "Switch",
                        color: Color.white.ToSystemColor(),
                        fontSize: 32.Sp(),
                        modifier: Modifier
                            .Background(Color.blue.ToSystemColor())
                            .Padding(all: 32.Dp())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            .Margin(top: 80.Dp())
                    );
                }
            );
        }

        [Composable]
        private static void OtherSampleReader(bool firstValue, bool secondValue)
        {
            Debug.Log($"{Time.frameCount}: {firstValue} vs {secondValue}");
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
                                AnimateColorAsState(
                                    LocalIsSwitched.Current ? Color.green.ToSystemColor() : Color.red.ToSystemColor()
                                ).Value
                            )
                            .Padding(all: 100.Dp())
                    );
                });
            });
        }
    }
}