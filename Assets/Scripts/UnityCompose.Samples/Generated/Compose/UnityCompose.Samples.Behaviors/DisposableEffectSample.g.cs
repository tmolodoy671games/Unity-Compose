#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class DisposableEffectSample
    {
        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-790205923);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var isEffectRunning = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    __composer.StartReplaceGroup(699811654);
                    if (isEffectRunning.Value)
                    {
                        DisposableEffect(string.Empty, !__composer.Changed() ? __composer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, UnityCompose.IDisposableEffectResult>>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.IDisposableEffectScope, UnityCompose.IDisposableEffectResult>>(it =>
                        {
                            Debug.Log("DisposableEffect()");
                            return it.OnDispose(() => Debug.Log("OnDispose()"));
                        }));
                    }

                    __composer.EndReplaceGroup(699811654);
                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    Text(text: $"DisposableEffect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20.Px()).Padding(horizontal: isHovered.Value ? 40.Px() : 20.Px(), transition: Transition()).Border(radius: 16.Px()).Margin(top: 32.Px()).OnMouseEnter(!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = false)).OnClick(!__composer.Changed(isEffectRunning) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-790205923, __isRestarted)?.UpdateScope(() => __Layout());
        }
    }
}