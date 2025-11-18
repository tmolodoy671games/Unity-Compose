using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class DisposableEffectSample : ComposeUI
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                {
                    var isEffectRunning = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IMutableState<bool>>>(__ => () => MutableStateOf(false)));
                    if (isEffectRunning.Value)
                    {
                        DisposableEffect(string.Empty, CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(__ => it =>
                        {
                            Debug.Log("DisposableEffect()");
                            return it.OnDispose(CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () => Debug.Log("OnDispose()")));
                        }));
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IMutableState<bool>>>(__ => () => MutableStateOf(false)));
                    Text(text: $"DisposableEffect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20).Padding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).Border(radius: 16).Margin(top: 32).OnMouseEnter(CurrentComposer.WithState(isHovered).Remember<System.Action>(__ => () => isHovered.Value = true)).OnMouseLeave(CurrentComposer.WithState(isHovered).Remember<System.Action>(__ => () => isHovered.Value = false)).OnClick(CurrentComposer.WithState(isEffectRunning).Remember<System.Action>(__ => () => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}