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
            __composer.StartRestartGroup(217995475);
            if (__composer.ShouldExecute(true))
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: !__composer.RememberedKeyChanged<bool>(2105749243, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    var isEffectRunning = !__composer.RememberedKeyChanged<bool>(-714569865, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    __composer.StartReplaceGroup(497652786);
                    if (isEffectRunning.Value)
                    {
                        DisposableEffect(string.Empty, !__composer.RememberedKeyChanged<bool>(2031826270, true) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
                        {
                            Debug.Log("DisposableEffect()");
                            return it.OnDispose(!__composer.RememberedKeyChanged<bool>(1388979315, true) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => Debug.Log("OnDispose()")));
                        }));
                    }

                    __composer.EndReplaceGroup(497652786);
                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = !__composer.RememberedKeyChanged<bool>(-136830866, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    Text(text: $"DisposableEffect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20).Padding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).Border(radius: 16).Margin(top: 32).OnMouseEnter(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(1295173627, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-1123855782, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = false)).OnClick(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-973640963, isEffectRunning) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(217995475)?.UpdateScope(() => __Layout());
        }
    }
}