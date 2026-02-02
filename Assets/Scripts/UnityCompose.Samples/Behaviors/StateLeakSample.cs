// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class StateLeakSample : ComposeUI
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
            CompositionLocalProvider(
                LocalTextStyle.Provides(
                    new TextStyle(
                        FontSize: 40,
                        Color: Color.black
                    )
                ),
                content: () =>
                {
                    Box(
                        alignment: Alignment.Center,
                        modifier: Modifier
                            .FillMaxSize(),
                        content: () =>
                        {
                            Column(
                                modifier: Modifier,
                                content: () =>
                                {
                                    var showFirst = Remember(() => MutableStateOf(false));
                                    if (showFirst.Value)
                                    {
                                        var firstCount = Remember(() => MutableStateOf(0));
                                        Text(
                                            text: $"Clicked {firstCount.Value} times",
                                            textAlign: TextAlign.MiddleCenter,
                                            modifier: Modifier
                                                .FillMaxWidth()
                                                .Background(Color.red)
                                                .Padding(all: 20.Px())
                                                .Border(radius: 16.Px())
                                                .OnClick(() => firstCount.Value++)
                                                .Name("first-button")
                                        );
                                    }

                                    var secondCount = Remember(() => MutableStateOf(0));
                                    Text(
                                        text: $"Clicked {secondCount.Value} times",
                                        textAlign: TextAlign.MiddleCenter,
                                        modifier: Modifier
                                            .FillMaxWidth()
                                            .Background(Color.green)
                                            .Padding(all: 20.Px())
                                            .Border(radius: 16.Px())
                                            .Margin(top: 16.Px())
                                            .OnClick(() => secondCount.Value++)
                                            .Name("second-button")
                                    );
                                    Text(
                                        text: "Switch",
                                        textAlign: TextAlign.MiddleCenter,
                                        modifier: Modifier
                                            .FillMaxWidth()
                                            .Background(Color.blue)
                                            .Padding(all: 20.Px())
                                            .Border(radius: 16.Px())
                                            .Margin(top: 16.Px())
                                            .OnClick(() => showFirst.Value = !showFirst.Value)
                                            .Name("switch-button")
                                    );
                                }
                            );
                        }
                    );
                }
            );
        }
    }
}