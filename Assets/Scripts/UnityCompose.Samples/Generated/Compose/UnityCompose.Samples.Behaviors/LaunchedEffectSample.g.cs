using System.Collections;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class LaunchedEffectSample
    {
        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Column(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: RememberComposable<global::System.Action>(null, () =>
                {
                    var count = Remember(() => MutableStateOf(0));
                    Text(text: count.Value.ToString(), textColor: Color.white, fontSize: 40, style: Modifier.Name("test-label").Background(Color.red).NewPadding(all: 10));
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

                        LaunchedEffect(key: null, EffectCoroutine());
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = Remember(() => MutableStateOf(false));
                    Text(text: $"Launched Effect is {onOrOff}", textColor: Color.white, fontSize: 40, style: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).NewPadding(vertical: 20).NewPadding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).Border(radius: 16).Margin(top: 32).OnMouseEnter(Remember<global::System.Action>(isHovered, () => isHovered.Value = true)).OnMouseLeave(Remember<global::System.Action>(isHovered, () => isHovered.Value = false)).OnClick(Remember<global::System.Action>(isEffectRunning, () => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}