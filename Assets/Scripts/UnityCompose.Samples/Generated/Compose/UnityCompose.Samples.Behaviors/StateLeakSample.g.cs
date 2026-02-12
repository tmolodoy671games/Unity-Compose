#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class StateLeakSample
    {
        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(293258060);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __CompositionLocalProvider(LocalTextStyle.Provides(new TextStyle(FontSize: 40, Color: Color.black)), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        __Column(modifier: Modifier, content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            var showFirst = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                            __composer.StartReplaceGroup(244906544);
                            if (showFirst.Value)
                            {
                                var firstCount = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0)));
                                __Text(text: $"Clicked {firstCount.Value} times", textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.red).Padding(all: 20.Px()).Border(radius: 16.Px()).OnClick((!__composer.Changed(firstCount) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => firstCount.Value++))).Name("first-button"), __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_00);
                            }

                            __composer.EndReplaceGroup(244906544);
                            var secondCount = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0)));
                            __Text(text: $"Clicked {secondCount.Value} times", textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.green).Padding(all: 20.Px()).Border(radius: 16.Px()).Margin(top: 16.Px()).OnClick((!__composer.Changed(secondCount) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => secondCount.Value++))).Name("second-button"), __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_00);
                            __Text(text: "Switch", textAlign: TextAlign.MiddleCenter, modifier: Modifier.FillMaxWidth().Background(Color.blue).Padding(all: 20.Px()).Border(radius: 16.Px()).Margin(top: 16.Px()).OnClick((!__composer.Changed(showFirst) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => showFirst.Value = !showFirst.Value))).Name("switch-button"), __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01);
                        })), __composer: __composer, __changed: 0b_01_01_00_00);
                    })), __composer: __composer, __changed: 0);
                })), __composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(293258060, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer, 0b_10);
        }
    }
}