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
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(854572948);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(854572948, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1267362897);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1267362897, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1913327683);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                const int AnimationDuration = 2;
                var animationSpec = Tween(AnimationDuration);
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed<global::UnityCompose.AnimationSpec>(animationSpec!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("animated-size-sample"), content: (!__composer.Changed<global::UnityCompose.AnimationSpec>(animationSpec!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                        var text = isSwitched.Value ? "Short" : "Loooooooooooooong\nLoooooooooooooong\nLoooooooooooooong";
                        __AnimatedSize(modifier: Modifier.Name("animated-size").Background(isSwitched.Value ? Color.green : Color.red, transition: Transition(AnimationDuration)).Padding(all: 16.Dp()).Clip(RoundedCornerShape(__AnimateFloatAsState(isSwitched.Value ? 8 : 32, animationSpec, __composer: __composer, __changed: 0b_01_00_00).Value.Dp())), animationSpec: animationSpec, content: (!__composer.Changed<string>(text!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent<global::UnityCompose.IModifier>>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent<global::UnityCompose.IModifier>>(modifier =>
                        {
                            __Text(text: text, color: Color.white, fontSize: 64.Sp(), textAlign: TextAlign.MiddleCenter, modifier: modifier.Name("animated-label-child"), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_00);
                        })), __composer: __composer, __changed: 0b_00_00_00);
                        __Text(text: "Switch", color: Color.white, fontSize: 64.Sp(), modifier: Modifier.Name("switch-button").Padding(all: 32.Dp()).Background(Color.blue).Margin(top: 16.Dp()).Clip(RoundedCornerShape(16.Dp())).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isSwitched.Value = !isSwitched.Value))), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_01);
                    })), __composer: __composer, __changed: 0b_01_00_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1913327683, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}