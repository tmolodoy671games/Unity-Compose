// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedSizeSample : ComposeUI
    {
        protected override void Content()
        {
            Layout();
        }

        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            Box(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Column(
                        horizontalAlignment: Alignment.Horizontal.Center,
                        modifier: Modifier
                            .Name("animated-size-sample"),
                        content: () =>
                        {
                            var isSwitched = Remember(() => MutableStateOf(false));
                            var text = isSwitched.Value
                                ? "Short"
                                : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";

                            AnimatedSize(
                                modifier: Modifier
                                    .Name("animated-size")
                                    .Background(isSwitched.Value ? Color.green : Color.red, Transition(5))
                                    .NewPadding(all: 16),
                                duration: 5,
                                content: () =>
                                {
                                    Text(
                                        text: text,
                                        color: Color.white,
                                        fontSize: 64,
                                        textAlign: TextAlign.MiddleCenter,
                                        modifier: Modifier
                                            .Name("animated-label-child")
                                    );
                                }
                            );

                            Text(
                                text: "Switch",
                                color: Color.white,
                                fontSize: 64,
                                modifier: Modifier
                                    .Name("switch-button")
                                    .NewPadding(all: 32)
                                    .Background(Color.blue)
                                    .NewMargin(top: 16)
                                    .Border(radius: 16)
                                    .OnClick(() => isSwitched.Value = !isSwitched.Value)
                            );
                        }
                    );
                }
            );
        }
    }
}