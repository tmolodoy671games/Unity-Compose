using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedSizeSample
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(1287702244, true))
                return;
            try
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(2089918652, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-size-sample"), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-1780592092, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                    {
                        var isSwitched = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(1961463133, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
                        var text = isSwitched.Value ? "Short" : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                        AnimatedSize(modifier: Modifier.Name("animated-size").Background(isSwitched.Value ? Color.green : Color.red, Transition(5)).Padding(all: 16), animationSpec: Tween(duration: 2), content: CurrentComposer.HasRememberedValue<string?, UnityCompose.ComposableContent>(1813367714, text) ? CurrentComposer.RememberedValue<string?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<string?, UnityCompose.ComposableContent>(() =>
                        {
                            Text(text: text, color: Color.white, fontSize: 64, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("animated-label-child"));
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Name("switch-button").Padding(all: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-1428713874, isSwitched) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isSwitched.Value = !isSwitched.Value)));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1287802244, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }
    }
}