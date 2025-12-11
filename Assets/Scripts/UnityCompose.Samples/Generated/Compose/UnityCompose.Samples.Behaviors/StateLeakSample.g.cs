using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class StateLeakSample
    {
        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-535283690);
            if (__composer.ShouldExecute())
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Column(modifier: Modifier, content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var showFirst = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                        if (showFirst.Value)
                        {
                            var firstCount = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                            Text(text: $"Clicked {firstCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.red).Padding(all: 20).Border(radius: 16).OnClick(!__composer.Changed(firstCount) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => firstCount.Value++)).Name("first-button"));
                        }

                        var secondCount = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                        Text(text: $"Clicked {secondCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.green).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(!__composer.Changed(secondCount) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => secondCount.Value++)).Name("second-button"));
                        Text(text: "Switch", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.blue).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(!__composer.Changed(showFirst) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => showFirst.Value = !showFirst.Value)).Name("switch-button"));
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-535283690)?.UpdateScope(() => __Layout());
        }
    }
}