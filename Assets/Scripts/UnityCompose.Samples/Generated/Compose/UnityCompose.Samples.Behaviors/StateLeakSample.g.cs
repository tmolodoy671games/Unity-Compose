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
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Column(modifier: Modifier, content: RememberComposable<global::System.Action<global::UnityCompose.IColumnScope>>(null, scope =>
                    {
                        var showFirst = Remember(() => MutableStateOf(false));
                        if (showFirst.Value)
                        {
                            var firstCount = Remember(() => MutableStateOf(0));
                            Text(text: $"Clicked {firstCount.Value} times", fontSize: 20, align: TextAnchor.MiddleCenter, modifier: Modifier.Then(scope.FillMaxWidth()).Background(Color.red).NewPadding(all: 20).Border(radius: 16).OnClick(Remember<global::System.Action>(firstCount, () => firstCount.Value++)).Name("first-button"));
                        }

                        var secondCount = Remember(() => MutableStateOf(0));
                        Text(text: $"Clicked {secondCount.Value} times", fontSize: 20, align: TextAnchor.MiddleCenter, modifier: Modifier.Then(scope.FillMaxWidth()).Background(Color.green).NewPadding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(Remember<global::System.Action>(secondCount, () => secondCount.Value++)).Name("second-button"));
                        Text(text: "Switch", fontSize: 20, align: TextAnchor.MiddleCenter, modifier: Modifier.Then(scope.FillMaxWidth()).Background(Color.blue).NewPadding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(Remember<global::System.Action>(showFirst, () => showFirst.Value = !showFirst.Value)).Name("switch-button"));
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