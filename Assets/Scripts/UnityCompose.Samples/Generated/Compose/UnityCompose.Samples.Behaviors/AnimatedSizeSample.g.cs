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
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1252201391);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                const int AnimationDuration = 2;
                var animationSpec = Tween(AnimationDuration);
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: !__composer.ChangedAsStruct((AnimationDuration, animationSpec)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-size-sample"), content: !__composer.ChangedAsStruct((AnimationDuration, animationSpec)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                        var text = isSwitched.Value ? "Short" : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                        AnimatedSize(modifier: Modifier.Name("animated-size").Background(isSwitched.Value ? Color.green : Color.red, Transition(AnimationDuration)).Padding(all: 16), animationSpec: animationSpec, content: !__composer.Changed(text) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            Text(text: text, color: Color.white, fontSize: 64, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("animated-label-child"));
                        }));
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Name("switch-button").Padding(all: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value)));
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1252201391, __isRestarted)?.UpdateScope(() => __Layout());
        }
    }
}