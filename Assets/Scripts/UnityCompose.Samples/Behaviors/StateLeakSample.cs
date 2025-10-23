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
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                style: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Column(
                        style: Modifier,
                        content: scope =>
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
                                        .Then(scope.FillMaxWidth())
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
                                    .Then(scope.FillMaxWidth())
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
                                    .Then(scope.FillMaxWidth())
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