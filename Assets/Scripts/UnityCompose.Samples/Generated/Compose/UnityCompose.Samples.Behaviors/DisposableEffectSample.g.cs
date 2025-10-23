using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class DisposableEffectSample
    {
        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Column(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: Modifier.Name("launched-effect-disposal").FlexGrow(1), content: RememberComposable<global::System.Action>(null, () =>
                {
                    var isEffectRunning = Remember(() => MutableStateOf(false));
                    if (isEffectRunning.Value)
                    {
                        DisposableEffect(null, Remember<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::System.IDisposable>>(null, it =>
                        {
                            Debug.Log("DisposableEffect()");
                            return it.OnDispose(() => Debug.Log("OnDispose()"));
                        }));
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = Remember(() => MutableStateOf(false));
                    Label(text: $"DisposableEffect is {onOrOff}", textColor: Color.white, fontSize: 40, style: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).NewPadding(vertical: 20).NewPadding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).BorderRadius(16).Margin(top: 32).OnMouseEnter(Remember<global::System.Action>(isHovered, () => isHovered.Value = true)).OnMouseLeave(Remember<global::System.Action>(isHovered, () => isHovered.Value = false)).OnClick(Remember<global::System.Action>(isEffectRunning, () => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}