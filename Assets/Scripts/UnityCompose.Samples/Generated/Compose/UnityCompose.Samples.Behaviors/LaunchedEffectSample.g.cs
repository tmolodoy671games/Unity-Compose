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
                Column(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: IModifier.Empty.Name("launched-effect-disposal").FlexGrow(1), content: RememberComposable<global::System.Action>(null, () =>
                {
                    var count = Remember(() => MutableStateOf(0));
                    Label(text: count.Value.ToString(), textColor: Color.white, fontSize: 40, style: IModifier.Empty.Name("test-label").BackgroundColor(Color.red).Padding(10));
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
                    Label(text: $"Launched Effect is {onOrOff}", textColor: Color.white, fontSize: 40, style: IModifier.Empty.Name("test-button").BackgroundColor(isHovered.Value ? Color.cyan : Color.blue, Transition()).PaddingVertical(20).PaddingHorizontal(isHovered.Value ? 40 : 20, Transition()).BorderRadius(16).MarginTop(32).OnMouseEnter(Remember<global::System.Action>(isHovered, () => isHovered.Value = true)).OnMouseLeave(Remember<global::System.Action>(isHovered, () => isHovered.Value = false)).OnClick(Remember<global::System.Action>(isEffectRunning, () => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}