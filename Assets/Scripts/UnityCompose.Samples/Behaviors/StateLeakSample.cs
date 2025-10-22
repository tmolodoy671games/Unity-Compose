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
                style: ComposeStyle.Empty
                    .FlexGrow(1),
                content: () =>
                {
                    Column(
                        alignHorizontally: Align.Stretch,
                        style: ComposeStyle.Empty,
                        content: () =>
                        {
                            var showFirst = Remember(() => MutableStateOf(false));
                            if (showFirst.Value)
                            {
                                var firstCount = Remember(() => MutableStateOf(0));
                                Label(
                                    text: $"Clicked {firstCount.Value} times",
                                    fontSize: 20,
                                    align: TextAnchor.MiddleCenter,
                                    style: ComposeStyle.Empty
                                        .BackgroundColor(Color.red)
                                        .Padding(20)
                                        .BorderRadius(16)
                                        .OnClick(() => firstCount.Value++)
                                        .Name("first-button")
                                );
                            }

                            var secondCount = Remember(() => MutableStateOf(0));
                            Label(
                                text: $"Clicked {secondCount.Value} times",
                                fontSize: 20,
                                align: TextAnchor.MiddleCenter,
                                style: ComposeStyle.Empty
                                    .BackgroundColor(Color.green)
                                    .Padding(20)
                                    .BorderRadius(16)
                                    .MarginTop(16)
                                    .OnClick(() => secondCount.Value++)
                                    .Name("second-button")
                            );

                            Label(
                                text: "Switch",
                                fontSize: 20,
                                align: TextAnchor.MiddleCenter,
                                style: ComposeStyle.Empty
                                    .BackgroundColor(Color.blue)
                                    .Padding(20)
                                    .BorderRadius(16)
                                    .MarginTop(16)
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