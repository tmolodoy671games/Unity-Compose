using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
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
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-size-sample"), content: RememberComposable<global::System.Action>(null, () =>
                    {
                        var isSwitched = Remember(() => MutableStateOf(false));
                        var text = isSwitched.Value ? "Short" : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                        AnimatedSize(modifier: Modifier.Name("animated-size").Background(isSwitched.Value ? Color.green : Color.red, Transition(5)).Padding(all: 16), animationSpec: Tween(duration: 5), content: Remember<global::System.Action>(text, () =>
                        {
                            Text(text: text, color: Color.white, fontSize: 64, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("animated-label-child"));
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Name("switch-button").Padding(all: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value)));
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