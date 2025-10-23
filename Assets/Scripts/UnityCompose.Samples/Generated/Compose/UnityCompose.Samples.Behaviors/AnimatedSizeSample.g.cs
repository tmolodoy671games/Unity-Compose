using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedSizeSample
    {
        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Box(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: IModifier.Empty.Width(100.Percent()).Height(100.Percent()).FlexGrow(1), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Column(alignHorizontally: Align.Center, style: IModifier.Empty.Name("animated-size-sample"), content: RememberComposable<global::System.Action>(null, () =>
                    {
                        var isSwitched = Remember(() => MutableStateOf(false));
                        var text = isSwitched.Value ? "Short" : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                        AnimatedSize(style: IModifier.Empty.Name("animated-size").BackgroundColor(isSwitched.Value ? Color.green : Color.red, Transition(5)).Padding(16), duration: 5, content: Remember<global::System.Action>(text, () =>
                        {
                            Label(text: text, textColor: Color.white, fontSize: 64, align: TextAnchor.MiddleCenter, style: IModifier.Empty.Name("animated-label-child"));
                        }));
                        Label(text: "Switch", textColor: Color.white, fontSize: 64, style: IModifier.Empty.Name("switch-button").Padding(32).BackgroundColor(Color.blue).MarginTop(16).BorderRadius(16).OnClick(Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value)));
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