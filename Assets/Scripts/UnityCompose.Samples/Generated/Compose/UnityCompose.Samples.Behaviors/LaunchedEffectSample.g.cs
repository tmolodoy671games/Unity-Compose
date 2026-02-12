#nullable enable
using System;
using System.Collections;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class LaunchedEffectSample
    {
        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1340125706);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var count = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0)));
                    __Text(text: count.Value.ToString(), color: Color.white, fontSize: 40, modifier: Modifier.Name("test-label").Background(Color.red).Padding(all: 10.Px()), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                    var isEffectRunning = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __composer.StartReplaceGroup(2066868923);
                    if (isEffectRunning.Value)
                    {
                        IEnumerator EffectCoroutine()
                        {
                            while (true)
                            {
                                yield return new WaitForSeconds(1f);
                                count.Value++;
                            }
                        }

                        __LaunchedEffect(key: string.Empty, coroutine: (!__composer.Changed(count) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => EffectCoroutine())), __composer: __composer, __changed: 0);
                    }

                    __composer.EndReplaceGroup(2066868923);
                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Text(text: $"Launched Effect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(__AnimateColorAsState(isHovered.Value ? Color.cyan : Color.blue, __composer: __composer, __changed: 0b_01_00).Value).Padding(vertical: 20.Px()).Padding(horizontal: __AnimateFloatAsState(isHovered.Value ? 40 : 20, __composer: __composer, __changed: 0b_01_00).Value.Px()).Border(radius: 16.Px()).Margin(top: 32.Px()).OnMouseEnter((!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = false))).OnClick((!__composer.Changed(isEffectRunning) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isEffectRunning.Value = !isEffectRunning.Value))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                })), __composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1340125706, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer, 0b_10);
        }
    }
}