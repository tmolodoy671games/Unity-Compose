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
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: static () =>
                {
                    Column(modifier: Modifier, content: static () =>
                    {
                        var showFirst = Remember(static () => MutableStateOf(false));
                        if (showFirst.Value)
                        {
                            var firstCount = Remember(static () => MutableStateOf(0));
                            Text(text: $"Clicked {firstCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.red).Padding(all: 20).Border(radius: 16).OnClick(CurrentComposer.WithState(firstCount).Remember<Action>(__ => () => firstCount.Value++)).Name("first-button"));
                        }

                        var secondCount = Remember(static () => MutableStateOf(0));
                        Text(text: $"Clicked {secondCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.green).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(CurrentComposer.WithState(secondCount).Remember<Action>(__ => () => secondCount.Value++)).Name("second-button"));
                        Text(text: "Switch", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.blue).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(CurrentComposer.WithState(showFirst).Remember<Action>(__ => () => showFirst.Value = !showFirst.Value)).Name("switch-button"));
                    });
                });
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }
    }
}