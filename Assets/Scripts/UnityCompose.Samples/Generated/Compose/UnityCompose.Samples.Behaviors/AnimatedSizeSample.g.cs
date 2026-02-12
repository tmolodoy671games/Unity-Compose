#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedSizeSample
    {
        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(484197062);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                const int AnimationDuration = 2;
                var animationSpec = Tween(AnimationDuration);
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.ChangedAsStruct((AnimationDuration, animationSpec)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    __Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("animated-size-sample"), content: (!__composer.ChangedAsStruct((AnimationDuration, animationSpec)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                        var text = isSwitched.Value ? "Short" : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                        __AnimatedSize(modifier: Modifier.Name("animated-size").Background(isSwitched.Value ? Color.green : Color.red, Transition(AnimationDuration)).Padding(all: 16.Px()), animationSpec: animationSpec, content: (!__composer.Changed(text) ? __composer.RememberedValue<UnityCompose.ComposableContent<UnityCompose.IModifier>>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent<UnityCompose.IModifier>>(modifier =>
                        {
                            __Text(text: text, color: Color.white, fontSize: 64, textAlign: TextAlign.MiddleCenter, modifier: modifier.Name("animated-label-child"), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                        })), __composer: __composer, __changed: 0);
                        __Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Name("switch-button").Padding(all: 32.Px()).Background(Color.blue).Margin(top: 16.Px()).Border(radius: 16.Px()).OnClick((!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                    })), __composer: __composer, __changed: 0b_01_00_00_00);
                })), __composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(484197062, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer, 0b_10);
        }
    }
}