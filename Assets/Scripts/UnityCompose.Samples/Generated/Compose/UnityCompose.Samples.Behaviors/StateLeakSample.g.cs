using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class StateLeakSample : ComposeUI
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                {
                    Column(modifier: Modifier, content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                    {
                        var showFirst = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IMutableState<bool>>>(__ => () => MutableStateOf(false)));
                        if (showFirst.Value)
                        {
                            var firstCount = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IMutableState<int>>>(__ => () => MutableStateOf(0)));
                            Text(text: $"Clicked {firstCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.red).Padding(all: 20).Border(radius: 16).OnClick(CurrentComposer.WithState(firstCount).Remember<System.Action>(__ => () => firstCount.Value++)).Name("first-button"));
                        }

                        var secondCount = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IMutableState<int>>>(__ => () => MutableStateOf(0)));
                        Text(text: $"Clicked {secondCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.green).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(CurrentComposer.WithState(secondCount).Remember<System.Action>(__ => () => secondCount.Value++)).Name("second-button"));
                        Text(text: "Switch", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.blue).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(CurrentComposer.WithState(showFirst).Remember<System.Action>(__ => () => showFirst.Value = !showFirst.Value)).Name("switch-button"));
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