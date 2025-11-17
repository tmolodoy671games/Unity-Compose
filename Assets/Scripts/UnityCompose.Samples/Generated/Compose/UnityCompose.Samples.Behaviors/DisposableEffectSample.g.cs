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
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: static () =>
                {
                    var isEffectRunning = Remember(static () => MutableStateOf(false));
                    if (isEffectRunning.Value)
                    {
                        DisposableEffect(string.Empty, static it =>
                        {
                            Debug.Log("DisposableEffect()");
                            return it.OnDispose(static () => Debug.Log("OnDispose()"));
                        });
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = Remember(static () => MutableStateOf(false));
                    Text(text: $"DisposableEffect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20).Padding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).Border(radius: 16).Margin(top: 32).OnMouseEnter(CurrentComposer.WithState(isHovered).Remember<Action>(__ => () => isHovered.Value = true)).OnMouseLeave(CurrentComposer.WithState(isHovered).Remember<Action>(__ => () => isHovered.Value = false)).OnClick(CurrentComposer.WithState(isEffectRunning).Remember<Action>(__ => () => isEffectRunning.Value = !isEffectRunning.Value)));
                });
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }
    }
}