#nullable enable
// ReSharper disable ArrangeNamespaceBody

using UnityEngine.UIElements;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(264962251);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(264962251, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1125058844);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1125058844, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(166723089);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    var isPressed = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    var isCapturingPointer = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Box(modifier: Modifier.Padding(horizontal: __AnimateFloatAsState(isHovered.Value ? 80 : 40, __composer: __composer, __changed: 0b_01_01_00).Value.Px(), vertical: 16.Px()).Background(__AnimateColorAsState(isPressed.Value ? Color.darkBlue : Color.blue, __composer: __composer, __changed: 0b_01_01_00).Value).Border(radius: 16.Px()).OnMouseEnter((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = false))).CapturePointer(isCapturingPointer.Value).Scale(2).OnLmbDown((!__composer.BuildChanged().Changed<global::UnityCompose.IMutableState<bool>>(isPressed!).Changed<global::UnityCompose.IMutableState<bool>>(isCapturingPointer!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                    {
                        isPressed.Value = true;
                        isCapturingPointer.Value = true;
                    }))).OnLmbUp((!__composer.BuildChanged().Changed<global::UnityCompose.IMutableState<bool>>(isPressed!).Changed<global::UnityCompose.IMutableState<bool>>(isCapturingPointer!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                    {
                        isPressed.Value = false;
                        isCapturingPointer.Value = false;
                    }))), content: (!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isPressed!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __CompositionLocalProvider(LocalContentColor.Provides(Color.white), content: (!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isPressed!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __Text(text: "Click me", fontSize: 24, modifier: Modifier.Scale(__AnimateFloatAsState(isPressed.Value ? 0.6f : 1f, __composer: __composer, __changed: 0b_01_01_00).Value).Alpha(__AnimateFloatAsState(isPressed.Value ? 0.6f : 1f, __composer: __composer, __changed: 0b_01_01_00).Value), __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01);
                        })), __composer: __composer, __changed: 0b_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(166723089, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}