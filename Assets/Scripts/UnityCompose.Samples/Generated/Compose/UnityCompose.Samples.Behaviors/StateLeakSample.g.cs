using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
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
                    Column(modifier: Modifier, content: RememberComposable<global::System.Action>(null, () =>
                    {
                        var showFirst = Remember(() => MutableStateOf(false));
                        if (showFirst.Value)
                        {
                            var firstCount = Remember(() => MutableStateOf(0));
                            Text(text: $"Clicked {firstCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.red).Padding(all: 20).Border(radius: 16).OnClick(Remember<global::System.Action>(firstCount, () => firstCount.Value++)).Name("first-button"));
                        }

                        var secondCount = Remember(() => MutableStateOf(0));
                        Text(text: $"Clicked {secondCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.green).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(Remember<global::System.Action>(secondCount, () => secondCount.Value++)).Name("second-button"));
                        Text(text: "Switch", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.blue).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(Remember<global::System.Action>(showFirst, () => showFirst.Value = !showFirst.Value)).Name("switch-button"));
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