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
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: !__composer.RememberedKeyChanged<bool>(-1900480729, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    var count = !__composer.RememberedKeyChanged<bool>(881651747, true) ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                    Text(text: count.Value.ToString(), color: Color.white, fontSize: 40, modifier: Modifier.Name("test-label").Background(Color.red).Padding(all: 10));
                    var isEffectRunning = !__composer.RememberedKeyChanged<bool>(-1413886213, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    __composer.StartReplaceGroup(-1237439406);
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

                        LaunchedEffect(key: string.Empty, coroutine: !__composer.RememberedKeyChanged<UnityCompose.IMutableState<int>?>(-127504686, count) ? CurrentComposer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : CurrentComposer.UpdateLambda<System.Func<System.Collections.IEnumerator>>(() => EffectCoroutine()));
                    }

                    __composer.EndReplaceGroup(-1237439406);
                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = !__composer.RememberedKeyChanged<bool>(-89714165, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    Text(text: $"Launched Effect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20).Padding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).Border(radius: 16).Margin(top: 32).OnMouseEnter(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-420369165, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(870908836, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = false)).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(1157415743, isEffectRunning) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isEffectRunning.Value = !isEffectRunning.Value)));
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