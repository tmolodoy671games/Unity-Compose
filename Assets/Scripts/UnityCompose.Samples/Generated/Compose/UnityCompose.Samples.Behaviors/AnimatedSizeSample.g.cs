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
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, style: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, style: Modifier.Name("animated-size-sample"), content: RememberComposable<global::System.Action>(null, () =>
                    {
                        var isSwitched = Remember(() => MutableStateOf(false));
                        var text = isSwitched.Value ? "Short" : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                        AnimatedSize(style: Modifier.Name("animated-size").Background(isSwitched.Value ? Color.green : Color.red, Transition(5)).NewPadding(all: 16), duration: 5, content: Remember<global::System.Action>(text, () =>
                        {
                            Text(text: text, textColor: Color.white, fontSize: 64, align: TextAnchor.MiddleCenter, style: Modifier.Name("animated-label-child"));
                        }));
                        Text(text: "Switch", textColor: Color.white, fontSize: 64, style: Modifier.Name("switch-button").NewPadding(all: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value)));
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