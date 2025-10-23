using UnityEngine.UIElements;

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
            Box(
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Column(
                        alignHorizontally: Align.Stretch,
                        style: Modifier,
                        content: () =>
                        {
                            var showFirst = Remember(() => MutableStateOf(false));
                            if (showFirst.Value)
                            {
                                var firstCount = Remember(() => MutableStateOf(0));
                                Text(
                                    text: $"Clicked {firstCount.Value} times",
                                    fontSize: 20,
                                    align: TextAnchor.MiddleCenter,
                                    style: Modifier
                                        .Background(Color.red)
                                        .NewPadding(all: 20)
                                        .Border(radius: 16)
                                        .OnClick(() => firstCount.Value++)
                                        .Name("first-button")
                                );
                            }

                            var secondCount = Remember(() => MutableStateOf(0));
                            Text(
                                text: $"Clicked {secondCount.Value} times",
                                fontSize: 20,
                                align: TextAnchor.MiddleCenter,
                                style: Modifier
                                    .Background(Color.green)
                                    .NewPadding(all: 20)
                                    .Border(radius: 16)
                                    .Margin(top: 16)
                                    .OnClick(() => secondCount.Value++)
                                    .Name("second-button")
                            );

                            Text(
                                text: "Switch",
                                fontSize: 20,
                                align: TextAnchor.MiddleCenter,
                                style: Modifier
                                    .Background(Color.blue)
                                    .NewPadding(all: 20)
                                    .Border(radius: 16)
                                    .Margin(top: 16)
                                    .OnClick(() => showFirst.Value = !showFirst.Value)
                                    .Name("switch-button")
                            );
                        }
                    );
                }
            );
        }
    }
}