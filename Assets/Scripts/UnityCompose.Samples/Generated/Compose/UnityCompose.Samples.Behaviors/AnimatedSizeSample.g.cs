using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedSizeSample : ComposeUI
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-size-sample"), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                    {
                        var isSwitched = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IMutableState<bool>>>(__ => () => MutableStateOf(false)));
                        var text = isSwitched.Value ? "Short" : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                        AnimatedSize(modifier: Modifier.Name("animated-size").Background(isSwitched.Value ? Color.green : Color.red, Transition(5)).Padding(all: 16), animationSpec: Tween(duration: 2), content: CurrentComposer.WithState(text).Remember<System.Action>(__ => () =>
                        {
                            Text(text: text, color: Color.white, fontSize: 64, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("animated-label-child"));
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Name("switch-button").Padding(all: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(CurrentComposer.WithState(isSwitched).Remember<System.Action>(__ => () => isSwitched.Value = !isSwitched.Value)));
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