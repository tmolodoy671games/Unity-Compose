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
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                {
                    var count = Remember(() => MutableStateOf(0));
                    Text(text: count.Value.ToString(), color: Color.white, fontSize: 40, modifier: Modifier.Name("test-label").Background(Color.red).Padding(all: 10));
                    var isEffectRunning = Remember(() => MutableStateOf(false));
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

                        LaunchedEffect(key: string.Empty, CurrentComposer.WithState(EffectCoroutine).Remember<Func<IEnumerator>>(static __ => () => __()));
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = Remember(() => MutableStateOf(false));
                    Text(text: $"Launched Effect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20).Padding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).Border(radius: 16).Margin(top: 32).OnMouseEnter(CurrentComposer.WithState(isHovered).Remember<System.Action>(__ => () => isHovered.Value = true)).OnMouseLeave(CurrentComposer.WithState(isHovered).Remember<System.Action>(__ => () => isHovered.Value = false)).OnClick(CurrentComposer.WithState(isEffectRunning).Remember<System.Action>(__ => () => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}