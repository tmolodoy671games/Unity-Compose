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
            if (CurrentComposer.BeginComposeGroup(-535283690, true))
                return;
            try
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(1580305090, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    Column(modifier: Modifier, content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(169014577, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                    {
                        var showFirst = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(-1971138338, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
                        if (showFirst.Value)
                        {
                            var firstCount = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<int>>(-7047828, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<int>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<int>>(() => MutableStateOf(0));
                            Text(text: $"Clicked {firstCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.red).Padding(all: 20).Border(radius: 16).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<int>?, System.Action>(-445599787, firstCount) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<int>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<int>?, System.Action>(() => firstCount.Value++)).Name("first-button"));
                        }

                        var secondCount = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<int>>(-1064852914, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<int>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<int>>(() => MutableStateOf(0));
                        Text(text: $"Clicked {secondCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.green).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<int>?, System.Action>(-1429953595, secondCount) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<int>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<int>?, System.Action>(() => secondCount.Value++)).Name("second-button"));
                        Text(text: "Switch", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.blue).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-1750898289, showFirst) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => showFirst.Value = !showFirst.Value)).Name("switch-button"));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-535183690, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }
    }
}