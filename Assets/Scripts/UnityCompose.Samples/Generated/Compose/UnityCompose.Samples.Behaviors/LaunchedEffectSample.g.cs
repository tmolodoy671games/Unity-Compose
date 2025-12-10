using System;
using System.Collections;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class LaunchedEffectSample
    {
        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(964402292);
            if (__composer.ShouldExecute(true))
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    var count = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                    Text(text: count.Value.ToString(), color: Color.white, fontSize: 40, modifier: Modifier.Name("test-label").Background(Color.red).Padding(all: 10));
                    var isEffectRunning = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    __composer.StartReplaceGroup(725906995);
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

                        LaunchedEffect(key: string.Empty, coroutine: !__composer.Changed(count) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => EffectCoroutine()));
                    }

                    __composer.EndReplaceGroup(725906995);
                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    Text(text: $"Launched Effect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20).Padding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).Border(radius: 16).Margin(top: 32).OnMouseEnter(!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = false)).OnClick(!__composer.Changed(isEffectRunning) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(964402292)?.UpdateScope(() => __Layout());
        }
    }
}