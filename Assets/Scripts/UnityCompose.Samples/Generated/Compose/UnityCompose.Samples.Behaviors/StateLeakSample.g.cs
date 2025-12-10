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
            if (__composer.ShouldExecute(true))
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.RememberedKeyChanged<bool>(1580305090, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Column(modifier: Modifier, content: !__composer.RememberedKeyChanged<bool>(169014577, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        var showFirst = !__composer.RememberedKeyChanged<bool>(-1971138338, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                        __composer.StartReplaceGroup(266786502);
                        if (showFirst.Value)
                        {
                            var firstCount = !__composer.RememberedKeyChanged<bool>(-7047828, true) ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                            Text(text: $"Clicked {firstCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.red).Padding(all: 20).Border(radius: 16).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<int>?>(-445599787, firstCount) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => firstCount.Value++)).Name("first-button"));
                        }

                        __composer.EndReplaceGroup(266786502);
                        var secondCount = !__composer.RememberedKeyChanged<bool>(-1064852914, true) ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                        Text(text: $"Clicked {secondCount.Value} times", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.green).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<int>?>(-1429953595, secondCount) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => secondCount.Value++)).Name("second-button"));
                        Text(text: "Switch", fontSize: 20, textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.blue).Padding(all: 20).Border(radius: 16).Margin(top: 16).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-1750898289, showFirst) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => showFirst.Value = !showFirst.Value)).Name("switch-button"));
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