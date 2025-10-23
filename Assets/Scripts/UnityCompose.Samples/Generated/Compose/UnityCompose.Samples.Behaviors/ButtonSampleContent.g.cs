using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent
    {
        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Column(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: Modifier.Width(100.Percent()).Height(100.Percent()).BackgroundColor(Color.white), content: RememberComposable<global::System.Action>(null, () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    Box(style: Modifier.Padding(isHovered.Value ? 80 : 40, 16, Transition()).BackgroundColor(Color.blue).BorderRadius(16).OnMouseEnter(Remember<global::System.Action>(isHovered, () => isHovered.Value = true)).OnMouseLeave(Remember<global::System.Action>(isHovered, () => isHovered.Value = false)), content: RememberComposable<global::System.Action>(null, () =>
                    {
                        Label(text: "Click me", fontSize: 24);
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}