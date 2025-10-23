using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class StateLeakSample
    {
        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Box(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Column(alignHorizontally: Align.Stretch, style: Modifier, content: RememberComposable<global::System.Action>(null, () =>
                    {
                        var showFirst = Remember(() => MutableStateOf(false));
                        if (showFirst.Value)
                        {
                            var firstCount = Remember(() => MutableStateOf(0));
                            Label(text: $"Clicked {firstCount.Value} times", fontSize: 20, align: TextAnchor.MiddleCenter, style: Modifier.Background(Color.red).NewPadding(all: 20).Border(radius: 16).OnClick(Remember<global::System.Action>(firstCount, () => firstCount.Value++)).Name("first-button"));
                        }

                        var secondCount = Remember(() => MutableStateOf(0));
                        Label(text: $"Clicked {secondCount.Value} times", fontSize: 20, align: TextAnchor.MiddleCenter, style: Modifier.Background(Color.green).NewPadding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(Remember<global::System.Action>(secondCount, () => secondCount.Value++)).Name("second-button"));
                        Label(text: "Switch", fontSize: 20, align: TextAnchor.MiddleCenter, style: Modifier.Background(Color.blue).NewPadding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(Remember<global::System.Action>(showFirst, () => showFirst.Value = !showFirst.Value)).Name("switch-button"));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}