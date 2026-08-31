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
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1158908072);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1158908072, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1514850918);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1514850918, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1324991242);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var count = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<int>>(MutableStateOf(0)));
                    __Text(text: count.Value.ToString(), color: Color.white, fontSize: 40.Sp(), modifier: Modifier.Name("test-label").Background(Color.red).Padding(all: 10.Dp()), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_00);
                    var isEffectRunning = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __composer.StartReplaceGroup(639840857);
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

                        __LaunchedEffect(key: string.Empty, coroutine: (!__composer.Changed<global::UnityCompose.IMutableState<int>>(count!) ? __composer.RememberedValue<global::System.Func<global::System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<global::System.Func<global::System.Collections.IEnumerator>>(() => EffectCoroutine())), __composer: __composer, __changed: 0b_00_00);
                    }

                    __composer.EndReplaceGroup(639840857);
                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Text(text: $"Launched Effect is {onOrOff}", color: Color.white, fontSize: 40.Sp(), modifier: Modifier.Name("test-button").Background(__AnimateColorAsState(isHovered.Value ? Color.cyan : Color.blue, __composer: __composer, __changed: 0b_01_00).Value).Padding(vertical: 20.Dp()).Padding(horizontal: __AnimateFloatAsState(isHovered.Value ? 40 : 20, __composer: __composer, __changed: 0b_01_00).Value.Dp()).Clip(RoundedCornerShape(16.Dp())).Margin(top: 32.Dp()).OnMouseEnter((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = false))).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isEffectRunning!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isEffectRunning.Value = !isEffectRunning.Value))), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1324991242, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}